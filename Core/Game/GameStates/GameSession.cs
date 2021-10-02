using Core.Game.Objects;
using Core.Game.Objects.Actions;

namespace Core.GameStates
{
	public class GameSession
	{
		public Player CurrentPlayer { get; }
		public int Turn { get; }
		public Field GameField { get; }

		public GameSession(Field gameField,Player player, int turn)
		{
			CurrentPlayer = player;
			Turn = turn;
			GameField = gameField;
		}


	}
}
