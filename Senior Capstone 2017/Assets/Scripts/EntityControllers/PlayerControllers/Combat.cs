using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers {
	public class Combat : EntityController {
		public Combat (Player parent) : base(parent) {}

		override public void Update () {
			if (entity.stats.hitpoints <= 0) {
				entity.Die ();
			}
		}

		override public void OnCollisionEnter2D (Collision2D collision) {
			if (collision.gameObject.tag == "Enemy") {
				entity.stats.hitpoints -= 10;
			}
		}
	}
}