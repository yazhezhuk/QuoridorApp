using System.Linq;
using Core.Game.Objects;
using Core.GameStates;

namespace Core.Services
{
	public class GameSessionService
	{
		public readonly GameSession _gameSession;
		public GameSessionService(GameSession gameSession)
		{
			_gameSession = gameSession;
		}

		public bool ValidateTurn(Player player)
		{
			return _gameSession.Turn % 2 == player.Id;
		}

		public Player GetCurrentPlayer() => _gameSession.CurrentPlayer;


		public bool ValidateVictory(Cell targetCell)
		{
			return _gameSession.GameField.Cells
				.Where(cell => cell.Coordinates.Y == (_gameSession.Turn % 2 == 0 ? 8 : 0))
				.Contains(targetCell);
		}

	}
}
