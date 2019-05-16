using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Round 
    {
        public int winningCount;

        public int roundCount;

        public void uproundCount()
        {
            roundCount++;
        }

        public int Getcount()
        {
            return roundCount;
        }

        public double WinningRate()
        {

            return (winningCount / roundCount);
        }


       
      
    }
}
