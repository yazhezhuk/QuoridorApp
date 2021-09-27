using System;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Interfaces.Game.Shared;

namespace Core.Interfaces.GameServices
{
	public interface ICellService
	{
		PlayerFigureDisplacement GetDisplacementByCells(Tuple<Cell> cells);
	}
}