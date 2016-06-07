using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Card
    {
        protected Suit _suit;
        public string Cardvalue;

        public Card(Suit suit, string cardvalue)
        {
            _suit = suit;
            Cardvalue = cardvalue;
        }
        public override string ToString()
        {
            return string.Format("{0} of {1}", Cardvalue, _suit);
        }
    }
}
