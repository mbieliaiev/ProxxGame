namespace ProxxGame.Contract
{
    public interface ICellTableParametersPicker
    {
        void PickParameters(out int width, out int height, out int numberBlackHoles);
    }
}
