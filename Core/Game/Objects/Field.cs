using System.Collections.Generic;
using Core.GameObjects;
using Core.GameStates;

namespace Core.Game.Objects
{
	public class Field : AbstractGameObject
	{
		public List<Wall> Walls { get; set; }
		public List<Cell> Cells { get; set; }

		public Field(List<Cell> gameCells, List<Wall> gameWalls)
		{
			Cells = gameCells;
			Walls = gameWalls;
		}

	}
}