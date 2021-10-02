using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Core.Game.Objects;
using Core.Game.Objects.Actions;

namespace Core.Services
{
	public class DisplacementService
	{
		private readonly CellService _cellService;
		private readonly WallService _wallService;

		public DisplacementService(WallService wallService,CellService fieldService)
		{
			_cellService = fieldService;
			_wallService = wallService;
		}

		public ISet<PlayerFigureDisplacement> GetFiguresBlockedDisplacements()
		{
			var displacements = new HashSet<PlayerFigureDisplacement>();
			foreach (var wall in _wallService.GetAllWalls())
			{
				foreach (var playerFigureDisplacement in wall.GetBlockedDisplacements())
					displacements.Add(playerFigureDisplacement);
			}

			foreach (var playerFigure in _cellService.GetAllCells().Where(cell => cell.HasPlayerFigure()))
			{
				foreach (var cell in _cellService.GetNonDiagonalCellNeighbours(playerFigure)
					.Where(cell => cell.HasPlayerFigure()))
				{
					displacements.Add(new PlayerFigureDisplacement(cell, playerFigure));
				}
			}

			return displacements;
		}

		public List<Cell> GetNotBlockedCellNeighbours(Cell cell) =>
			_cellService.GetNonDiagonalCellNeighbours(cell)
				.Where(neighbourCell => !IsDisplacementBlocked(new PlayerFigureDisplacement(cell,neighbourCell)))
				.ToList();


		public bool IsDisplacementBlocked(PlayerFigureDisplacement displacement) =>
			GetFiguresBlockedDisplacements().Contains(displacement);

	}
}
