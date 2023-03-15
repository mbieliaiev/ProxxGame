namespace ProxxGame.Contract
{
    /// <summary>
    /// This interface provides methods for actions a user can make during the game
    /// </summary>
    public interface IProxxEngine
    {
        void Initialize(int width = -1, int height = -1, ICell[] blackHoles = null);

        GameResult MakeStep(int row, int column);

        void MarkAsBlackHole(int row, int column);
    }
}
