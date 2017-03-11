using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers {
	public class Combat : EntityControllers.EntityController {
		public Combat (Snake snake) : base(snake) {
			entity.stats.hitpoints = 20;
		}

		override public void Update () {
			if (entity.stats.hitpoints <= 0) {
				entity.Die ();
			}
		}
	}
}