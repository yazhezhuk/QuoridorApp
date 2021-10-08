using System.Collections;
using System.Collections.Generic;
using Core.Game.Objects.Actions;

namespace Core.Extensions
{
	public class DisplacementComparator : IComparer<PlayerFigureMove>
	{
		public int Compare(PlayerFigureMove x, PlayerFigureMove y)
		{
			return x.CompareTo(y);
		}
	}
}
