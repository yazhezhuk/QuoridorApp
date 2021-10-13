using System;
using System.Collections.Generic;
using Core.Game.Objects;
using Microsoft.AspNetCore.Mvc;

namespace QuoridorClient
{
	public class MoveContext
	{
		public ValueTuple<int, int> From { get; set; }
		public ValueTuple<int, int> To { get; set; }
		public int Turn { get; set; }
		public ValueTuple<int, int> Opponent { get; set; }
		public List<Wall> Walls { get; set; }
	}
}
