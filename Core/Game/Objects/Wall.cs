using System.Collections.Generic;
using System.Linq;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;

namespace Core.Game.Objects
{ 
	public abstract class Wall : GameObject
	{

		public Wall(){}

		public Wall(int line, int offsetFromEdge)
		{
			Line = line;
			OffsetFromEdge = offsetFromEdge;
		}

		public const int WallSize = 1;

		public int OffsetFromEdge { get; set; }
		
		public int Line { get; set; }

		public abstract Coordinates BeginCoordinates { get; }

		public abstract Coordinates EndCoordinates { get; }

		public abstract List<Coordinates> GetAdjacentCoordinates();

		public bool Intersects(Wall other)
		{
			/*Walls only have 2 possibilities to intersect:
				1) if walls have different directions and one crosses another in middle:
				 |
				-+-
				 |
				 2) if walls have a similar direction and one lies in another partly or fully:
				 ---
				 or
				 |
				 |
				 |
			*/
			return (GetAdjacentCoordinates().Count(coord => other.GetAdjacentCoordinates().Contains(coord)) >= 2 &&
			other.EndCoordinates == BeginCoordinates && other.GetType() == GetType()) ||
			       GetAdjacentCoordinates().Count(coord => other.GetAdjacentCoordinates().Contains(coord)) == 4;


		}

		public List<PlayerFigureDisplacement> GetBlockedDisplacements()
		{
			var adjacentCoordinates = GetAdjacentCoordinates();
			return new List<PlayerFigureDisplacement>
			{
				//vertical/horizontal displacement blocked by first part of wall
				new PlayerFigureDisplacement(adjacentCoordinates[0], adjacentCoordinates[1]),
				//vertical/horizontal displacement blocked by second part of wall
				new PlayerFigureDisplacement(adjacentCoordinates[2], adjacentCoordinates[3]),
				//diagonal
				new PlayerFigureDisplacement(adjacentCoordinates[0], adjacentCoordinates[3]),
				new PlayerFigureDisplacement(adjacentCoordinates[1], adjacentCoordinates[2])
			};
		}

	}
}
