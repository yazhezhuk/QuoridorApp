using System;
using Core.Game.Objects;
using Core.Interfaces.Game.Shared;

namespace Core.Interfaces.GameServices
{
	public interface ICellService
	{
		IPlayerFigureDisplacement GetDisplacementByCells(Tuple<Cell> cells);
	}
}