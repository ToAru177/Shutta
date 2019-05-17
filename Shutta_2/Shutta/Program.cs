using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{

    partial class Program
    {
        public const int NumOfMinPlayer = 2;
        public const int NumOfMaxPlayer = 5;

        public const int NumOfMinSeedMoney = 1000;
        public const int NumOfMaxSeedMoney = 10000;

        public const int NumOfMinBattingMoney = 100;
        public const int NumOfMaxBattingMoney = 1000;

        static void Main(string[] args)
        {
            // 인트로 출력
            IntroShutta();

            int numOfPlayer = 0;
            int seedMoney = 0;
            int battingMoney = 0;
            int ruleTypeInput = 0;

            int menuNum = 1;
            while (true)
            {
                if (menuNum == 1)
                {
                    // 플레이어수 입력
                    numOfPlayer = InputNumOfPlayer();
                    // 범위 위반시 재입력 요구
                    if (numOfPlayer < NumOfMinPlayer || numOfPlayer > NumOfMaxPlayer)
                    {
                        Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                        Console.WriteLine();
                        menuNum = 1;
                    }
                    else
                        menuNum = 2;
                }

                else if (menuNum == 2)
                {
                    // 기본 소지금 입력
                    seedMoney = InputSeedMoney();

                    // 범위 위반시 재입력 요구
                    if (seedMoney < NumOfMinSeedMoney || seedMoney > NumOfMaxSeedMoney)
                    {
                        Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                        Console.WriteLine();
                        menuNum = 2;
                    }
                    else
                        menuNum = 3;

                }

                else if (menuNum == 3)
                {
                    // 판돈 입력
                    battingMoney = InputBattingMoney();

                    // 범위 위반시 재입력 요구
                    if (battingMoney < NumOfMinBattingMoney || battingMoney > NumOfMaxBattingMoney)
                    {
                        Console.WriteLine("입력 범위를 넘었습니다. 다시 입력하세요.");
                        Console.WriteLine();
                        menuNum = 3;
                    }

                    else
                        menuNum = 4;

                }

                else if (menuNum == 4)
                {
                    // 룰 타입 입력
                    ruleTypeInput = InputRuleType();

                    // 1,2 외에 수 입력시 재입력 요구
                    if (ruleTypeInput != 1 && ruleTypeInput != 2)
                    {
                        Console.WriteLine("존재 하지 않은 룰 타입 입니다. 다시 입력하세요.");
                        Console.WriteLine();
                        menuNum = 4;

                    }
                    else
                        menuNum = 5;
                }

                else
                    break;

            }


            RuleType ruleType = (RuleType)ruleTypeInput;


            // 플레이어 생성
            List<Player> players = CreatePlayers(numOfPlayer, ruleType);


            // player 초기 설정
            InitiatePlayers(players, seedMoney, battingMoney);


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

                Console.Clear();

                Console.WriteLine($"=================================== Round {round.GetRoundCount()} ===================================");

                Dealer dealer = new Dealer();

                // 학교 출석
                SchoolAttendance(players, dealer);

                // 카드 돌리기
                DivideCards(players, dealer);



                // 승자 찾기
                Player winner = FindWinner(players, dealer);
                Console.WriteLine($"{winner.ID.PadLeft(38)} 승!!");

                // 승자의 승리 횟수 증가
                winner.UpWinnigCount();

                // 승자에게 상금 주기
                winner.Money += (dealer.GetMoney());

                Console.WriteLine();
                Console.WriteLine("===============================================================================");

                // 각 플레이어의 정보 출력
                // "player.ID player.Money(소지금) rate(승률)"
                PrintingPlayersInfo(players, round);

            }

            // 순위 출력
            PrintingRanking(players, round);

            Console.ReadKey();

        }

    }
}
