using SOSGameLogic.Interfaces;

namespace SOSGameLogic.Implementation
{
    public class Engine : IEngine
    {
        private IGenericGameModeLogic _genericGameModeLogic;

        public Engine(IGenericGameModeLogic genericGameModeLogic)
        {
            _genericGameModeLogic = genericGameModeLogic;
        }

        public void MakeMove(int row, int col)
        {
            _genericGameModeLogic.CheckForSOS(row, col);
        }

        public bool HasWinner()
        {
            return _genericGameModeLogic.HasWinner();
        }

        public int GetCurrentPlayerScore()
        {
            return _genericGameModeLogic.GetCurrentPlayerScore();
        }
    }
}
