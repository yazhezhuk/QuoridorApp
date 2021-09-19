using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Interfaces.Game.Objects
{
	public interface IWall : IGameObject
	{
		public const int WallSize = 2;
		WallDirection WallDirection { get; }
		public Coordinates BeginCoordinates { get; }
		public Coordinates EndCoordinates { get; }
	}
}