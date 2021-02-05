using System;
using System.Collections.Generic;
using System.Linq;

namespace RussianSchnapsen.Domain.Model
{
    public class Deck
    {
        private const int FirstCard = 0;

        public static readonly int NoOfRanksInSuit = 6;
        public static readonly int NoOfSuits = 4;
        public static readonly int DeckSize = NoOfSuits * NoOfRanksInSuit;

        private readonly Random random = new Random();
        private readonly Card[] _cards;

        private Deck()
        {
            _cards = Enumerable.Range(0, DeckSize)
                .Select(AsCard).ToArray();
        }

        public static Deck CreateShuffled()
        {
            return new Deck().Shuffle();
        }

        public IReadOnlyList<Card> Cards => _cards;

        public Deck Shuffle()
        {
            for (int alreadyShuffledCount = 0; alreadyShuffledCount < DeckSize - 1; ++alreadyShuffledCount)
            {
                var positionInRestOfDeck = random.Next(DeckSize - alreadyShuffledCount);
                SwapCards(offset: alreadyShuffledCount, positionInRestOfDeck);
            }
            return this;
        }

        private static Card AsCard(int positionInDeck)
        {
            var rankIndex = positionInDeck % Deck.NoOfRanksInSuit;
            var suitIndex = positionInDeck / Deck.NoOfRanksInSuit;

            return new Card((CardRank)rankIndex, (CardSuit)suitIndex);
        }

        private void SwapCards(int offset, int positionInRestOfDeck)
        {
            if (positionInRestOfDeck == FirstCard)
                return;

            var tmp = _cards[offset];
            _cards[offset] = _cards[offset + positionInRestOfDeck];
            _cards[offset + positionInRestOfDeck] = tmp;
        }
    }
}
