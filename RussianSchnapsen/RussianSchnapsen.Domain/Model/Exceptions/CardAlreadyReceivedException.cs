using System;
using System.Runtime.Serialization;

namespace RussianSchnapsen.Domain.Model.Exceptions
{
    [Serializable]
    public class CardAlreadyReceivedException : Exception
    {
        public CardAlreadyReceivedException(Card card)
            : base($"Card ({card.Rank},{card.Suit}) has been already added to hand.")
        {
            Card = card;
        }


        public Card Card { get; }
    }
}