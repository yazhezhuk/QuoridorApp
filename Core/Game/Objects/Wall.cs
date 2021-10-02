using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;

namespace Core.Game.Objects
{ 
	public class Wall : GameObject
	{
		public const int WallSize = 1;

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
						return new Coordinates(OffsetFromEdge, Line);
					case WallDirection.Vertical:
						return new Coordinates( Line, OffsetFromEdge);
				}
				return new Coordinates();

			} 
			 }

		public Coordinates EndCoordinates
		{
			get
			{
				switch (WallDirection)
				{
					case WallDirection.Horizontal:
						return new Coordinates(OffsetFromEdge  + WallSize , Line);
					case WallDirection.Vertical:
						return new Coordinates(Line, OffsetFromEdge + WallSize);
				}
				return new Coordinates();
			}
		}
	}
}