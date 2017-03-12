using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers
{
	public class Combat : EntityControllers.EntityController
	{
		public Combat (Snake snake) : base (snake)
		{
			entity.stats.hitpoints = 10;
		}
	}
}