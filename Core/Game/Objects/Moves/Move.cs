using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Moves
{
	public abstract class Move<TTarget> : AbstractGameObject
	{
		public IObjectDisplacement PositionDisplacement { get; set; }
		public TTarget MoveTarget { get; set; }
		
	}
}