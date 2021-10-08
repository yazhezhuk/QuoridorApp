using Core.Game.Objects;
using Core.Game.Types;
using Core.Interfaces.GameServices;
using Microsoft.AspNetCore.Mvc;

namespace QuoridorClient.Controllers
{
	[ApiController]
	[Route("wall")]
	public class WallController : ControllerBase
	{
		private readonly IMoveService _moveService;

		public WallController(IMoveService moveService) =>
			_moveService = moveService;

		[HttpGet]
		public bool TryPlaceWall(int wallId,int line, int offset, int direction) =>
			_moveService.TryPlaceWall(wallId,line,offset,(Direction)direction);
	}
}
