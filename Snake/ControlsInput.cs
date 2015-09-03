using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class ControlsInput
    {
        //Load list of available keyboard buttons.
        private static Hashtable keyTable = new Hashtable();

        //Perform a check to see if a button is pressed.
        public static bool KeyPressed(Keys key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }

            return (bool) keyTable[key];
        }

        //Detects if a keyboard button is pressed.
        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
