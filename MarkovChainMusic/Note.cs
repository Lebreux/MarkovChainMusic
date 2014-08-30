using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    class Note : IEquatable<Note>
    {
        public int RelValueToTonic { get; private set; }
        public NoteLengths NoteLength { get; private set; }

        public Note(int relValueToTonic, NoteLengths noteLength)
        {
            this.RelValueToTonic = relValueToTonic;
            this.NoteLength = noteLength;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.RelValueToTonic.ToString(), this.NoteLength.ToString());
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + RelValueToTonic.GetHashCode();
                hash = hash * 23 + NoteLength.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Note);
        }

        public bool Equals(Note other)
        {
            return other != null && this.GetHashCode() == other.GetHashCode();
        }
    }
}
