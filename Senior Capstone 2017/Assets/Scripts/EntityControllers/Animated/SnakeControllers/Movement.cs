using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated.SnakeControllers
{
	public class Movement : EntityAnimatedController
	{
		Vector2 a, b, target;

		public Movement (Snake parent) : base (parent)
		{
			Vector2 distance = new Vector2 (((Snake)entity).distance, 0);
			a = entity.rigidBody.position - distance;
			b = entity.rigidBody.position + distance;
			target = a;
			entity.stats.speed = 2f;
		}

		void NotifyAnimator (Vector2 movement)
		{
			if (movement != Vector2.zero) {
				entity.currentState = Snake.State.Walking;
				entity.animator.SetFloat ("inputX", movement.x);
				entity.animator.SetFloat ("inputY", movement.y);
			} else {
				entity.currentState = Snake.State.Idling;
			}
		}

		void SearchForTargets ()
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll (entity.hitbox.bounds.center, 5f);
			foreach (Collider2D collider in colliders) {
				if (collider.gameObject.tag == "Player") {
					Player player = collider.gameObject.GetComponent<Player> ();
					if (player.IsDying ()) break;

					entity.stats.speed = 3.5f;
					target = player.hitbox.bounds.center;
					if (Vector2.Distance (entity.rigidBody.position, target) < entity.stats.attackRange) {
						entity.AttackIfAble (player);
					}

					return;
				}
			}

			entity.stats.speed = 2f;
			if (target != a && target != b) {
				target = a;
			}
		}

		Vector2 MovementVectorToTarget () {
			Vector2 position = entity.rigidBody.position;
			return target - position;
		}

		override public void Update ()
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

		override public void OnCollisionEnter2D (Collision2D collision) {
			target = target == a ? b : a;
		}
	}
}
