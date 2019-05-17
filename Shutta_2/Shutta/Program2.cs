using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    partial class Program
    {
        // 플레이어들 생성
        private static List<Player> CreatePlayers(int numOfPlayer, RuleType ruleType)
        {
            var players = new List<Player>();
            for (int i = 0; i < numOfPlayer; i++)
            {
                if (ruleType == RuleType.Basic)
                    players.Add(new BasicPlayer());
                else
                    players.Add(new SimplePlayer());
            }

            return players;
        }

        // 플레이어들 초기화
        private static void InitiatePlayers(List<Player> players, int seedMoney, int battingMoney)
        {
            int idNum = 1;
            foreach (var player in players)
            {
                player.Money = seedMoney;
                player.BattingMoney = battingMoney;
                player.ID = string.Format($"player_{idNum++}");
            }
        }

        // 학교 출석
        private static void SchoolAttendance(List<Player> players, Dealer dealer)
        {
            foreach (var player in players)
            {

                if (player.Money < player.BattingMoney)
                {
                    int lackBattingMoney = player.Money;
                    player.Money -= lackBattingMoney;
                    dealer.PutMoney(lackBattingMoney);
                    //stackmoney += lackBattingMoney;
                }
                else
                {
                    player.Money -= player.BattingMoney;
                    dealer.PutMoney(player.BattingMoney);
                    //stackmoney += player.BattingMoney;
                }

            }
        }


        //  남은 잔액 내림차순 정렬
        private static void MoneySort(List<Player> players)
        {
            players.Sort((Player p1, Player p2) => p2.Money.CompareTo(p1.Money));
        }

        // 승자 찾기
        private static Player FindWinner(List<Player> players, Dealer dealer)
        {
            List<int> score = new List<int>();
            for (int i = 0; i < players.Count(); i++)
                score.Add(players[i].CalculateScore());

            int sameScore = 0;

            int max = score[0];
            for (int i = 0; i < players.Count() - 1; i++)
            {
                if (max == score[i + 1]) sameScore++;

                max = max > score[i + 1] ? max : score[i + 1];
            }

            if (sameScore != 0) return Regame(players, dealer);

            for (int i = 0; i < players.Count(); i++)
                if (max == score[i]) return players[i];

            return players[0];
        }

        // 무승부 재경기
        private static Player Regame(List<Player> players, Dealer dealer)
        {
            //Dealer dealer = new Dealer();
            Console.WriteLine();
            Console.WriteLine("=================================== 재 경기 ===================================");
            // 카드 돌리기
            DivideCards(players, dealer);
            Console.WriteLine("-------------------------------------------------------------------------------");

            //dealer.PutMoney(money);
            return FindWinner(players, dealer);
        }

        // 오링 여부
        private static bool IsAnyoneOring(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money == 0)
                    return true;
            return false;
        }

        // 카드 돌리기
        private static void DivideCards(List<Player> players, Dealer dealer)
        {
            foreach (var player in players)
            {
                // 카드를 날려 줌.
                player.Cards.Clear();

                for (int i = 0; i < 2; i++)
                    player.Cards.Add(dealer.DrawCard());

                // 플레이어들의 각 카드 출력
                Console.WriteLine();
                Console.Write($"[{player.ID}]의 패는 ");
                Console.Write(player.GetCardText() + "입니다. ");

                int score = player.CalculateScore();

                if (score >= 0 && score <= 9)
                    Console.WriteLine($"[ {score}끗 ]");

                else if (score >= 11 && score <= 20)
                {
                    if (score == 20)
                        Console.WriteLine($"[ 장땡 ]");

                    Console.WriteLine($"[ {score % 10}땡 ]");
                }

                else if (score > 20 && score < 200)
                    Console.WriteLine($"[ {score % 10}광땡 ]");

                else
                    Console.WriteLine($"[ {score}점 ]");
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------------------------");

                Console.WriteLine();
                Console.Write($"{player.ID} 콜(1)/다이(2) 선택 : ");
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
                Console.WriteLine("===============================================================================");


            }
        }

        // 플레이어들 정보 출력
        private static void PrintingPlayersInfo(List<Player> players, Round round)
        {
            foreach (var player in players)
            {
                double rate = ((double)player.GetWinningCount() / (double)round.GetRoundCount()) * 100.0;
                Console.WriteLine($"{player.ID} {player.Money.ToString("n0").PadLeft(15)} {rate.ToString("0.0").PadLeft(8)}%".PadLeft(56));
            }
            Console.ReadKey();
        }

        // 순위 출력
        private static void PrintingRanking(List<Player> players, Round round)
        {
            Console.Clear();
            Console.WriteLine("================================== 최종 순위 ==================================");
            Console.WriteLine();
            int cnt = 1;
            foreach (var player in players)
            {
                double rate = ((double)player.GetWinningCount() / (double)round.GetRoundCount()) * 100.0;
                Console.WriteLine($"{cnt++}위 : {player.ID} \t{player.Money.ToString("n0").PadLeft(15)} \t{rate.ToString("0.0").PadLeft(8)}%".PadLeft(54));
            }
            Console.WriteLine();
            Console.WriteLine("===============================================================================");

        }

        // 룰 타입 입력
        private static int InputRuleType()
        {
            int ruleTypeInput;

            Console.Write("4. 룰 타입을 선택하세요. (1:Basic, 2:Simple) : ");
            ruleTypeInput = int.Parse(Console.ReadLine());

            return ruleTypeInput;
        }

        // 판돈 입력
        private static int InputBattingMoney()
        {
            int battingMoney;

            Console.Write($"3. 기본 판돈을 입력하세요.({NumOfMinBattingMoney.ToString("n0")} ~ {NumOfMaxBattingMoney.ToString("n0")}) : ");
            battingMoney = int.Parse(Console.ReadLine());

            return battingMoney;
        }

        // 기본 소지금 입력
        private static int InputSeedMoney()
        {
            int seedMoney;

            Console.Write($"2. 기본 소지금을 입력하세요.({NumOfMinSeedMoney.ToString("n0")} ~ {NumOfMaxSeedMoney.ToString("n0")}) : ");
            seedMoney = int.Parse(Console.ReadLine());

            return seedMoney;
        }

        // 플레이어수 입력
        private static int InputNumOfPlayer()
        {
            int numOfPlayer;

            Console.Write("1. 플레이어 수를 입력하세요. (2 ~ 5명) : ");
            numOfPlayer = int.Parse(Console.ReadLine());

            return numOfPlayer;
        }

        // 인트로 출력
        private static void IntroShutta()
        {
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("//\t\t\t\t\t\t\t\t\t   //");
            Console.WriteLine("//\t\t\t    ☆C#을 이용한 섯다☆\t\t\t   //");
            Console.WriteLine("//\t\t\t\t\t\t\t\t\t   //");
            Console.WriteLine("//\t1.게임 시작: 플레이어 수, 기본 소지금, 판돈, 룰타입 선택\t   //");
            Console.WriteLine("//\t2.무승부의 경우: 추가 판돈 없이 카드 재배부\t\t\t   //");
            Console.WriteLine("//\t3.오링의 경우: 한 명 오링시 게임종료 후 순위출력\t\t   //");
            Console.WriteLine("//\t4.무승부의 경우: 추가 판돈 없이 카드 재배부\t\t\t   //");
            Console.WriteLine("//\t5.배팅의 경우: 플레이어가 선택하여 배팅 가능\t\t\t   //");
            Console.WriteLine("//\t6.무승부의 경우: 추가 판돈 없이 카드 재배부\t\t\t   //");
            Console.WriteLine("//\t7.콜/다이의 선택: 패를 받고 콜 or 다이 선택 가능\t\t   //");
            Console.WriteLine("//\t\t\t\t\t\t\t\t\t   //");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////");

        }
    }

}

