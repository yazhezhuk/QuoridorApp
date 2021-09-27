using System.Collections.Generic;
using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Services
{
	public class WallService
	{
		private List<Wall> _walls { get; set; }

		public WallService(List<Wall> walls)
		{
			_walls = walls;
		}

		public IReadOnlyCollection<Wall> GetAllWalls() => _walls.AsReadOnly();

		public void TryPlaceWall(int id, int line, int offset, WallDirection wallDirection)
		{
			var wall = _walls.Find(wall => wall.Id == id);
			
		}
		
	}
}