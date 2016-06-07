using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class GameSettings
    {
        public int TargetScore { get; set; }
        public bool MaxCardWinOn { get; set; }
        public int MaxCardWin { get; set; }
        public bool ShowAllDealerCards { get; set; }
        public bool Blackjack { get; set; }
        public bool Gambling { get; set; }
        public decimal StartChips { get; set; }
        public decimal Payout { get; set; }
        public decimal BlackjackPayout { get; set; }
        public int NumberOfDecks { get; set; }
        public int DealerStandValue { get; set; }
        public bool BetsReturnOnTie { get; set; }
        public bool Insurance { get; set; }
        public bool Surrender { get; set; }
        public bool Splitting { get; set; }
        public bool DoubleDown { get; set; }
    }
}
