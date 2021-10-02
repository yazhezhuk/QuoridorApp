using System;
using Core.Game.Types;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Actions
{
	public class PlayerFigureDisplacement : IComparable<PlayerFigureDisplacement>
	{
		private bool Equals(PlayerFigureDisplacement other)
		{
			return FromCoordinates.Equals(other.FromCoordinates) && NewCoordinates.Equals(other.NewCoordinates);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && (PlayerFigureDisplacement)obj == this;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(FromCoordinates, NewCoordinates);
		}

		public Coordinates FromCoordinates { get; set; }
		public Coordinates NewCoordinates { get; set; }

		public static MoveDirection GetMoveDirection(Coordinates fromCoordinates, Coordinates newCoordinates)
		{
			if (fromCoordinates.X - newCoordinates.X == 0 &&
			    newCoordinates.Y - fromCoordinates.Y == -1)
			{
				return MoveDirection.North;
			}
			if (fromCoordinates.X - newCoordinates.X == 0 &&
			    newCoordinates.Y - fromCoordinates.Y == 1)
			{
				return MoveDirection.South;
			}
			if (fromCoordinates.Y - newCoordinates.Y == 0 &&
			    newCoordinates.X - fromCoordinates.X == -1)
			{
				return MoveDirection.West;
			}
			if (fromCoordinates.Y - newCoordinates.Y == 0 &&
			    newCoordinates.X - fromCoordinates.X == 1)
			{
				return MoveDirection.East;
			}

			return MoveDirection.None;
		}

		public PlayerFigureDisplacement(Coordinates from, Coordinates to)
		{
			FromCoordinates = from;
			NewCoordinates = to;
		}

		public PlayerFigureDisplacement(Cell from, Cell to)
		{
			FromCoordinates = from.Coordinates;
			NewCoordinates = to.Coordinates;
		}

		public override string ToString()
		{
			return $"C1:{FromCoordinates},C2:{NewCoordinates}";
		}

		public static bool operator ==(PlayerFigureDisplacement o1, PlayerFigureDisplacement o2)
		{
			return ((o1.FromCoordinates == o2.FromCoordinates) && (o2.NewCoordinates == o1.NewCoordinates)) ||
			       ((o2.NewCoordinates == o1.FromCoordinates) && (o2.FromCoordinates == o1.NewCoordinates));
		}

		public static bool operator !=(PlayerFigureDisplacement o1, PlayerFigureDisplacement o2)
		{
			return !(o1 == o2);
		}

		public int CompareTo(PlayerFigureDisplacement other)
		{
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			var fromCoordinatesComparison = FromCoordinates.CompareTo(other.FromCoordinates);
			return fromCoordinatesComparison != 0 ? fromCoordinatesComparison : NewCoordinates.CompareTo(other.NewCoordinates);
		}
	}
}