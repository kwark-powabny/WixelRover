# WixelRover
The repository contains a set of programs for controlling a simple three-wheeled rover. I built the robot myself in 2012. I chose the motors and the motor controller cooperating with the Wixel module myself.
The Rover is controlled from the Windows computer with the arrow keys on the keyboard.

<p align="center">
  <img src="https://github.com/kwark-powabny/WixelRover/blob/main/photos/rover2.JPG" width="350" title="Rover">
</p>

The set consists of two Wixel modules. One is connected to a Windows computer via USB. The wixel_rover_tx.c program is running in the module. It receives control commands from the WindowsController application and transmits it via radio to the second Wixel module. The second Wixel, using the program wixel_rover_rx.c, receives control commands and controls the motors via the DRV8833 driver. 
