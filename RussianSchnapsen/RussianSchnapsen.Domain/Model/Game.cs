namespace RussianSchnapsen.Domain.Model
{
    public class Game
    {
        public GameStatus Status { get; private set; } = GameStatus.NotStarted;

        public CardSuit? Trump { get; private set; }

        public void SetTrump(CardSuit trump)
        {
            Trump = trump;
        }

        public Player Player1 { get; private set; }

        public Game WithPlayer1(Player player)
        {
            Player1 = player;
            return this;
        }

        public Player Player2 { get; private set; }

        public Game WithPlayer2(Player player)
        {
            Player2 = player;
            return this;
        }

        public Player Player3 { get; private set; }

        public Game WithPlayer3(Player player)
        {
            Player3 = player;
            return this;
        }
    }
}
