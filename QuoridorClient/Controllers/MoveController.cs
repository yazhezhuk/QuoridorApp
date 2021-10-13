using System;
using System.Collections.Generic;
using Core.Game.GameStates;
using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.GameServices;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace QuoridorClient.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MoveController : ControllerBase
	{

		private GameSession _gameSession;
		private GameSessionService _gameSessionService;
		private CellService _cellService;
		private WallService _wallService;
		private MoveService _moveService;



		public MoveController()
		{
			var gameField = new Field(new List<Wall>());
			_gameSession = new GameSession(gameField);
			_gameSessionService = new GameSessionService(_gameSession);
			_cellService = new CellService(_gameSession,_gameSessionService);
			_wallService = new WallService(_gameSession);
			_moveService = new MoveService(_gameSessionService, _cellService,_wallService);
		}

		[HttpGet]

		public bool TryExecuteMove([FromBody]MoveContext context)
		{
			_gameSession.GameField.Walls = context.Walls;
			_gameSession.Turn = context.Turn;

			_gameSession.GameField.SetFigure(Player.First, new Coordinates(context.From.Item1,context.From.Item2));
			_gameSession.GameField.SetFigure(Player.Second, new Coordinates(context.Opponent.Item1,context.Opponent.Item2));

			return _moveService.CanExecuteMove(new PlayerFigureMove(new Cell(context.From),new Cell(context.To)));
		}

	}
}
