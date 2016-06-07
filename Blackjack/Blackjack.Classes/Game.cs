using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Game
    {
        public Hand Dealer;
        public Hand Player;
        public Deck Deck;
        private bool _playerWin;
        private int _cardPosition;
        private bool _gameOver;
        public GameSettings GameSettings;

        public Game(GameSettings settings)
        {
            GameSettings = settings;
            Player = new Hand();
            Dealer = new Hand();
            Deck = new Deck(GameSettings.NumberOfDecks);
            Deck.Shuffle();
            Player.Cards.Add(Deck.Cards[0]);
            Dealer.Cards.Add(Deck.Cards[1]);
            Player.Cards.Add(Deck.Cards[2]);
            Dealer.Cards.Add(Deck.Cards[3]);
            _cardPosition = 4;
        }

        public void Draw(Hand hand)
        {
            hand.Cards.Add(Deck.Cards[_cardPosition]);
            _cardPosition++;
        }
    }
}
