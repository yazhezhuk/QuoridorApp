namespace Core.Interfaces.Game.Objects
{
	public interface IExecuable<TObject>
	{
		public bool Apply(TObject gameObj);
	}
}