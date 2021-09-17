using Core.Game.Objects;
using Core.GameObjects;

namespace Core.Managers
{
	public class UserManager
	{
		public Player[] GamePlayers { get; init; }


		public UserManager(Player[] gamePlayers)
		{
			GamePlayers = gamePlayers;
		}
	}
}