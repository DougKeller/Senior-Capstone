using UnityEngine;

namespace Entities
{
	public class Projectile : MonoBehaviour
	{
		public Entity entity;
		public new Collider2D collider;

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
		}

		void OnTriggerEnter2D (Collider2D collider) {
			entity.AttackIfAble (collider.gameObject);
			entity.Die ();
		}
	}
}
