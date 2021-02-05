using System.Collections.Generic;

namespace RussianSchnapsen.Domain.Model
{
    public class Hand
    {
        private List<Card> _cards = new List<Card>();

        public static readonly int MaxNoOfCardsOnHand = 8;

        public Hand ReceiveCard(Card card)
        {
            if (_cards.Count == MaxNoOfCardsOnHand)
                throw new Exceptions.MaxNoOfCardsOnHandExceededException();

            if (_cards.Contains(card))
                throw new Exceptions.CardAlreadyReceivedException(card);

            _cards.Add(card);

            return this;
        }
        
        public IReadOnlyList<Card> Cards => _cards;

        public Card PlayCard(int cardIndex)
        {
            if (cardIndex < 0 || cardIndex >= _cards.Count)
                throw new Exceptions.InvalidCardIndexException(cardIndex);

            var playedCard = _cards[cardIndex];
            _cards.RemoveAt(cardIndex);

            return playedCard;
        }
    }
}
