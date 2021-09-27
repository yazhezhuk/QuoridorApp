namespace Core.Interfaces.Game.Objects
{
	public interface IExecuable<TObject> where TObject : IGameObject
	{
		public bool Apply(TObject gameObj);
	}
}