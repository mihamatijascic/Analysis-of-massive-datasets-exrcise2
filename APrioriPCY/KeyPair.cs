using System;
using System.Collections.Generic;
using System.Text;

namespace APrioriPCY
{
    public class KeyPair
    {
        public int FirstKey { get; }
        public int SecondKey { get; }

        public KeyPair(int firstKey, int secondKey)
        {
            this.FirstKey = firstKey;
            this.SecondKey = secondKey;
        }

        public int GetGreater()
        {
            return FirstKey > SecondKey ? FirstKey : SecondKey;
        }

        public int GetSmaller()
        {
            return FirstKey < SecondKey ? FirstKey : SecondKey;
        }

        protected bool Equals(KeyPair other)
        {
            return (FirstKey == other.FirstKey && SecondKey == other.SecondKey 
                || FirstKey == other.SecondKey && SecondKey == other.FirstKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyPair)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.GetSmaller(), this.GetGreater());
        }
    }
}
