using System;
using Core.Game.Types;
using Core.Interfaces.Game.Shared;

namespace Core.Game.Objects.Actions
{
	public class PlayerFigureDisplacement : IPlayerFigureDisplacement
	{
		

		public PlayerFigureDisplacement(Coordinates from, Coordinates to)
		{
			FromCoordinates = from;
			NewCoordinates = to;
		}
		public Coordinates FromCoordinates { get; set; }
		public Coordinates NewCoordinates { get; set; }
		public MoveDirection MoveDirection { get; set; }

		public static bool operator ==(PlayerFigureDisplacement o1, PlayerFigureDisplacement o2)
			=> (o1?.FromCoordinates == o2?.FromCoordinates || o1?.FromCoordinates == o2?.NewCoordinates) &&
			   (o1?.NewCoordinates == o2?.NewCoordinates || o1?.NewCoordinates == o2?.FromCoordinates);

		public static bool operator !=(PlayerFigureDisplacement o1, PlayerFigureDisplacement o2)
		{
			return !(o1 == o2);
		}

	}
}