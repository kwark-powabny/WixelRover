/*
based on the Pololu sample:
https://github.com/pololu/wixel-sdk/blob/master/apps/wireless_adc_rx/wireless_adc_rx.c
*/

/** Dependencies **************************************************************/
#include <wixel.h>
#include <usb.h>
#include <usb_com.h>
#include <radio_queue.h>

#include <stdio.h>

#define GEAR_L_DIRECTION P1_3 // Left wheel: 0 - move forward, 1 - move backward
#define GEAR_R_DIRECTION P1_2 // Right wheel: 0 - move forward, 1 - move backward

#define FORWARD 0;
#define BACKWARD 1;

/** Types *********************************************************************/

typedef struct commandData
{
    uint8 length;
    uint8 serialNumber[4]; // It can be more than one transmitter. But for now I use only one.
    uint8 command;         // Command to drive a robot: turn left, right, stop etc...
    uint8 commandParam;    // A "gear" - the speed at which the movement should be performed
    uint8 reserve[4];      // To be used in the future
} commandData;

/** Variables******************************************************************/
commandData XDATA * rxPacket; // command received by radio


/** Functions *****************************************************************/

void updateLeds()
{
    usbShowStatusWithGreenLed();
    LED_YELLOW(0);
    LED_RED(0);
}


//initialize motor driver
void motorDriverInit()
{

	//set the PWM frequency - 50 kHz
	T1CC0L = 0xE0;
	T1CC0H = 0x01;

	// fulfilment
	// Timer 1, channel 1 - right motor
	T1CC1L = 0x38;
	T1CC1H = 0x01;
	// Timer 1, channel 2 - left motor
	T1CC2L = 0x38;
	T1CC2H = 0x01;

	// Timer 1 channel 1 (for servo PWM) set compare mode 4
	T1CCTL1 = 0x24;

	// Timer 1 channel 2 (for motor PWM) set compare mode 4
	T1CCTL2 = 0x24;

	// Timer 1 set to Alternate 2 location
	PERCFG = 0x40;

	// P1_1 set peripheral function which associated with Timer 1 Ch.0 and Ch.1 outputs
	P1SEL = 0x03;

	// set ports as OUT
    P1DIR |= (1<<2); // P1_2 - GEAR_R_DIRECTION
    P1DIR |= (1<<3); // P1_3 - GEAR_L_DIRECTION

    // initial wheels direction - forward
    GEAR_L_DIRECTION = FORWARD;
    GEAR_R_DIRECTION = FORWARD;

	//// set modulo mode prescaler to Tick Freq / 1
	T1CTL = 0x02;

}

// move left motor forward: at speed defined by "gear"
void leftMotorForwardGear(int gear)
{
	switch (gear)
	{
	case 0:
		T1CC2L = 0x00;
		T1CC2H = 0x00;
		break;
	case 1:
		T1CC2L = 0x50;
		T1CC2H = 0x01;
		break;
	case 2:
		T1CC2L = 0x80;
		T1CC2H = 0x01;
		break;
	case 3:
		T1CC2L = 0xB0;
		T1CC2H = 0x01;
		break;
	case 4:
		T1CC2L = 0xE0;
		T1CC2H = 0x01;
		break;
	default:

		break;
	}
	GEAR_L_DIRECTION = FORWARD;
}

// move left motor backward: at speed defined by "gear"
void leftMotorBackGear(int gear)
{
	switch (gear)
	{
	case 0:
		T1CC2L = 0x00;
		T1CC2H = 0x00;
		break;
	case 1:
		T1CC2L = 0x44;
		T1CC2H = 0x01;
		break;
	case 2:
		T1CC2L = 0xD8;
		T1CC2H = 0x00;
		break;
	case 3:
		T1CC2L = 0x6c;
		T1CC2H = 0x00;
		break;
	case 4:
		T1CC2L = 0x01;
		T1CC2H = 0x00;
		break;
	default:

		break;
	}
	GEAR_L_DIRECTION = BACKWARD;
}

// move right motor forward: at speed defined by "gear"
void rightMotorForwardGear(int gear)
{
	switch (gear)
	{
	case 0:
		T1CC2L = 0x00;
		T1CC2H = 0x00;
		break;
	case 1:
		T1CC1L = 0x50;
		T1CC1H = 0x01;
		break;
	case 2:
		T1CC1L = 0x80;
		T1CC1H = 0x01;
		break;
	case 3:
		T1CC1L = 0xB0;
		T1CC1H = 0x01;
		break;
	case 4:
		T1CC1L = 0xE0;
		T1CC1H = 0x01;
		break;
	default:

		break;
	}
	GEAR_R_DIRECTION = FORWARD;
}

// move right motor backward: at speed defined by "gear"
void rightMotorBackGear(int gear)
{
	switch (gear)
	{
	case 0:
		T1CC2L = 0x00;
		T1CC2H = 0x00;
		break;
	case 1:
		T1CC1L = 0x44;
		T1CC1H = 0x01;
		break;
	case 2:
		T1CC1L = 0xD8;
		T1CC1H = 0x00;
		break;
	case 3:
		T1CC1L = 0x6c;
		T1CC1H = 0x00;
		break;
	case 4:
		T1CC1L = 0x01;
		T1CC1H = 0x00;
		break;
	default:

		break;
	}
	GEAR_R_DIRECTION = BACKWARD;
}


void processRadioPacket()
{

    // Check if there is a radio packet to report and space in the USB TX buffers to report it.
    //if ((rxPacket = (commandData XDATA *)radioQueueRxCurrentPacket()) && usbComTxAvailable() >= 64)
    if ((rxPacket = (commandData XDATA *)radioQueueRxCurrentPacket())) // TODO - testowo - bez usb
    {
      // We received a packet from a Wixel. Log sender number and command
       printf("%02X-%02X-%02X-%02X %5u",
               rxPacket->serialNumber[3],
               rxPacket->serialNumber[2],
               rxPacket->serialNumber[1],
               rxPacket->serialNumber[0],
               (uint16)getMs()
               );
       printf("Command: %5u", rxPacket->command);
       printf("Parameter: %5u", rxPacket->commandParam);
 
        // execute command
       switch (rxPacket->command)
        {
    	case 1: // forward
   			leftMotorForwardGear(rxPacket->commandParam);
   			rightMotorForwardGear(rxPacket->commandParam);
   			break;
    	case 2: // backward
   			leftMotorBackGear(rxPacket->commandParam);
   			rightMotorBackGear(rxPacket->commandParam);
   			break;
    	case 3: // turn right
   			leftMotorForwardGear(0);
    		rightMotorForwardGear(rxPacket->commandParam);
   			break;
    	case 4: // turn left
   			rightMotorForwardGear(0);
    		leftMotorForwardGear(rxPacket->commandParam);
   			break;

    	default:
    		// do nothing - unnown command
    		break;
        }


        radioQueueRxDoneWithPacket();
    }

}


void main(void)
{
    systemInit();
    usbInit();
    radioQueueInit();
    motorDriverInit();

    while(1) // endless loop
    {
        updateLeds();
        boardService();
        usbComService();
        processRadioPacket();
    }
}
