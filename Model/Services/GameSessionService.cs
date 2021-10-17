using System.Linq;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Types;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class GameSessionService : IGameSessionService
	{
		private readonly GameSession _gameSession;
		public GameSessionService(GameSession gameSession)
		{
			_gameSession = gameSession;
		}

		public bool ValidateTurn(Player player)
		{
			return _gameSession.Turn % 2 == (int)_gameSession.CurrentPlayer;
		}

		public Player GetCurrentPlayer() => _gameSession.CurrentPlayer;
		public Player GetOpponentPlayer() => _gameSession.CurrentPlayer == Player.First ? Player.Second : Player.First;


		public bool ValidateVictory(Cell targetCell)
		{
			return _gameSession.GameField.Cells
				.Where(cell => cell.Coordinates.Y == (_gameSession.Turn % 2 == 0 ? 8 : 0))
				.Contains(targetCell);
		}

	}
}