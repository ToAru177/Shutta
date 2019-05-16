using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Dealer
    {
        #region money
        private int _money;

        public void PutMoney(int bettingMoney)
        {
            _money += bettingMoney;
        }

        public int GetMoney()
        {
            int moneyToReturn = _money;
            _money = 0;

            return moneyToReturn;
        }
        #endregion

        private List<Card> _cards = new List<Card>();

        private int _cardIndex;

        public Card DrawCard()
        {
            //Card card = _cards[_cardIndex];
            //_cardIndex++;
            //return card;

            // C에서만 허용되는 문법
            return _cards[_cardIndex++];
        }

        public Dealer()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    bool isKwang = (j == 0) && (i==1||i==3||i==8);
                    Card card = new Card(i, isKwang);
                    _cards.Add(card);
                }
            }

            // 람다식?
            // 카드를 무작위로 섞기(shuffle)
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
