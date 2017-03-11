using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers {
	public class Movement : EntityControllers.EntityController {
		float baseSpeed;

		public Movement(Snake parent) : base(parent) {
			baseSpeed = 3f;
		}

		Vector2 GetMovementUnitVector () {
			float horizontal = -1 * Input.GetAxisRaw ("Horizontal");
			float vertical = -1 * Input.GetAxisRaw ("Vertical");
			Vector2 movement = new Vector2 (horizontal, vertical);
			movement.Normalize ();
			return movement;
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

			Vector2 movement = GetMovementUnitVector ();
			NotifyAnimator (movement);
			if (movement == Vector2.zero) {
				return;
			}

			Vector2 deltaMovement = movement * baseSpeed * Time.deltaTime;
			Vector2 newPosition = entity.rigidBody.position + deltaMovement;
			entity.rigidBody.MovePosition (newPosition);
		}
	}
}
