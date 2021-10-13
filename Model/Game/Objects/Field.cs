using System.Collections.Generic;
using System.Linq;
using Core.Game.Types;

namespace Core.Game.Objects
{
	public class Field : GameObject
	{
		public List<Wall> Walls { get; set; }
		public List<Cell> Cells { get; private set; }

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

		public Field(List<Wall> gameWalls)
		{
			Walls = gameWalls;
			PopulateField();
		}

		private void PopulateField()
		{
			Cells = new List<Cell>();
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
					Cells.Add(new Cell(new Coordinates(j, i)));
			}
		}

		public void SetFigure(Player player, Coordinates coordinates)
		{
			Cells.Find(cell => cell.Coordinates == coordinates).StandingPlayer = player;
		}


	}
}
