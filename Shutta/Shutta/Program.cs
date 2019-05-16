using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    
    class Program
    {
        //private int battingMoney;
        static void Main(string[] args)
        {
            /*
            int numOfPlayer;
            int seedMoney;
            int battingMoney;
            int input;

            
            while (true)
            {
                Console.WriteLine("플레이어 수를 입력하세요. (2 ~ 5명)");
                numOfPlayer = int.Parse(Console.ReadLine());

                // 2보다 작거나, 5보다 큰수 입력시 재입력 요구
                if(numOfPlayer < 2 || numOfPlayer > 5)
                {
                    Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("기본 소지금을 입력하세요.(500 ~ 2000)");
                seedMoney = int.Parse(Console.ReadLine());

                // 금액 범위 위반시 재입력 요구
                if(seedMoney < 500 || seedMoney > 2000)
                {
                    Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("기본 판돈을 입력하세요.(100 ~ 500)");
                battingMoney = int.Parse(Console.ReadLine());

                // 금액 범위 위반시 재입력 요구
                if (battingMoney < 100 || battingMoney > 500)
                {
                    Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("룰 타입을 선택하세요. (1:Basic, 2:Simple)");
                input = int.Parse(Console.ReadLine());

                // 1,2 외에 수 입력시 재입력 요구
                if (input != 1 && input != 2)
                {
                    Console.WriteLine("존재 하지 않은 룰 타입 입니다. 다시 입력하세요.");
                    Console.WriteLine();
                    continue;
                }

                break;

            }
            */

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

            // player 초기 설정
            int num = 1;
            foreach (var player in players)
            {
                player.Money = seedMoney;
                player.BattingMoney = battingMoney;
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
                    
                    if (player.Money < player.BattingMoney)
                    {
                        int lackBattingMoney = player.Money;                                            
                        player.Money -= lackBattingMoney;
                        dealer.PutMoney(lackBattingMoney);                        
                    }
                    else
                    {                        
                        player.Money -= player.BattingMoney;
                        dealer.PutMoney(player.BattingMoney);
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
                    Console.WriteLine($"[{player.ID} 패]");
                    Console.WriteLine(player.GetCardText());
                    Console.WriteLine("-----------------------------------");

                    int score = player.CalculateScore();

                    if (score >= 0 && score <= 9)
                        Console.WriteLine($"[ {score}끗 ]");

                    else if (score >= 11 && score <= 20)
                        Console.WriteLine($"[ {score % 10}땡 ]");

                    else if (score > 20 && score < 200)
                        Console.WriteLine($"[ {score % 10}광땡 ]");

                    else
                        Console.WriteLine($"[ {score}점 ]");

                    Console.WriteLine();
                    Console.WriteLine($"{player.ID} 콜(1)/다이(2) 선택.");
                    Console.Write(">>");
                    input = int.Parse(Console.ReadLine());

                    Batting batting = (Batting)input;

                    if (batting == Batting.Call)
                    {
                        if (player.Money < player.BattingMoney)
                        {
                            int lackBattingMoney = player.Money;
                            player.Money -= lackBattingMoney;
                            dealer.PutMoney(lackBattingMoney);
                        }
                        else
                        {
                            player.Money -= player.BattingMoney;
                            dealer.PutMoney(player.BattingMoney);
                        }
                    }

                    else
                    {
                        player.DropCard();
                    }

                    Console.WriteLine();

                }
                Console.WriteLine("-----------------------------------");



                // 승자 찾기
                Player winner = FindWinner(players);
                Console.WriteLine($"\t{winner.ID} 승!!");

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
                Console.WriteLine($"[{player.ID} 패]");
                Console.WriteLine(player.GetCardText());
                Console.WriteLine("-----------------------------------");

                int score = player.CalculateScore();

                if (score >= 0 && score <= 9)
                    Console.WriteLine($"[ {score}끗 ]");

                else if (score >= 11 && score <= 20)
                    Console.WriteLine($"[ {score % 10}땡 ]");
                                         
                else if(score > 20 && score < 200)
                    Console.WriteLine($"[ {score % 10}광땡 ]");
               
                else 
                    Console.WriteLine($"[ {score}점 ]");

                Console.WriteLine();
                Console.WriteLine($"{player.ID} 콜(1)/다이(2) 선택.");
                Console.Write(">>");
                int input = int.Parse(Console.ReadLine());

                Batting batting = (Batting)input;

                if (batting == Batting.Call)
                {
                    if (player.Money < player.BattingMoney)
                    {
                        int lackBattingMoney = player.Money;
                        player.Money -= lackBattingMoney;
                        dealer.PutMoney(lackBattingMoney);
                    }
                    else
                    {
                        player.Money -= player.BattingMoney;
                        dealer.PutMoney(player.BattingMoney);
                    }
                }

                else
                {
                    player.DropCard();
                }

                Console.WriteLine();

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
