using System.Collections.Generic;
using System.Linq;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class WallService : IWallService
	{
		private readonly GameSession _gameSession;

		public WallService(GameSession gameSession)
		{
			_gameSession = gameSession;
		}

		public List<PlayerFigureMove> MovesBlockedByWall(Wall wall) => wall.GetBlockedDisplacements();
		public List<Wall> GetAllWalls() => _gameSession.GameField.Walls;
		public void AddWall(Wall wall) => _gameSession.GameField.Walls.Add(wall);
		public void RemoveWall(Wall wall) => _gameSession.GameField.Walls.Remove(wall);
	}
}
