using Core.Game.Types;

namespace Core.Interfaces.Game
{
	public interface IWall : IGameObject
	{
		const int WallSize = 2;
		WallDirection WallDirection { get; }
	}
}