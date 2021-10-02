using System.Collections.Generic;
using System.Linq;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.GameStates;

namespace Core.Services
{
	public class WallService
	{
		private readonly GameSession _gameSession;


		public List<Wall> GetAllWalls() => _gameSession.GameField.Walls;
		public void AddWall(Wall wall) => _gameSession.GameField.Walls.Add(wall);
		public void RemoveWall(Wall wall) => _gameSession.GameField.Walls.Remove(wall);


		public WallService(GameSession gameSession)
		{
			_gameSession = gameSession;
		}

		public List<PlayerFigureDisplacement> GetWallBlockedDisplacement(Wall wall) =>
			wall.GetBlockedDisplacements();



	}
}
