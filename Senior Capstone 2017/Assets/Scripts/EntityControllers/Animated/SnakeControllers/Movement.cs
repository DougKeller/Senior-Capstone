using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated.SnakeControllers
{
	public class Movement : EntityAnimatedController
	{
		float baseSpeed;
		int direction = 1;
		float distanceTravelled;

		public Movement (Snake parent) : base (parent)
		{
			baseSpeed = 3f;
			distanceTravelled = 0f;
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

		override public void Update ()
		{
			if (distanceTravelled > 5) {
				distanceTravelled = 0;
				direction = -direction;
			}

			Vector2 movement = new Vector2 (direction, 0);
			NotifyAnimator (movement);

			Vector2 deltaMovement = movement * baseSpeed * Time.deltaTime;
			distanceTravelled += deltaMovement.magnitude;

			Vector2 newPosition = entity.rigidBody.position + deltaMovement;
			entity.rigidBody.MovePosition (newPosition);
		}
	}
}
