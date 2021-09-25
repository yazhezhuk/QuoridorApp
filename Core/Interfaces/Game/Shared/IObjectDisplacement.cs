using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Interfaces.Game.Shared
{
	public interface IObjectDisplacement
	{
		Coordinates NewCoordinates { get; set; }
		MoveDirection MoveDirection { get; set; }
	}
}