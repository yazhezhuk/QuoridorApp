using System;
using System.Collections.Generic;
using Core.Game.Objects;
using Microsoft.AspNetCore.Mvc;
using Nest;
using QuoridorClient.Model;

namespace QuoridorClient
{
	public class FigureMoveContext
	{
		public Coordinates cellTo { get; set; }
		public Coordinates cellFrom { get; set; }

		public int turn { get; set; }
		public Coordinates opponent { get; set; }
		public List<WallViewModel> walls { get; set; } = new List<WallViewModel>();
	}
}
