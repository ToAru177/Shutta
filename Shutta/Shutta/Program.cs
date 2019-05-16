using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("플레이어 수를 입력하세요. (2 ~ 5명)");
            int numOfPlayer = int.Parse(Console.ReadLine());
            // 2보다 작거나, 5보다 큰수 입력시 재입력 요구

            Console.WriteLine("기본 소지금을 입력하세요.(500 ~ 2000)");
            int seedMoney = int.Parse(Console.ReadLine());
            // 금액 범위 위반시 재입력 요구

            Console.WriteLine("기본 판돈을 입력하세요.(100 ~ 500)");
            int battingMoney = int.Parse(Console.ReadLine());
            // 금액 범위 위반시 재입력 요구

            Console.WriteLine("룰 타입을 선택하세요. (1:Basic, 2:Simple)");
            int input = int.Parse(Console.ReadLine());
            // 1,2 외에 수 입력시 재입력 요구

            RuleType ruleType = (RuleType)input;


            // 플레이어 생성
            List<Player> players = new List<Player>();
            for (int i = 0; i < numOfPlayer; i++)
            {
                if (ruleType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            }

            // player들에게 기본금 지급
            int num = 1;
            foreach (var player in players)
            {
                player.Money = seedMoney;
                player.ID = string.Format($"player_{num++}");
            }

            Round round = new Round();

            while (true)
            {
                // 한명이 오링되면 게임 종료
                if (IsAnyoneOring(players))
                {
                    MoneySort(players);
                    break;
                }

                round.UpRoundCount();

                Console.WriteLine($"============= Round {round.GetRoundCount()} =============");

                Dealer dealer = new Dealer();

                // 학교 출석
                foreach (var player in players)
                {
                    if (player.Money < battingMoney)
                    {
                        int lackBattingMoney = player.Money;                                            
                        player.Money -= lackBattingMoney;
                        dealer.PutMoney(lackBattingMoney);                        
                    }
                    else
                    {                        
                        player.Money -= battingMoney;
                        dealer.PutMoney(battingMoney);
                    }
                    
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

                // 승자의 승리 횟수 증가
                winner.UpWinnigCount();

                // 승자에게 상금 주기
                winner.Money += dealer.GetMoney();

                // 각 플레이어의 돈 출력
 
                foreach (var player in players)
                    Console.Write("\t"+player.Money);
                Console.WriteLine();
                Console.WriteLine("===================================");
            }

            int cnt = 1;
            foreach (var player in players)
            {
                double rate = ((double)player.GetWinningCount() / (double)round.GetRoundCount()) * 100.0;
                Console.WriteLine($"{cnt++}위 : {player.ID} \t{player.Money} \t{rate.ToString("0.0")}%");
            }

        }

        //  남은 잔액 내림차순 정렬
        private static void MoneySort(List<Player> players)
        {
            players.Sort((Player p1, Player p2) => p2.Money.CompareTo(p1.Money));
        }

        private static Player FindWinner(List<Player> players)
        {
            List<int> score = new List<int>();
            for (int i = 0; i < players.Count(); i++)
                score.Add(players[i].CalculateScore());

            int sameScore = 0;

            int max = score[0];
            for (int i = 0; i < players.Count()- 1; i++)
            {
                if (max == score[i + 1]) sameScore++;

                max = max > score[i + 1] ? max : score[i + 1];               
            }
            
            if (sameScore != 0) return Regame(players);

            for (int i = 0; i < players.Count(); i++)
                if (max == score[i]) return players[i];

            return players[0];
        }

        private static Player Regame(List<Player> players)
        {
            Dealer dealer = new Dealer();
            Console.WriteLine();
            Console.WriteLine("============= 재경기 =============");
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

            return FindWinner(players);
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
