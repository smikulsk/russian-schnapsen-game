using FluentAssertions;
using NUnit.Framework;
using RussianSchnapsen.Domain.Model;
using RussianSchnapsen.Domain.Model.Exceptions;
using System;
using System.Linq;

namespace RussianSchnapsen.Domain.Tests.Model
{
    [TestFixture]
    class HandTests
    {
        private Deck _deck;

        private Hand _testedInstance;

        [SetUp]
        public void SetUp()
        {
            _deck = Deck.CreateShuffled();

            _testedInstance = new Hand();
        }

        [Test]
        public void ReceiveCard_WhenMaximumCardCountExceeded_ThrowsAppriopriateExcetion()
        {
            PrepareFullHand();

            Action action = () => _testedInstance.ReceiveCard(_deck.Cards[Hand.MaxNoOfCardsOnHand + 1]);

            action.Should().Throw<MaxNoOfCardsOnHandExceededException>();
        }

        [Test]
        public void ReceiveCard_WhenTheSameCardReceived_ThrowsAppriopriateExcetion()
        {
            var card = new Card(CardRank.Jack, CardSuit.Hearts);
            var theSameCard = new Card(CardRank.Jack, CardSuit.Hearts);

            _testedInstance.ReceiveCard(card);

            Action action = () => _testedInstance.ReceiveCard(theSameCard);

            action.Should().Throw<CardAlreadyReceivedException>()
                .Which.Card.Should().BeEquivalentTo(card);

            action.Should().Throw<CardAlreadyReceivedException>()
                .Which.Card.Should().BeSameAs(theSameCard);
        }

        [Test]
        public void ReceiveCard_WhenDifferentCardsReceived_CardsAreAddedInOrder()
        {
            var card = new Card(CardRank.Jack, CardSuit.Hearts);
            var secondCard = new Card(CardRank.Queen, CardSuit.Clubs);

            _testedInstance
                .ReceiveCard(card)
                .ReceiveCard(secondCard);

            var receivedCards = _testedInstance.Cards;

            receivedCards[0].Should().BeSameAs(card);
            receivedCards[1].Should().BeSameAs(secondCard);
        }

        [Test]
        public void ReceiveCard_WhenCardIsReceived_CardIsAddedAsLast()
        {
            var card = new Card(CardRank.Jack, CardSuit.Hearts);
            var noOfCards = _testedInstance.Cards.Count;

            _testedInstance
                .ReceiveCard(card);

            _testedInstance.Cards.Count.Should().Be(noOfCards + 1);
            _testedInstance.Cards.Last().Should().BeSameAs(card);
        }

        [Test]
        public void PlayCard_WhenIndexExceedingNumberOfCardsInHandProvided_ThrowsAppriopriateExcetion()
        {
            PrepareFullHand();

            var cardIndex = _testedInstance.Cards.Count + 1;

            Action action = () => _testedInstance.PlayCard(cardIndex);

            action.Should().Throw<InvalidCardIndexException>()
                .Which
                .CardIndex.Should().Be(cardIndex);
        }

        [Test]
        public void PlayCard_WhenNegativeCardIndexProvided_ThrowsAppriopriateExcetion()
        {
            PrepareFullHand();

            var cardIndex = -1;

            Action action = () => _testedInstance.PlayCard(cardIndex);

            action.Should().Throw<InvalidCardIndexException>()
                .Which
                .CardIndex.Should().Be(cardIndex);
        }

        [Test]
        public void PlayCard_WhenCorrectIndexProvided_ShouldRemoveAppropriateCard()
        {
            var receivedCards = new[]
            {
                new Card(CardRank.Jack, CardSuit.Hearts),
                new Card(CardRank.Nine, CardSuit.Clubs),
                new Card(CardRank.King, CardSuit.Diamonds),
                new Card(CardRank.Ace, CardSuit.Spades),
            };
            PrepareHandWith(receivedCards);

            var cardIndex = 2;

            var playedCard = _testedInstance.PlayCard(cardIndex);

            playedCard.Should().BeEquivalentTo(receivedCards[cardIndex]);
        }

        [Test]
        public void PlayCard_WhenCorrectIndexProvided_ShouldDecreaseCardCount()
        {
            var receivedCards = new[]
            {
                new Card(CardRank.Jack, CardSuit.Hearts),
                new Card(CardRank.Nine, CardSuit.Clubs),
                new Card(CardRank.King, CardSuit.Diamonds),
                new Card(CardRank.Ace, CardSuit.Spades),
            };
            PrepareHandWith(receivedCards);

            var cardIndex = 2;
            var cardCoundBefore = _testedInstance.Cards.Count;

            _testedInstance.PlayCard(cardIndex);

            var cardCountAfter = _testedInstance.Cards.Count;
            cardCountAfter.Should().Be(cardCoundBefore - 1);
        }

        [Test]
        public void PlayCard_WhenCorrectIndexProvided_ShouldNotContainCard()
        {
            var receivedCards = new[]
            {
                new Card(CardRank.Jack, CardSuit.Hearts),
                new Card(CardRank.Nine, CardSuit.Clubs),
                new Card(CardRank.King, CardSuit.Diamonds),
                new Card(CardRank.Ace, CardSuit.Spades),
            };
            PrepareHandWith(receivedCards);

            var cardIndex = 2;

            var playedCard = _testedInstance.PlayCard(cardIndex);

            _testedInstance.Cards.Should().NotContain(playedCard);
            _testedInstance.Cards.Should().NotContain(receivedCards[cardIndex]);
        }

        private void PrepareFullHand()
        {
            for (int cardNo = 0; cardNo < Hand.MaxNoOfCardsOnHand; ++cardNo)
            {
                _testedInstance.ReceiveCard(_deck.Cards[cardNo]);
            }
        }

        private void PrepareHandWith(params Card[] cards)
        {
            foreach (var card in cards)
                _testedInstance.ReceiveCard(card);
        }
    }
}
