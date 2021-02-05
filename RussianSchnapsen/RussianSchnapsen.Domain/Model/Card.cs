using System;

namespace RussianSchnapsen.Domain.Model
{
    public class Card : IEquatable<Card>
    {
        
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank { get; }

        public CardSuit Suit { get; }

        public bool Equals(Card other)
        {
            if (other == null) return false;
            return (this.Rank.Equals(other.Rank) && this.Suit.Equals(other.Suit));
        }
    }
}
