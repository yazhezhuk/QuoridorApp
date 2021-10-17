namespace Core.Game.Objects.Actions
{
	public class ActionResult
	{
		public ActionResult(bool result)
		{
			CanExecute = result;
		}
		public bool CanExecute { get; private set; }
	}
}
