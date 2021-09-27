using Core.Game.Objects.Actions;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Moves
{
	public class PlayerFigureMove : Move<PlayerFigure>
	{
		public PlayerFigureDisplacement PositionDisplacement { get; set; }

		public PlayerFigureMove(PlayerFigureDisplacement coordinatesChange)
		{
			PositionDisplacement = coordinatesChange;
		}
	}
}