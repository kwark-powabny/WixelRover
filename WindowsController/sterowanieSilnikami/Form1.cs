using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sterowanieSilnikami
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort port;
        bool alreadyPressed; // the flag protects multiple executing when a key is long pressed
        public Form1()
        {
            InitializeComponent();

            alreadyPressed = false;
            
            // Choose the port name and the baud rate.
            port = new System.IO.Ports.SerialPort("COM19", 9600);
            // Connect to the port.
            port.Open();

            // Form that has a button on it
            button1.PreviewKeyDown += new PreviewKeyDownEventHandler(button1_PreviewKeyDown);
            button1.KeyDown += new KeyEventHandler(button1_KeyDown);

            //button1.PreviewKeyUp += new PreviewKeyDownEventHandler(button1_PreviewKeyUp);
            button1.KeyUp += new KeyEventHandler(button1_KeyUp);
        }

        // By default, KeyDown does not fire for the ARROW keys 
        void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!alreadyPressed)
            {
                alreadyPressed = true; // zapobieganie wielokrotnemu wywołaniu
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        label1.Text = "do tyłu";
                        robotBackward();
                        break;
                    case Keys.Up:
                        label1.Text = "do przodu";
                        robotForward();
                        break;
                    case Keys.Right:
                        label1.Text = "w prawo";
                        robotRight();
                        break;
                    case Keys.Left:
                        label1.Text = "w lewo";
                        robotLeft();
                        break;
                    case Keys.Space:
                        label1.Text = "spacja";
                        break;
                }
            }
        }

        // 
        void button1_KeyUp(object sender, KeyEventArgs e)
        {
            alreadyPressed = false; // zapobieganie wielokrotnemu wywołaniu
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Right:
                case Keys.Left:
                case Keys.Space:
                    label1.Text = "-----";
                    robotStop();
                    break;
            }
        }


        // PreviewKeyDown is where you preview the key. 
        // Do not put any logic here, instead use the 
        // KeyDown event after setting IsInputKey to true. 
        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Right:
                case Keys.Left:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void button1_PreviewKeyUp(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Right:
                case Keys.Left:
                    e.IsInputKey = true;
                    break;
            }
        }

        /// <summary>
        /// wysłanie komendy do Wixela
        /// </summary>
        /// <param name="command">komenda</param>
        /// <param name="commandParam">parametr komendy</param>
        void sendCommand(byte command, byte commandParam)
        {
            //port.Write(new byte[] { command, commandParam }, 0, 2);  
            //TODO - dostosować wireless_adc_tx.c
            // przesłanie komendy. Znaczenie kolejnych bajtów: długość pakietu, nr seryjny Wixela docelowego, komenda, parametr
            port.Write(new byte[] {7, 1, 2, 3, 4, command, commandParam }, 0, 7);

            System.Threading.Thread.Sleep(10);
            // odpowiedź Wixela po przyjęciu komendy
            byte[] buffer = new byte[1000];
            port.Read(buffer, 0, 1000);

            usbResponseTb.Text += System.Text.Encoding.Default.GetString(buffer);
            //usbResponseTb.Text = "";
            //usbResponseTb.Text = System.Text.Encoding.Default.GetString(buffer);
            // przewinięcie boxu, żeby był widoczny koniec tekstu
            usbResponseTb.SelectionStart = usbResponseTb.Text.Length;
            usbResponseTb.ScrollToCaret();
        }

        /// <summary>
        /// zatrzymanie robota
        /// </summary>
        void robotStop()
        {
            sendCommand(1, 0); 
        }

        /// <summary>
        /// robot naprzód
        /// </summary>
        void robotForward()
        {
            sendCommand(1, (byte) gearForwardNumberUpDown.Value);
        }

        /// <summary>
        /// robot w tył
        /// </summary>
        void robotBackward()
        {
            sendCommand(2, (byte)gearBackwardNumberUpDown.Value);
        }

        /// <summary>
        /// robot w prawo
        /// </summary>
        void robotRight()
        {
            sendCommand(4, 3);
        }

        /// <summary>
        /// robot w lewo
        /// </summary>
        void robotLeft()
        {
            sendCommand(3, 3);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            port.Close();
        }

    }
}
