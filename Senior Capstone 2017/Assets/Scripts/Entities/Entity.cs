using UnityEngine;

namespace Entities
{
	public class Entity : MonoBehaviour
	{
		public Rigidbody2D rigidBody;
		public Statistics.Stats stats;
		public float deathDuration;
		public Animated animated;
		public Collider2D hitbox;
		public Combat combat;

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
	}
}