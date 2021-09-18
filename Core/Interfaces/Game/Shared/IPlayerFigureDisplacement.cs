using Core.Game.Objects;

namespace Core.Interfaces.Game.Shared
{
	public interface IPlayerFigureDisplacement : IObjectDisplacement
	{
		Coordinates FromCoordinates { get; set; }
	}
}