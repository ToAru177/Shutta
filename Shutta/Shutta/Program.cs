using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Program
    {
        // 상수는 대게 public으로..
        public const int SeedMoney = 500;
        public const int BattingMoney = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("룰 타입을 선택하세요. (1:Basic, 2:Simple)");
            int input = int.Parse(Console.ReadLine());

            RuleType ruleType = (RuleType)input;


            // 두명의 플레이어 존재
            List<Player> players = new List<Player>();
            for (int i = 0; i < 2; i++)
            {
                if (ruleType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            }

            // player들에게 기본금 지급
            foreach(var player in players)
                player.Money = SeedMoney;

            int round = 1;

            while (true)
            {
                // 한명이 오링되면 게임 종료
                if (IsAnyoneOring(players))
                    break;

                Console.WriteLine($"============= Round {round++} =============");

                Dealer dealer = new Dealer();

                // 학교 출석
                foreach (var player in players)
                {
                    player.Money -= BattingMoney;
                    dealer.PutMoney(BattingMoney);
                }

                // 카드 돌리기
                foreach (var player in players)
                {
                    // 카드를 날려 줌.
                    player.Cards.Clear();

                    for (int i = 0; i < 2; i++)
                        player.Cards.Add(dealer.DrawCard());

                    // 플레이어들의 각 카드 출력
                    Console.WriteLine(player.GetCardText());
                }
                Console.WriteLine("-----------------------------------");


                // 승자 찾기
                Player winner = FindWinner(players);

                // 승자에게 상금 주기
                winner.Money += dealer.GetMoney();

                // 각 플레이어의 돈 출력
                
                foreach (var player in players)
                    Console.Write("\t" + player.Money + "\t");
                Console.WriteLine();
                Console.WriteLine("===================================");
            }

        }

        private static Player FindWinner(List<Player> players)
        {
            int score0 = players[0].CalculateScore();
            int score1 = players[1].CalculateScore();

            if (score0 > score1)
                return players[0];
            // 무승부인 경우 추가하기
            // 학교를 한번더 가기
            else
                return players[1];
        }

        private static bool IsAnyoneOring(List<Player> players)
        {
            foreach(Player player in players)
                if (player.Money == 0)
                    return true;
            return false;
        }
    }
}
