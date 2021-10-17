using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects
{
	public class Cell : IEquatable<Cell>
	{
		public Cell(Coordinates coordinates,Player standingPlayer)
		{
			Coordinates = coordinates;
			StandingPlayer = standingPlayer;
		}

		public Cell((int x, int y) coordinates)
		{
			Coordinates = new Coordinates(coordinates.x,coordinates.y);
			StandingPlayer = Player.None;
		}

		public Cell(Coordinates coordinates)
		{
			Coordinates = coordinates;
			StandingPlayer = Player.None;
		}

		public Cell(int x,int y)
		{
			Coordinates = new Coordinates(x,y);
			StandingPlayer = Player.None;
		}

		public Cell(int x,int y,Player player) : this(x,y)
		{
			Coordinates = new Coordinates(x, y);
			StandingPlayer = player;
		}

		public Player StandingPlayer { get; set; }
		public Coordinates Coordinates { get; set; }

		public bool HasPlayerFigure() => StandingPlayer != Player.None;


		public bool IsSameRow(Cell other) =>
			Math.Abs(other.Coordinates.Y - Coordinates.Y) == 0;

		public bool IsSameColumn(Cell other) =>
			Math.Abs(other.Coordinates.X - Coordinates.X) == 0;

		public bool IsNeighbourInColumnWith(Cell other) =>
			IsSameColumn(other) && Math.Abs(other.Coordinates.Y - Coordinates.Y) == 1;

		public bool IsNeighbourInRowWith(Cell other) =>
			IsSameRow(other) && Math.Abs(other.Coordinates.X - Coordinates.X) == 1;

		public bool IsNeighbourInDiagonalWith(Cell other) =>
			Math.Abs(other.Coordinates.X - Coordinates.X) == 1 && Math.Abs(other.Coordinates.X - Coordinates.X) == Math.Abs(other.Coordinates.Y - Coordinates.Y);


		public int GetDistanceToAnotherCell(Cell other)
		{
			return Math.Abs(other.Coordinates.X - Coordinates.X) +
			                         Math.Abs(other.Coordinates.Y - Coordinates.Y);
		}

		public bool Equals(Cell other)
		{
			return other is not null && (ReferenceEquals(this,other) || (StandingPlayer == other.StandingPlayer
			&& Coordinates == other.Coordinates));
		}

		public static bool operator ==(Cell c1, Cell c2)
		{
			if (ReferenceEquals(c1, c2)) return true;

			return c1.Equals(c2);
		}

		public static bool operator !=(Cell c1, Cell c2)
		{
			return !(c1 == c2);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (obj.GetType() != GetType()) return false;
			return Equals((Cell)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(StandingPlayer, Coordinates);
		}
	}
}
