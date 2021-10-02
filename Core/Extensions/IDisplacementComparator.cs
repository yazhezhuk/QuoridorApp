using System.Collections;
using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Extensions
{
	public class DisplacementComparator : IComparer<PlayerFigureDisplacement>
	{
		public int Compare(PlayerFigureDisplacement x, PlayerFigureDisplacement y)
		{
			if (ReferenceEquals(x, y)) return 0;
			if (ReferenceEquals(null, y)) return 1;
			if (ReferenceEquals(null, x)) return -1;
			return x.CompareTo(y);
		}
	}
}