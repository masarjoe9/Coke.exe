//Imports
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Coke
{
    //Main Form
    public partial class Form1 : Form
    {
        //Create the form and then center it
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        //No Button Click
        private void noButton_Click(object sender, EventArgs e)
        {
            //If they dont want to play just close the app
            Close();
        }

        //Import opening of CD Dlls
        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendStringA(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        //Yes button Click
        private void yesButton_Click(object sender, EventArgs e)
        {
            Boolean hasCD = false;

            //Get Drive Types
            DriveInfo[] dr = System.IO.DriveInfo.GetDrives();

            //Find which drive is the CD Drive
            //lets hope they dont have two or they will both open :) extra support for that awesome coke they will enjoy??
            foreach (DriveInfo d in dr)
            {
                //Look for CDdrive
                if (d.DriveType == DriveType.CDRom)
                {
                    //Tell me you do have a CD
                    hasCD = true;

                    //Set and open the CD drive
                    mciSendStringA("open " + d.Name + ": type CDaudio alias drive" + d.Name , "", 0, 0);
                    mciSendStringA("set drive" + d.Name + " door open", "", 0, 0);
                }
            }
            //Show a message depending drives installed
            if (hasCD)
            {
                //Show message for having a CD drive
                string message = "Here is a cup holder";
                string caption = "Hey!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            else
            {
                //Show message for not having a CD drive
                string message = "Your computer does not have a CDRom Drive for me to open, but if it did it would be open right now";
                string caption = "Hey!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }
    }
}
