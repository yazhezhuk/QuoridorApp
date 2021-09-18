using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Moves
{
	public class PlayerFigureMove : Move<PlayerFigure>
	{
		
		public PlayerFigureMove(IPlayerFigureDisplacement coordinatesChange)
		{
			PositionDisplacement = coordinatesChange;
		}
	}
}