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

       

        // Property
        public List<Card> Cards { get; }

        public int Money { get; set; }
        public string ID { get; set; }

        // 승리한 횟수
        private int _winningcount;

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

        // _winningcount 증가 메소드
        public void UpWinnigCount()
        {
            _winningcount++;
        }

        public int GetWinningCount()
        {
            return _winningcount;
        }
        
    }
}
