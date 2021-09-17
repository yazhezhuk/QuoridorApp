using Core.Interfaces.Game;

namespace Core.Interfaces
{
	public interface IGameRule
	{
		void Apply(IGameObject gameObject);
	}
}