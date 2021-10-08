using System;
using Core.Game.Types;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Actions
{
	public struct PlayerFigureMove : IEquatable<PlayerFigureMove>, IComparable<PlayerFigureMove>
	{
		public Cell Source { get; set; }
		public Cell Target { get; set; }

		public MoveDirection MoveDirection
		{
			get
			{
				if (Source.IsSameRow(Target) &&
				    Target.Coordinates.Y - Source.Coordinates.Y < 0)
				{
					return MoveDirection.North;
				}

				if (Source.IsSameRow(Target) &&
				    Target.Coordinates.Y - Source.Coordinates.Y > 0)
				{
					return MoveDirection.South;
				}

				if (Source.IsSameColumn(Target) &&
				    Target.Coordinates.X - Source.Coordinates.X < 0)
				{
					return MoveDirection.West;
				}

				if (Source.IsSameColumn(Target) &&
				    Target.Coordinates.X - Source.Coordinates.X > 0)
				{
					return MoveDirection.East;
				}

				return MoveDirection.Diagonal;
			}
		}

		public PlayerFigureMove(Cell source, Cell target)
		{
			Source = source;
			Target = target;
		}

		public override string ToString()
		{
			return $"C1:{Source.Coordinates},C2:{Target.Coordinates}";
		}


		public override bool Equals(object obj)
		{
			return obj is PlayerFigureMove other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Source.Coordinates, Target.Coordinates) + HashCode.Combine(Target.Coordinates,Source.Coordinates);
		}

		public int CompareTo(PlayerFigureMove other)
		{
			var fromCoordinatesComparison = Source.Coordinates.CompareTo(other.Source.Coordinates) + Source.Coordinates
			.CompareTo(other.Target.Coordinates);
			return fromCoordinatesComparison != 0 ? fromCoordinatesComparison : Target.Coordinates.CompareTo(other.Target.Coordinates) +
				Target.Coordinates.CompareTo(other.Source.Coordinates);
		}

		public bool Equals(PlayerFigureMove other)
		{
			return (Source.Coordinates.Equals(other.Source.Coordinates) && Target.Coordinates.Equals(other.Target.Coordinates))
				|| (Source.Coordinates.Equals(other.Target.Coordinates) && Target.Coordinates.Equals(other.Source.Coordinates));
		}
	}
}
