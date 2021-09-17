using Core.Game.Objects;
using Core.GameStates;

namespace Core.GameObjects
{
	public class Game
	{
		public IGameState CurrentState { get; set; }
		public Field GameField { get; set; }

		public Game(Field gameFiled, IGameState currentState)
		{
			GameField = gameFiled;
			CurrentState = currentState;
		}
	}
}