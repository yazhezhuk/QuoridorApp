using Core.Game.Objects;
using Core.Game.Types;

namespace QuoridorClient.Model
{
	public class WallViewModel
	{

		public WallViewModel(int wallDirection, Coordinates _position)
		{
			direction = wallDirection;
			position = _position;
		}
		public int direction { get; set; }

		public Coordinates position { get; set; }
	}
}
