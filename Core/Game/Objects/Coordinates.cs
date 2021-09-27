using System;
using Ardalis.GuardClauses;

namespace Core.Game.Objects
{
	public struct Coordinates
	{

		public int X { get; private set; }
		public int Y { get; private set;}

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


		public bool IsNeighbourInColumnWith(Coordinates other) =>
			(Math.Abs(other.X - X) == 0) && (Math.Abs(other.Y - Y) == 1);
		
		public bool IsNeighbourInRowWith(Coordinates other) =>
			(Math.Abs(other.Y - Y) == 0) && (Math.Abs(other.X - X) == 1);

		public bool IsNeighbourInDiagonalWith(Coordinates other) =>
			Math.Abs(other.X - X) == Math.Abs(other.Y - Y);

	}
}