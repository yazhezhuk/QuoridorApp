using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;

namespace Core.Game.Objects.Actions
{
	public abstract class GameRule<TObject> : IExecuable<TObject> where TObject : GameObject
	{
		public GameRule(Field gameField){}

		public abstract bool Apply(TObject gameObj);
	}
}