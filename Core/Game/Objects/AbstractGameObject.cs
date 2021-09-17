using Core.Interfaces;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{
	public abstract class AbstractGameObject : IGameObject
	{
		public int Id { get; set; }
	}
}