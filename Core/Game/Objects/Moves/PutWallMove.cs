using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Moves
{
	public class PutWallMove : Move<Wall>
	{
		public PutWallMove(IWallCoordinatesChange coordinatesChange)
		{
			PositionDisplacement = coordinatesChange;
		}
	}
}