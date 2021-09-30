using Core.Game.Objects;

namespace Core.Interfaces.Game.Shared
{
	public interface IWithCoordinates
	{
		Coordinates Coordinates { get; set; }
	}
}