using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Game.Objects;
using Core.Game.Types;
using Core.Interfaces.Game.Shared;

namespace Core.Interfaces.GameServices
{
	public interface IFieldService
	{
		bool IsCellIsBlocked(Cell cell);

		bool CanPlaceWall();

		public IList<IPlayerFigureDisplacement> DisplacementsBlockedByWall(Wall wall);
	}
}