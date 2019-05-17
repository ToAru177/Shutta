﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class SimplePlayer : Player
    {
        public override int CalculateScore()
        {
            return (Cards[0].Number + Cards[1].Number)*100; // 200~2000
        }
    }
}
