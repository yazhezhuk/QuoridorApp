using Core.Game.Objects;
using Core.Game.Objects.Moves;
using Core.Interfaces.Game;

namespace Core.Interfaces.GameServices
{
	public interface IMoveService
	{
		bool ValidatePlayerFigureMove(PlayerFigureMove move);
	}
}