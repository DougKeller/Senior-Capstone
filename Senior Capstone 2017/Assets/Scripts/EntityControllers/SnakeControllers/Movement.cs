using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers
{
	public class Movement : MonoBehaviour
	{
		public Entity entity;
		Vector2 a, b, target;

		void Start ()
		{
			Vector2 distance = new Vector2 (GetComponent<Snake> ().distance, 0);
			a = entity.rigidBody.position - distance;
			b = entity.rigidBody.position + distance;
			target = a;
			entity.stats.speed = 2f;
		}

		void NotifyAnimator (Vector2 movement)
		{
			if (movement != Vector2.zero) {
				entity.animated.currentState = Animated.State.Walking;
				entity.animated.animator.SetFloat ("inputX", movement.x);
				entity.animated.animator.SetFloat ("inputY", movement.y);
			} else {
				entity.animated.currentState = Animated.State.Idling;
			}
		}

		void SearchForTargets ()
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll (entity.hitbox.bounds.center, 5F);
			foreach (Collider2D collider in colliders) {
				if (collider.gameObject.tag == "Player") {
					Entity player = collider.gameObject.GetComponent<Entity> ();
					if (player.animated.IsDying ()) break;

					entity.stats.speed = 4.5f;
					target = player.hitbox.bounds.ClosestPoint (entity.rigidBody.position);
					entity.AttackIfAble (player);

					return;
				}
			}

			entity.stats.speed = 3f;
			if (target != a && target != b) {
				target = a;
			}
		}

		Vector2 MovementVectorToTarget () {
			Vector2 position = entity.rigidBody.position;
			return target - position;
		}

		void Update ()
		{
			SearchForTargets ();
			Vector2 movement = MovementVectorToTarget ();

			if ((target == a || target == b) && Vector2.Distance (entity.rigidBody.position, target) < 0.1f) {
				target = target == a ? b : a;
			}

			movement = movement.normalized * entity.stats.speed * Time.deltaTime;
			NotifyAnimator (movement);

			Vector2 newPosition = entity.rigidBody.position + movement;
			entity.rigidBody.MovePosition (newPosition);
		}

		void OnCollisionEnter2D (Collision2D collision) {
			target = target == a ? b : a;
		}
	}
}
