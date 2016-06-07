using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Deck
    {
        public Card[] Cards;
        public int NumberOfDecks {get; set;}
        public string[] Numbers = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        public Deck(int numberOfDecks)
        {
            NumberOfDecks = numberOfDecks;
            Shuffle();
        }

        public void Shuffle()
        {
            Cards = new Card[52 * NumberOfDecks];
            for (int i = 0; i < NumberOfDecks; i++)
            {
                PopulateDeck(i);
            }

            var random = new Random();

            for (int n = Cards.Length - 1; n > 0; --n)
            {
                var k = random.Next(n + 1);
                var temp = Cards[n];
                Cards[n] = Cards[k];
                Cards[k] = temp;
            }
        }

        private void PopulateDeck(int deckNumber = 0)
        {
            var arrayIncrement = deckNumber * 52;

            Cards[0 + arrayIncrement] = new Card(Suit.Spades, "A");
            Cards[13 + arrayIncrement] = new Card(Suit.Hearts, "A");
            Cards[26 + arrayIncrement] = new Card(Suit.Clubs, "A");
            Cards[39 + arrayIncrement] = new Card(Suit.Diamonds, "A");
            Cards[10 + arrayIncrement] = new Card(Suit.Spades, "J");
            Cards[23 + arrayIncrement] = new Card(Suit.Hearts, "J");
            Cards[36 + arrayIncrement] = new Card(Suit.Clubs, "J");
            Cards[49 + arrayIncrement] = new Card(Suit.Diamonds, "J");
            Cards[11 + arrayIncrement] = new Card(Suit.Spades, "Q");
            Cards[24 + arrayIncrement] = new Card(Suit.Hearts, "Q");
            Cards[37 + arrayIncrement] = new Card(Suit.Clubs, "Q");
            Cards[50 + arrayIncrement] = new Card(Suit.Diamonds, "Q");
            Cards[12 + arrayIncrement] = new Card(Suit.Spades, "K");
            Cards[25 + arrayIncrement] = new Card(Suit.Hearts, "K");
            Cards[38 + arrayIncrement] = new Card(Suit.Clubs, "K");
            Cards[51 + arrayIncrement] = new Card(Suit.Diamonds, "K");

            for (int i = 1; i < 10; i++)
            {
                Cards[i + arrayIncrement] = new Card(Suit.Spades, (i + 1).ToString());
                Cards[i + 13 + arrayIncrement] = new Card(Suit.Hearts, (i + 1).ToString());
                Cards[i + 26 + arrayIncrement] = new Card(Suit.Clubs, (i + 1).ToString());
                Cards[i + 39 + arrayIncrement] = new Card(Suit.Diamonds, (i + 1).ToString());
            }
        }
    }
}
