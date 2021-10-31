using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.GameServices;
using Core.Services;

namespace CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			var cells = new List<Cell>();
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
					cells.Add(new Cell(new Coordinates(j, i)));
			}

			var walls = new List<Wall>
			{
				//new VerticalWall()
			};

			//walls[0].Id = 0;
		//	walls[0].Y = 5;
			//walls[0].X = 0;




			//walls[1] = new HorizontalWall();
			//walls[1].Y = 0;
		//	walls[1].X = 5;
		//	walls[1].Placed = false;

		Cell cell1 = cells[11];
		Cell cell2 = cells[12];
		cell1.StandingPlayer = Player.First;
		cell2.StandingPlayer = Player.Second;


	    var field = new Field(walls);
	    
	    var gameSession = new GameSession(field,Player.First,0);
	    
	    var gameSessionService = new GameSessionService(gameSession);
	    var fieldService = new CellService(gameSession,gameSessionService);
	    var wallService = new WallService(gameSession);
	    Console.WriteLine(fieldService.GetCellWithPlayerFigure(Player.First).Coordinates);

	    var moveService = new MoveService(gameSessionService, fieldService,wallService);

	    Console.WriteLine("Can place wall : " + moveService.TryPlaceWall(new Wall {X = 1,Y = 1},Direction.Horizontal,walls));

	    //Console.WriteLine("Can place wall : " + moveService.TryPlaceWall(4, 0, 6, Direction.Horizontal));
	    //Console.WriteLine("Can place wall : " + moveService.TryPlaceWall(5, 1, 7, Direction.Horizontal));
	    //Console.WriteLine("Can place wall : " + moveService.TryPlaceWall(6, 6, 1, Direction.Vertical));
	    
	    wallService.GetAllWalls().ForEach(Console.WriteLine);
	    
	    Console.WriteLine(moveService.ValidatePlayerFigureMove(
	    	new PlayerFigureMove(fieldService.GetCellWithPlayerFigure(Player.First), new Cell(6,1))));
	    
	    for (int i = 0; i < 9; i++)
	    {
	    	var lineWalls = walls.FindAll(wall =>
	    		wall is VerticalWall && (wall.X == i || wall.X == i - 1));
	    
	    	for (int j = 0; j < 9; j++)
	    	{
	    		Console.Write(field.CellsAsArray[i, j].HasPlayerFigure() ? "+" : "*");
	    		Console.Write(lineWalls.Any(wall => wall.Y == j) ? "|" : " ");
	    	}
	    	Console.WriteLine();
	    }
	  }
	}
}
