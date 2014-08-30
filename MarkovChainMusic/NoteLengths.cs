using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class NoteLengths
    {
        public static NoteLengths DoubleWhole = new NoteLengths(2);
        public static NoteLengths Whole = new NoteLengths(1);
        public static NoteLengths Half = new NoteLengths(.5f);
        public static NoteLengths Quarter = new NoteLengths(.25f);
        public static NoteLengths Eight = new NoteLengths(.125f);

        public float Value { get; private set; }

        private NoteLengths(float value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
