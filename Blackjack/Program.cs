using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Blackjack.Classes;

namespace Blackjack
{
    public class Program
    {
        public static void Main()
        {
            var settings = PopulateSettings();
            var game = new Game(settings);
            Console.WriteLine("Let's play Blackjack!" + Console.Out.NewLine);
            Console.WriteLine("Dealer cards:" + Console.Out.NewLine);
            PrintCards(game.Dealer, true, game.GameSettings.TargetScore);
            Console.WriteLine("Player cards:" + Console.Out.NewLine);
            PrintCards(game.Player, false, game.GameSettings.TargetScore);
            DisplayPlayerOptions(game);
            Console.WriteLine("Press 'y' to play again");
            var exitTrigger = Console.ReadKey().Key.ToString();
            if(exitTrigger.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Clear();
                Main();
            }
        }

        private static void PrintCards(Hand hand, bool isInitialDealerHand, int targetScore, int pause = 1000)
        {
            if (isInitialDealerHand)
            {
                Thread.Sleep(pause);
                Console.WriteLine(hand.Cards.First().ToString());
                Console.WriteLine("???" + Console.Out.NewLine);
            }
            else
            {
                foreach(var card in hand.Cards)
                {
                    Thread.Sleep(pause);
                    Console.WriteLine(card.ToString());
                }
                Console.WriteLine("Score: {0}" + Console.Out.NewLine, hand.GetValue(targetScore).ToString());
            }
        }

        private static GameSettings PopulateSettings()
        {
            var settings = new GameSettings();
            settings.TargetScore = Convert.ToInt32(ConfigurationManager.AppSettings["targetScore"]);
            settings.MaxCardWinOn = Convert.ToBoolean(ConfigurationManager.AppSettings["maxCardWinOn"]);
            settings.MaxCardWin = Convert.ToInt32(ConfigurationManager.AppSettings["maxCardWin"]);
            settings.ShowAllDealerCards = Convert.ToBoolean(ConfigurationManager.AppSettings["showAllDealerCards"]);
            settings.Blackjack = Convert.ToBoolean(ConfigurationManager.AppSettings["blackjack"]);
            settings.Gambling = Convert.ToBoolean(ConfigurationManager.AppSettings["gambling"]);
            settings.StartChips = Convert.ToDecimal(ConfigurationManager.AppSettings["startChips"]);
            settings.Payout = Convert.ToDecimal(ConfigurationManager.AppSettings["payout"]);
            settings.BlackjackPayout = Convert.ToDecimal(ConfigurationManager.AppSettings["blackjackPayout"]);          
            settings.NumberOfDecks = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfDecks"]);
            settings.DealerStandValue = Convert.ToInt32(ConfigurationManager.AppSettings["dealerStandValue"]);
            settings.BetsReturnOnTie = Convert.ToBoolean(ConfigurationManager.AppSettings["betsReturnOnTie"]);
            settings.Insurance = Convert.ToBoolean(ConfigurationManager.AppSettings["insurance"]);
            settings.Surrender = Convert.ToBoolean(ConfigurationManager.AppSettings["surrender"]);
            settings.Splitting = Convert.ToBoolean(ConfigurationManager.AppSettings["splitting"]);
            settings.DoubleDown = Convert.ToBoolean(ConfigurationManager.AppSettings["doubleDown"]);
            return settings;
        }

        private static void DisplayPlayerOptions(Game game)
        {
            if(game.Player.IsBlackJack && game.GameSettings.Blackjack)
            {
                Console.WriteLine("BLACKJACK!");
                Thread.Sleep(1000);
                DrawCard(game, false);
                return;
            }
            else if(game.Player.GetValue(game.GameSettings.TargetScore) == game.GameSettings.TargetScore)
            {
                Console.WriteLine("{0} REACHED!", game.GameSettings.TargetScore);
                Thread.Sleep(1000);
                DrawCard(game, false);
                return;
            }
            else if(game.Player.GetValue(game.GameSettings.TargetScore) > game.GameSettings.TargetScore)
            {
                Console.WriteLine("BUST! YOU LOSE");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                var validInput = false;
                while(!validInput)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Stick (S) or Twist (T)?");
                    var playerCommand = Console.ReadKey().Key.ToString();
                    if (playerCommand.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DrawCard(game, false);
                        return;
                    }
                    else if (playerCommand.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DrawCard(game, true);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input" + Console.Out.NewLine);
                    }
                }
            }
        }

        private static void DrawCard(Game game, bool isPlayer)
        {
            if (isPlayer)
            {
                game.Draw(game.Player);
                Console.Clear();
                Console.WriteLine("Dealer cards:" + Console.Out.NewLine);
                PrintCards(game.Dealer, true, game.GameSettings.TargetScore, 0);
                Console.WriteLine("Player cards:" + Console.Out.NewLine);
                PrintCards(game.Player, false, game.GameSettings.TargetScore);
                DisplayPlayerOptions(game);
                return;
            }
            else
            {
                if (game.Dealer.GetValue(game.GameSettings.TargetScore) >= game.GameSettings.DealerStandValue)
                {
                    Console.WriteLine(Console.Out.NewLine + "Dealer cards:" + Console.Out.NewLine);
                    PrintCards(game.Dealer, false, game.GameSettings.TargetScore);
                }

                var isFirstDealerDraw = true;

                while (game.Dealer.GetValue(game.GameSettings.TargetScore) < game.GameSettings.DealerStandValue)
                {
                    if(isFirstDealerDraw)
                    {
                        Thread.Sleep(1000);
                        isFirstDealerDraw = false;
                    }
                    else
                    {
                        game.Draw(game.Dealer);
                    }

                    Console.Clear();
                    Console.WriteLine("Dealer cards:" + Console.Out.NewLine);
                    PrintCards(game.Dealer, false, game.GameSettings.TargetScore);
                    Thread.Sleep(1000);

                    if(game.Dealer.GetValue(game.GameSettings.TargetScore) >= game.GameSettings.DealerStandValue)
                    {
                        Console.WriteLine("Player cards:" + Console.Out.NewLine);
                        PrintCards(game.Player, false, game.GameSettings.TargetScore, 0);
                    }
                }
                
                if (game.Player.GetValue(game.GameSettings.TargetScore) > 
                    game.Dealer.GetValue(game.GameSettings.TargetScore) && !game.Player.IsBust ||
                    game.Player.GetValue(game.GameSettings.TargetScore) <
                    game.Dealer.GetValue(game.GameSettings.TargetScore) && game.Dealer.IsBust
                    )
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lose!");
                }

                return;
            }
        }
    }
}
