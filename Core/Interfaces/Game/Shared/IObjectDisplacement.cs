using Core.Game.Objects;

namespace Core.Interfaces.Game.Shared
{
	public interface IObjectDisplacement
	{
		Coordinates NewCoordinates { get; set; }
	}
}