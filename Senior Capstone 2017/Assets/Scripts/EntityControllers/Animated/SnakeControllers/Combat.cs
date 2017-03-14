using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated.SnakeControllers
{
	public class Combat : EntityAnimatedController
	{
		public Combat (Snake snake) : base (snake)
		{
			entity.stats.hitpoints = 10;
		}
	}
}