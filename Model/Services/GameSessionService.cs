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

		public Player GetCurrentPlayer() => _gameSession.Turn % 2 == 0 ? Player.First : Player.Second ;
		public Player GetOpponentPlayer() => GetCurrentPlayer() == Player.First ? Player.Second : Player.First;


		public bool ValidateVictory(Cell targetCell)
		{
			return _gameSession.GameField.Cells
				.Where(cell => cell.Coordinates.X == (_gameSession.Turn % 2 == 0 ? 8 : 0))
				.Contains(targetCell);
		}

	}
}
