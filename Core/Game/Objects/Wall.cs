using Core.Game.Types;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{ 
	public class Wall : AbstractGameObject, IWithCoordinates, IWall
	{
		public WallDirection WallDirection { get; }

		public int X { get; set; }
		public int Y { get; set; }
	}
}