using System;
using Core.Game.Types;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Actions
{
	public class PlayerFigureDisplacement : IComparable
	{
		private bool Equals(PlayerFigureDisplacement other)
		{
			return FromCoordinates.Equals(other.FromCoordinates) && NewCoordinates.Equals(other.NewCoordinates);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && (PlayerFigureDisplacement)obj== this;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(FromCoordinates, NewCoordinates);
		}

		public Coordinates FromCoordinates { get; set; }
		public Coordinates NewCoordinates { get; set; }

		public PlayerFigureDisplacement(Coordinates from, Coordinates to)
		{
			FromCoordinates = from;
			NewCoordinates = to;
		}

		public override string ToString()
		{
			return $"C1:{FromCoordinates},C2:{NewCoordinates}";
		}

		public int CompareTo(object? obj)
		{
			return ((PlayerFigureDisplacement)obj) == (this) ? 1 : -1;
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

	}
}