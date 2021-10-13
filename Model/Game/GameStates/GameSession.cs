using System;
using Core.Game.Objects;
using Core.Game.Types;

namespace Core.Game.GameStates
{
	public class GameSession
	{
		public Player CurrentPlayer { get; }
		public int Turn { get; set; }
		public Field GameField { get; }

		public GameSession(Field gameField, int turn)
		{
			CurrentPlayer = (Player)Convert.ToInt32(Turn % 2 == 0);
			Turn = turn;
			GameField = gameField;
		}
		public GameSession(Field gameField,Player player, int turn)
		{
			CurrentPlayer = player;
			Turn = turn;
			GameField = gameField;
		}
		public GameSession(Field gameField)
		{
			CurrentPlayer = Player.None;
			Turn = 0;
			GameField = gameField;
		}


	}
}
