using System;

namespace RussianSchnapsen.Domain.Model.Exceptions
{
    [Serializable]
    public class InvalidCardIndexException : Exception
    {
        public InvalidCardIndexException(int cardIndex)
            : base($"Invalid index ({cardIndex}) for hand")
        {
            CardIndex = cardIndex;
        }

        public int CardIndex { get; }
    }
}