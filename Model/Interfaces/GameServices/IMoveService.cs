using System.Collections.Generic;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.Game;

namespace Core.Interfaces.GameServices
{
	public interface IMoveService
	{
		public ISet<PlayerFigureMove> GetFiguresBlockedDisplacements();
		public List<Cell> GetNotBlockedCellNeighbours(Cell cell);

		public bool ValidatePlayerFigureMove(PlayerFigureMove move);
		public bool MoveIsBlocked(PlayerFigureMove move);
		public ActionResult TryPlaceWall(Wall wall, Direction direction, List<Wall> other);
		public bool PlayerCanReachOppositeSide();
		public ActionResult TryExecuteMove(PlayerFigureMove move);


	}
}
