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
		public bool TryPlaceWall(int wallId, int line, int offset, Direction direction);
		public bool PlayerCanReachOppositeSide();
		public bool CanExecuteMove(PlayerFigureMove move);


	}
}
