using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers {
	public class Combat : EntityController {
		public int hitpoints;

		public Combat (Player parent) : base(parent) {
			hitpoints = 20;
		}

		override public void Update () {
			if (hitpoints <= 0) {
				entity.Die ();
			}
		}

		override public void OnCollisionEnter2D (Collision2D collision) {
			if (collision.gameObject.tag == "Enemy") {
				hitpoints--;
				Debug.Log (hitpoints);
			}
		}
	}
}