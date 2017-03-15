using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated.SnakeControllers
{
	public class Combat : EntityAnimatedController
	{
		public Combat (Snake snake) : base (snake)
		{
			entity.stats.hitpoints = 10;
			entity.stats.attackRange = 0.5f;
			entity.stats.attackSpeed = 0.5f;
			entity.stats.damage = 1;
		}
	}
}