using Core.Game.Types;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{ 
	public class Wall : AbstractGameObject, IWithCoordinates, IWall
	{
		public WallDirection WallDirection { get; set; } = WallDirection.None;
		
		public Coordinates Coordinates { get; set; }
	}
}