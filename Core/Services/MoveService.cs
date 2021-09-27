using System;
using System.Linq;
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
			_fieldService.GetAllBlockedDisplacements()
				.Select(disp => disp.ToString())
				.ToList()
				.ForEach(Console.WriteLine);
			Console.WriteLine(move.PositionDisplacement);
			return !_fieldService.GetAllBlockedDisplacements()
				       .ToHashSet()
				       .Contains(move.PositionDisplacement) &&
			       _fieldService.CanMoveToCell(
				       _fieldService.GetCellByPosition(move.PositionDisplacement.FromCoordinates),
			_fieldService.GetCellByPosition(move.PositionDisplacement.NewCoordinates));


		}
	}
}