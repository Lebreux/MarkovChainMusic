using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class Scale
    {
        public static readonly Scale Whole = new Scale(new int[] { 2 });
        public static readonly Scale Major = new Scale(new int[] { 2, 2, 1, 2, 2, 2, 1 });
        public static readonly Scale NaturalMinor = new Scale(new int[] { 2, 1, 2, 2, 1, 2, 2 });
        
        private int[] _scaleSteps;

        private Scale(int[] scaleSteps)
        {
            this._scaleSteps = scaleSteps;
        }

        public int GetScaleStepsTo(int noteRelativePosition)
        {
            bool pos = noteRelativePosition >= 0;
            int totalSteps = 0;
            for (int i = 0; i < Math.Abs(noteRelativePosition); i++)
            {
                int scalePos = (pos ? i : -i - 1) % this._scaleSteps.Length;
                if (scalePos < 0) scalePos = this._scaleSteps.Length + scalePos;
                int steps = this._scaleSteps[scalePos];
                totalSteps = pos ? totalSteps + steps : totalSteps - steps;
            }
            return totalSteps;
        }
    }
}
