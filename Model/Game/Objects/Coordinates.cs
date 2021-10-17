using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Core.Game.Types;

namespace Core.Game.Objects
{
	public record Coordinates : IComparable<Coordinates>
	{

		public int X { get; set; }
		public int Y { get; set; }

		public Coordinates(int x, int y)
		{

			this.X = x;
			this.Y = y;
		}

		public int CompareTo(Coordinates other)
		{
			var xComparison = X.CompareTo(other.X);
			return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
		}

		public override string ToString()
		{
			return "X: " + X + ",Y: " + Y;
		}

	}
}
