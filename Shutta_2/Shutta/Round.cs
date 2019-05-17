using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Round
    {
        //private static int _roundCount;
        private int _roundCount;

        public void UpRoundCount()
        {
            _roundCount++;
        }

        public int GetRoundCount()
        {
            return _roundCount;
        }
    }
}
