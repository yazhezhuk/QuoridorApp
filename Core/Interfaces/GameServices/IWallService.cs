using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Interfaces.GameServices
{
	public interface IWallService
	{
		bool CanPlaceAWall(Coordinates startCoordinates, WallDirection direction);
	}
}