using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Interfaces.GameServices
{
	public interface IGameSessionService
	{
		bool ValidateTurn(Player player);
		Player GetCurrentPlayer();
		Player GetOpponentPlayer();
		bool ValidateVictory(Cell targetCell);

	}
}
