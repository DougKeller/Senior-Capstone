using UnityEngine;

namespace Entities
{
	public class Projectile : MonoBehaviour
	{
		public Entity entity;

		void Start () {
			entity.stats.speed = 20f;
			entity.stats.damage = 5;
			Invoke ("Die", 1f);
		}

		void Update () {
			Vector2 deltaMovement = transform.up * entity.stats.speed * Time.deltaTime;
			Vector2 newPosition = entity.rigidBody.position + deltaMovement;
			entity.rigidBody.MovePosition (newPosition);
		}

		void OnTriggerEnter2D (Collider2D collider) {
			entity.AttackIfAble (collider.gameObject);
			entity.Die ();
		}
	}
}
