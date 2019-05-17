using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shutta.UniTest
{
    [TestClass]
    public class DealerTest
    {
        [TestMethod]
        // 가급적 한글로 메소드 이름 짓는 걸 권장 함.
        public void 스물장의_카드에는_광이_3장_들어있어야_함()
        {
            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();

            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int kwangCount = 0;

            foreach (Card card in cards)
                if (card.IsKwang)
                    kwangCount++;

            // kwangCount에는 3이 들어 있어야 한다.
            Assert.AreEqual(3, kwangCount);
        }

        [TestMethod]
        // 가급적 한글로 메소드 이름 짓는 걸 권장 함.
        public void 스물장의_카드에는_1이_2장_들어있어야_함()
        {
            Dealer dealer = new Dealer();

            List<Card> cards = new List<Card>();

            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.DrawCard();
                cards.Add(card);
            }

            int countOf1 = 0;

            foreach (Card card in cards)
                if (card.Number == 1)
                    countOf1++;

            // kwangCount에는 3이 들어 있어야 한다.
            Assert.AreEqual(2, countOf1);
        }
    }
}
