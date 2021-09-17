using Core.Game.Objects;
using Core.Interfaces;
using Core.Interfaces.Game;

namespace Core.GameObjects
{
	public class Cell : AbstractGameObject, IWithCoordinates
	{
		public Player StandingPlayer { get; set; }
		
		public int X { get; set; }
		public int Y { get; set; }
	}
}