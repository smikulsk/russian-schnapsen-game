using System;

namespace RussianSchnapsen.Domain.Model.Exceptions
{
    [Serializable]
    public class MaxNoOfCardsOnHandExceededException : Exception
    {
        public MaxNoOfCardsOnHandExceededException()
            : base("Cannot add more cards to hand.")
        {
        }
    }
}