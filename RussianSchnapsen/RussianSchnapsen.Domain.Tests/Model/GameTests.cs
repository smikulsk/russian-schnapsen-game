using FluentAssertions;
using NUnit.Framework;
using RussianSchnapsen.Domain.Model;
using System;
using System.Collections.Generic;

namespace RussianSchnapsen.Domain.Tests.Model
{
    [TestFixture]
    class GameTests
    {
        private static class PlayerTypes
        {
            public static IEnumerable<PlayerType> All = (PlayerType[])Enum.GetValues(typeof(PlayerType));
        }

        private static class CardSuits
        {
            public static IEnumerable<CardSuit> All = (CardSuit[])Enum.GetValues(typeof(CardSuit));
        }

        private Game _testedInstance;

        [SetUp]
        public void SetUp()
        {
            _testedInstance = new Game();
        }

        [Test]
        public void Constructor_WhenGameIsCreated_InitialStausIsNotStarted()
        {
            _testedInstance = new Game();

            _testedInstance.Status.Should().Be(GameStatus.NotStarted);
        }

        [Test]
        public void Constructor_WhenGameIsCreated_InitialTrumpIsNull()
        {
            _testedInstance = new Game();

            _testedInstance.Trump.Should().BeNull();
        }

        [TestCaseSource(typeof(CardSuits),nameof(CardSuits.All))]
        public void SetTrump_WhenGivenCardSuit_SetsTrumpProperty(CardSuit suit)
        {
            _testedInstance.SetTrump(suit);

            _testedInstance.Trump.Should().Be(suit);
        }

        [TestCaseSource(typeof(PlayerTypes), nameof(PlayerTypes.All))]
        public void WithPlayer1_GivenPlayerObject_SetsPlayer1Property(PlayerType playerType)
        {
            var player = new Player(playerType.ToString(), playerType);

            _testedInstance.WithPlayer1(player);

            _testedInstance.Player1.Should().BeSameAs(player);
        }

        [TestCaseSource(typeof(PlayerTypes), nameof(PlayerTypes.All))]
        public void WithPlayer2_GivenPlayerObject_SetsPlayer2Property(PlayerType playerType)
        {
            var player = new Player(playerType.ToString(), playerType);

            _testedInstance.WithPlayer2(player);

            _testedInstance.Player2.Should().BeSameAs(player);
        }

        [TestCaseSource(typeof(PlayerTypes), nameof(PlayerTypes.All))]
        public void WithPlayer3_GivenPlayerObject_SetsPlayer3Property(PlayerType playerType)
        {
            var player = new Player(playerType.ToString(), playerType);

            _testedInstance.WithPlayer3(player);

            _testedInstance.Player3.Should().BeSameAs(player);
        }
    }
}
