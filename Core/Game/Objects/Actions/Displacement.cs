using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Actions
{
	public class PlayerFigureDisplacement : IPlayerFigureDisplacement
	{
		public PlayerFigureDisplacement(Coordinates from, Coordinates to)
		{
			FromCoordinates = from;
			NewCoordinates = to;
		}
		public Coordinates FromCoordinates { get; set; }
		public Coordinates NewCoordinates { get; set; }
	}
}