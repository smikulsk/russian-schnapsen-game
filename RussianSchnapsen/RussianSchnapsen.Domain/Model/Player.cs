using System;
using System.Collections.Generic;
using System.Text;

namespace RussianSchnapsen.Domain.Model
{
    public class Player
    {
        private Hand hand = new Hand();

        public Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public PlayerType Type { get; }

        public Player ReceiveCard(Card card)
        {
            hand.ReceiveCard(card);
            return this;
        }

        public Card PlayCard(int cardIndex)
        {
            return hand.PlayCard(cardIndex);
        }
    }
}
