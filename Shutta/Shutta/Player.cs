using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public abstract class Player
    {
        public Player()
        {
            Cards = new List<Card>();
        }

        //기ㅓ미ㅓ이ㅏㄻ너ㅏㅣㅓㅂ재ㅔㅑ더

        // Property
        public List<Card> Cards { get; }

        public int Money { get; set; }

        // 점수 계산 method
        public abstract int CalculateScore();

        public string GetCardText()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var card in Cards)
            {
                builder.Append("\t" + card.ToString() + "\t");
            }

            return builder.ToString();
        }

        
    }
}
