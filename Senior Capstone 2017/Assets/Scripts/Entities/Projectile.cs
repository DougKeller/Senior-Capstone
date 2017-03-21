using UnityEngine;

namespace Entities
{
	public class Projectile : MonoBehaviour
	{
		public Entity entity;
		public new Collider2D collider;
		public Entity source;

		void Start () {
			Invoke ("Die", 3f);
		}

		void Update () {
			Vector2 deltaMovement = transform.up * entity.stats.speed * Time.deltaTime;
			Vector2 newPosition = entity.rigidBody.position + deltaMovement;
			entity.rigidBody.MovePosition (newPosition);
		}

		void Die ()
		{
			entity.Die ();
			if (source != null) {
				source.stats.GiveExperience (entity.stats.TotalExperience ());
			}
		}

		void OnTriggerEnter2D (Collider2D collider) {
			entity.AttackIfAble (collider.gameObject);
			Die ();
		}
	}
}
