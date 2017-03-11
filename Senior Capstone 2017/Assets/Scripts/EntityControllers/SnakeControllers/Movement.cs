using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers {
	public class Movement : EntityControllers.EntityController {
		float baseSpeed;
		int direction = 1;
		float distanceTravelled;

		public Movement(Snake parent) : base(parent) {
			baseSpeed = 3f;
			distanceTravelled = 0f;
		}

		void NotifyAnimator (Vector2 movement) {
			if (movement != Vector2.zero) {
				entity.currentState = Entity.State.Walking;
				entity.animator.SetFloat ("inputX", movement.x);
				entity.animator.SetFloat ("inputY", movement.y);
			} else {
				entity.currentState = Entity.State.Idling;
			}
		}

		override public void Update () {
			if (entity.currentState == Entity.State.Dying) {
				return;
			}
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
