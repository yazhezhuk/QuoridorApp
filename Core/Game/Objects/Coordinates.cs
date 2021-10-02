using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace Core.Game.Objects
{
	public struct Coordinates : IComparable<Coordinates>
	{
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

		public int X { get; private set; }
		public int Y { get; private set; }

		public Coordinates(int x, int y)
		{
			X = ValidateCoordinate(x);
			Y = ValidateCoordinate(y);
		}

		private static int ValidateCoordinate(int coordinate) =>
			Guard.Against.OutOfRange(coordinate, nameof(coordinate), 0, 8);

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

		public void UpdateX(int x) =>
			X = ValidateCoordinate(x);

		public void UpdateY(int y) =>
			Y = ValidateCoordinate(y);


		public bool IsSameRow(Coordinates other) =>
			Math.Abs(other.Y - Y) == 0;

		public bool IsSameColumn(Coordinates other) =>
			Math.Abs(other.X - X) == 0;

		public bool IsNeighbourInColumnWith(Coordinates other) =>
			IsSameColumn(other) && Math.Abs(other.Y - Y) == 1;
		
		public bool IsNeighbourInRowWith(Coordinates other) =>
			IsSameRow(other) && Math.Abs(other.X - X) == 1;

		public bool IsNeighbourInDiagonalWith(Coordinates other) =>
			Math.Abs(other.X - X) == 1 && Math.Abs(other.X - X) == Math.Abs(other.Y - Y);

		public int CompareTo(Coordinates other)
		{
			var xComparison = X.CompareTo(other.X);
			return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
		}
	}
}
