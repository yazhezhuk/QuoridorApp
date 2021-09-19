using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;

namespace Core.Game.Objects
{ 
	public class Wall : AbstractGameObject, IWall
	{
		public WallDirection WallDirection { get; set; } = WallDirection.None;
		
		public int OffsetFromEdge { get; set; }
		
		public int Line { get; set; }
		
		public Coordinates BeginCoordinates 
		{
			get
			{
				switch (WallDirection)
				{
					case WallDirection.Horizontal:
						return new Coordinates(OffsetFromEdge, Line + 1);
					case WallDirection.Vertical:
						return new Coordinates( Line + 1, OffsetFromEdge);
				}
				return null;

			} 
			 }

		public Coordinates EndCoordinates
		{
			get
			{
				switch (WallDirection)
				{
					case WallDirection.Horizontal:
						return new Coordinates(OffsetFromEdge + IWall.WallSize, Line + 1);
					case WallDirection.Vertical:
						return new Coordinates(Line + 1, OffsetFromEdge + IWall.WallSize);
				}
				return null;
			}
		}
	}
}