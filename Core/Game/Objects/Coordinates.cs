using System;
using Ardalis.GuardClauses;

namespace Core.Game.Objects
{
	public class Coordinates
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
		
		public void UpdateX(int x)
		{
			X = ValidateCoordinate(x);
		}
		public void UpdateY(int y)
		{
			Y = ValidateCoordinate(y);
		}

		public bool IsNeighbourInColumnWith(Coordinates other) =>
			(Math.Abs(other.X - X) == 1) && (Math.Abs(other.Y - this.Y) == 0);
		
		public bool IsNeighbourInRowWith(Coordinates other) =>
			(Math.Abs(other.Y - Y) == 1) && (Math.Abs(other.X - this.X) == 0);

		public bool IsNeighbourInDiagonalWith(Coordinates other) =>
			Math.Abs(other.X - X) == Math.Abs(other.Y - Y);

	}
}