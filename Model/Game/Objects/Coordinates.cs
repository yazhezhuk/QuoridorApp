using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Core.Game.Types;

namespace Core.Game.Objects
{
	public struct Coordinates : IComparable<Coordinates>
	{

		public int X { get; private set; }
		public int Y { get; private set; }

		public Coordinates(int x, int y)
		{

			X = x;
			Y = y;
		}

		public int CompareTo(Coordinates other)
		{
			var xComparison = X.CompareTo(other.X);
			return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
		}

		public bool Equals(Coordinates other)
		{
			return X == other.X && Y == other.Y;
		}

		public override bool Equals(object obj)
		{
			return obj is Coordinates other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public static bool operator ==(Coordinates c1, Coordinates c2)
		{
			return c1.X == c2.X && c1.Y == c2.Y;
		}

		public static bool operator !=(Coordinates c1, Coordinates c2)
		{
			return !(c1 == c2);
		}

		public override string ToString()
		{
			return "X: " + X + ",Y: " + Y;
		}

	}
}
