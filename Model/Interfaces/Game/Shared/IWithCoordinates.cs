using Core.Game.Objects;

namespace Core.Interfaces.Game.Shared
{
	public interface IWithCoordinates
	{
		Coordinates Cell { get; set; }
	}
}