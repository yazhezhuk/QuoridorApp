using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.Game;
using Core.Interfaces.Game.Objects;

namespace Core.Game.Objects
{ 
	public class Wall
	{



		public const int WallSize = 1;

		public int X { get; set; }
		
		public int Y { get; set; }

		public virtual Cell BeginCell { get; }

		public virtual Cell EndCell { get; }

		public virtual List<Cell> GetAdjacentCells()
		{
			return new List<Cell>();
		}

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
			return (GetAdjacentCells().Count(coord => other.GetAdjacentCells().Contains(coord)) >= 2 &&
			        other.EndCell == BeginCell && other.GetType() == GetType()) ||
			       GetAdjacentCells().Count(coord => other.GetAdjacentCells().Contains(coord)) == 4;


		}

		public List<PlayerFigureMove> GetBlockedDisplacements()
		{
			var adjacentCells = GetAdjacentCells();
			return new List<PlayerFigureMove>
			{
				//vertical/horizontal displacement blocked by first part of wall
				new PlayerFigureMove(adjacentCells[0], adjacentCells[1]),
				//vertical/horizontal displacement blocked by second part of wall
				new PlayerFigureMove(adjacentCells[2], adjacentCells[3]),
				//diagonal
				new PlayerFigureMove(adjacentCells[0], adjacentCells[3]),
				new PlayerFigureMove(adjacentCells[1], adjacentCells[2])
			};
		}

	}
}
