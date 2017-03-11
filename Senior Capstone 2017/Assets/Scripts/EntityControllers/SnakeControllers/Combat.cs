using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers {
	public class Combat : EntityControllers.EntityController {
		public int hitpoints;

		public Combat (Snake snake) : base(snake) {
			hitpoints = 20;
		}

		override public void Update () {
			if (hitpoints <= 0) {
				entity.Die ();
			}
		}

		override public void OnTriggerEnter2D(Collider2D collision) {
		}
	}
}