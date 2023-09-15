namespace CountMasters.Game
{
    public static class GameStateManager
    {
        public static GameState GameState => _currentGameState;

        private static GameState _currentGameState;

        public static void SetGameState(GameState gameState)
        {
            _currentGameState = gameState;
            GameStateEvents.GameStateChanged?.Invoke(_currentGameState);
        }
    }
}