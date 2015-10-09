using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Swipe
    {
        public float xDirectionStart = 0, yDirectionStart = 0;
        public float xDirectionEnd = 0, yDirectionEnd = 0;
        public float xDirectionDelta = 0, yDirectionDelta = 0;
        public bool Tap = false;
        public bool Enabled = false;
        
        public void Reset()
        {
            xDirectionStart = 0; yDirectionStart = 0;
            xDirectionEnd = 0; yDirectionEnd = 0;
            xDirectionDelta = 0; yDirectionDelta = 0;
            Tap = false;
            Enabled = false;
        }
    }
}
