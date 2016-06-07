using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Hand
    {
        public List<Card> Cards = new List<Card>();
        public bool IsBust = false;
        public bool IsBlackJack = false;

        public int GetValue(int targetScore)
        {
            var total = 0;
            var acesInHand = 0;

            foreach (var card in Cards)
            {
                int number;
                if (int.TryParse(card.Cardvalue, out number))
                {
                    total += number;
                }
                else if (card.Cardvalue == "A")
                {
                    acesInHand += 1;
                    total += 11;
                }
                else
                {
                    total += 10;
                }
            }

            IsBlackJack = false;
            if (Cards.Count == 2 && total == targetScore)
            {
                IsBlackJack = true;
            }

           
            while (acesInHand > 0)
            {
                if (total > targetScore)
                {
                    total -= 10;
                    acesInHand--;
                }
                else
                {
                    break;
                }
            }

            if (total > targetScore)
            {
                
                IsBust = true;
            }

            return total;
        }

    }
}
