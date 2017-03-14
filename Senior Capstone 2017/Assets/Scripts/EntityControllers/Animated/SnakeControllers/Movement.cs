using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated.SnakeControllers
{
	public class Movement : EntityAnimatedController
	{
		Vector2 a, b, target;

		public Movement (Snake parent) : base (parent)
		{
			Vector2 distance = new Vector2 (5, 0);
			a = entity.rigidBody.position - distance;
			b = entity.rigidBody.position + distance;
			target = a;
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

		Vector2 MovementVectorToTarget () {
			Vector2 position = entity.rigidBody.position;
			return target - position;
		}

		override public void Update ()
		{
			Vector2 movement = MovementVectorToTarget ();
			if (movement.magnitude < 0.1f) {
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
