using ProxxGame.Contract;

namespace ProxxGame.Logics
{
    public class CellTableParametersPicker : ICellTableParametersPicker
    {
        private readonly ICommunicationChannel _communicationChannel;

        public CellTableParametersPicker(ICommunicationChannel communicationChannel) 
        {
            _communicationChannel = communicationChannel;
        }

        public void PickParameters(out int width, out int height, out int numberBlackHoles)
        {
            width = _communicationChannel.ReadIntWithMessage("Input table width:"); ; 
            height = _communicationChannel.ReadIntWithMessage("Input table height:"); ; 
            numberBlackHoles = _communicationChannel.ReadIntWithMessage("Input number of black holes:"); ;
        }
    }
}
