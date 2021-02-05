using FluentAssertions;
using NUnit.Framework;
using RussianSchnapsen.Domain.Model;
using RussianSchnapsen.Domain.Model.Exceptions;
using System;

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

        private void PrepareFullHand()
        {
            for (int cardNo = 0; cardNo < Hand.MaxNoOfCardsOnHand; ++cardNo)
            {
                _testedInstance.ReceiveCard(_deck.Cards[cardNo]);
            }
        }
    }
}
