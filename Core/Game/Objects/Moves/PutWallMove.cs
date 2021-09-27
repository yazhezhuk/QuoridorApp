using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Moves
{
	public class PutWallMove : Move<Wall>
	{
		public IWallCoordinatesDisplacement PositionDisplacement { get; set; }

		public PutWallMove(IWallCoordinatesDisplacement coordinatesChange)
		{
			PositionDisplacement = coordinatesChange;
		}
	}
}