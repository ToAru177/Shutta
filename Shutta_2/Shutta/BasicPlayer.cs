using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class BasicPlayer : Player
    {
        public override int CalculateScore()
        {
            // 광땡
            if (Cards[0].IsKwang && Cards[1].IsKwang)
                return Cards[0].Number + 20;
            // 땡
            else if (Cards[0].Number == Cards[1].Number)
            {
                if (Cards[0].Number == 0)   // Die 한 경우
                    return 0;

                return Cards[0].Number + 10;    // 11 ~ 20
            }
            // 끗
            else
                return (Cards[0].Number + Cards[1].Number) % 10;    // 0 ~ 9
        }
    }
}
