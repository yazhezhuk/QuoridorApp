using Core.Interfaces.Game;

namespace Core.Game.Objects
{
	public class Cell : AbstractGameObject, IWithCoordinates
	{
		public PlayerFigure StandingPlayer { get; set; } = null;
		public Coordinates Coordinates { get; set; }
	}
}