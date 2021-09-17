using System.Collections.Generic;
using Core.GameObjects;
using Core.GameStates;

namespace Core.Game.Objects
{
	public class Field : AbstractGameObject
	{
		public IList<Wall> Walls { get; set; }
		public IList<Cell> Cells { get; set; }

		public Field(IList<Cell> gameCells, IList<Wall> gameWalls)
		{
			Cells = gameCells;
			Walls = gameWalls;
		}

	}
}