using Core.Game.Objects;
using Core.GameObjects;

namespace Core.GameStates
{
	public interface IGameState
	{
		public abstract Player CurrentPlayer { get; }
		public int Turn { get; }
		
		

	}
}