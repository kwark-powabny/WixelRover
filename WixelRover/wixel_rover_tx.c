/*
based on the Pololu sample:
https://github.com/pololu/wixel-sdk/blob/master/apps/wireless_adc_tx/wireless_adc_tx.c
*/

/** Dependencies **************************************************************/
#include <wixel.h>
#include <usb.h>
#include <usb_com.h>
#include <radio_queue.h>
#include <stdio.h>


/** Variables ****************************************************************/
/** A temporary buffer used for composing responses to the computer before
 * they are sent.  Must be bigger than the longest possible response to any
 * command.
 */
uint8 XDATA response[32];
uint8 XDATA usbBuffer[32];

/** Parameters ****************************************************************/

int32 CODE param_input_mode = 0;
int32 CODE param_report_period_ms = 20;


/** Functions *****************************************************************/


void updateLeds()
{
    usbShowStatusWithGreenLed();
    LED_YELLOW(1);
    LED_RED(0);
}

// This function should be called regularly.
// It takes care of reading the ADC values and sending them
// to the radio when appropriate.
void commandToRadioService()
{
    static uint16 lastTx = 0;

    uint8 XDATA * txPacket;

    // Check to see if there is a radio TX buffer available.
    if (txPacket = radioQueueTxCurrentPacket())
    {

        ///uint8 i;
        uint16 XDATA * ptr = (uint16 XDATA *)&txPacket[5];

        // Byte 0 is the length.
        txPacket[0] = 10;

        // Bytes 1-4: the serial number.
        txPacket[1] = serialNumber[0];
        txPacket[2] = serialNumber[1];
        txPacket[3] = serialNumber[2];
        txPacket[4] = serialNumber[3];

        // Bytes 5-10: a command get from USB
        txPacket[5] = usbBuffer[0];
        txPacket[6] = usbBuffer[1];
        txPacket[7] = usbBuffer[2];
        txPacket[8] = usbBuffer[3];
        txPacket[9] = usbBuffer[4];
        txPacket[10] = usbBuffer[5];
        
        radioQueueTxSendPacket();
    }
}


// helper function - write to the terminal int value and it's description
void printIntToSerial(char label[50], int value)
{
    uint8 responseLength;
    responseLength = sprintf(response, "%s: %d\r\n", label, value);
    usbComTxSend(response, responseLength);
}



// receive packet with command from USB and send by radio
void sendPacketFromUsbToRadio()
{
    uint8 bytesLeft = usbComRxAvailable();
    if (bytesLeft > 0)
    {
    	// receive command from USB
    	usbComRxReceive(usbBuffer, bytesLeft);
    	//printIntToSerial("Packet received: ", bytesLeft); // for test purpose
    	
      // send command by radio
    	commandToRadioService();
    }

}

void main(void)
{
    systemInit();
    usbInit();
    radioQueueInit();

    while(1)
    {
        updateLeds();
        boardService();
        usbComService();
        sendPacketFromUsbToRadio();
     }
}
