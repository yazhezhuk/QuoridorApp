using System;
using Core.Interfaces.Game;

namespace Core.Game.Objects
{
	public class Cell : AbstractGameObject, IWithCoordinates, IEquatable<Cell>
	{
		public Cell(Coordinates startCoordinates)
		{
			Coordinates = startCoordinates;
		}
		
		public PlayerFigure StandingPlayer { get; set; } = null;
		public Coordinates Coordinates { get; set; }

		public bool Equals(Cell other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(StandingPlayer, other.StandingPlayer) && Coordinates.Equals(other.Coordinates);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Cell)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(StandingPlayer, Coordinates);
		}
	}
}