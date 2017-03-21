using UnityEngine;

namespace Entities
{
	public class Entity : MonoBehaviour
	{
		public Rigidbody2D rigidBody;
		public Statistics.Stats stats;
		public Collider2D hitbox;
		public float deathDuration;
		public Animated animated;

		void SetPhysicsAttributes ()
		{
			rigidBody.freezeRotation = true;

			Collider2D[] colliders = GetComponentsInChildren<Collider2D> ();
			hitbox = colliders [colliders.Length - 1];

			Vector3 size = hitbox.bounds.size;
			rigidBody.mass = size.x * size.x * size.y * size.y;
			rigidBody.drag = Mathf.Pow (10, rigidBody.mass);
		}

		void Start ()
		{
			Physics2D.IgnoreLayerCollision (6, 7);
			SetPhysicsAttributes ();
		}

		public void Die ()
		{
			Destroy (rigidBody);
			Destroy (GetComponent<Collider>());
			Destroy (gameObject, deathDuration);
			if (animated) {
				animated.currentState = Animated.State.Dying;
			}
		}

		public int GetDamage () {
			return stats.damage;
		}

		public void AttackIfAble (GameObject enemyGameObject) {
			if (enemyGameObject.tag != "Hitbox") return;
			if (!stats.CanAttack ()) return;

			stats.Attack ();
			enemyGameObject.SendMessageUpwards ("TakeDamage", this, SendMessageOptions.DontRequireReceiver);
		}

		public void AttackIfAble (Entity otherEntity) {
			Vector3 point = otherEntity.hitbox.bounds.ClosestPoint (rigidBody.position);
			float distance = Vector2.Distance (point, rigidBody.position);
			if (distance > stats.attackRange)
				return;
			AttackIfAble (otherEntity.hitbox.gameObject);
		}

		void TakeDamage (Entity offender) {
			stats.hitpoints -= offender.GetDamage ();
		}
	}
}