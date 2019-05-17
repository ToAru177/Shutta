using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Card
    {

        private int _number;

        public int Number { get; set; }
        public bool IsKwang { get; private set; }

        public Card(int number, bool iskwang)
        {
            Number = number;
            IsKwang = iskwang;
        }

        public override string ToString()
        {
            if (IsKwang)
                return Number + "K";
            else
                return Number.ToString();
        }
    }
}
