using System.Collections.Generic;
using Core.GameObjects;
using Core.GameStates;

namespace Core.Game.Objects
{
	public class Field : GameObject
	{
		public List<Wall> Walls { get; set; }
		public List<Cell> Cells { get; }

		public Cell[,] CellsAsArray
		{
			get
			{
				var cells = new Cell[9,9];
				for (var i = 0; i < 9; i++)
				{
					for (var j = 0; j < 9; j++)
						cells[i,j] = Cells[i * 9 + j];
				}
				return cells;
			}
		}




		public Field(List<Cell> gameCells, List<Wall> gameWalls)
		{
			Cells = gameCells;
			Walls = gameWalls;
		}

	}
}