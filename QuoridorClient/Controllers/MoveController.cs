using Core.Game.Objects;
using Core.Game.Objects.Actions;
using Core.Game.Types;
using Core.Interfaces.GameServices;
using Microsoft.AspNetCore.Mvc;

namespace QuoridorClient.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MoveController : ControllerBase
	{
		private readonly IMoveService _moveService;

		public MoveController(IMoveService moveService) =>
			_moveService = moveService;

		[HttpGet]
		public bool TryExecuteMove(Cell from, Cell to) =>
			_moveService.CanExecuteMove(new PlayerFigureMove(from,to));
	}
}
