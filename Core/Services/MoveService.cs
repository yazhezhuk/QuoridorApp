using Core.Game.Objects.Moves;
using Core.Interfaces.GameServices;

namespace Core.Services
{
	public class MoveService : IMoveService
	{
		private readonly FieldService _fieldService;
		public MoveService(FieldService fieldService)
		{
			_fieldService = fieldService;
		}

		public bool ValidatePlayerFigureMove(PlayerFigureMove move)
		{
			move.PositionDisplacement
		}
	}
}