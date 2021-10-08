using System.Collections.Generic;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;

namespace Core.Interfaces.GameServices
{
	public interface IWallService
	{
		List<PlayerFigureMove> MovesBlockedByWall(Wall wall);
		List<Wall> GetAllWalls();

		void AddWall(Wall wall);
		void RemoveWall(Wall wall);
	}
}
