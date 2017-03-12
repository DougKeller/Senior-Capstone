using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers
{
	public class Movement : EntityController
	{
		float baseSpeed;
		float stamina;
		float runMultiplier;
		float runCost;
		float runRegen;

		public Movement (Player parent) : base (parent)
		{
			baseSpeed = 3f;
			runMultiplier = 2f;
			stamina = 100f;
			runCost = 5f;
			runRegen = 1f;
		}

		Vector2 GetMovementUnitVector ()
		{
			float horizontal = Input.GetAxisRaw ("Horizontal");
			float vertical = Input.GetAxisRaw ("Vertical");
			Vector2 movement = new Vector2 (horizontal, vertical);
			movement.Normalize ();
			return movement;
		}

		void NotifyAnimator (Vector2 movement)
		{
			if (movement != Vector2.zero) {
				entity.currentState = Player.State.Walking;
				entity.animator.SetFloat ("inputX", movement.x);
				entity.animator.SetFloat ("inputY", movement.y);
			} else {
				entity.currentState = Player.State.Idling;
			}
		}

		bool IsRunning (Vector2 movement)
		{
			if (movement == Vector2.zero) {
				return false;
			}

			bool shiftKeyPressed = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift);
			return shiftKeyPressed;
		}

		float GetSpeed (Vector2 movement)
		{
			float speed = baseSpeed;
			float staminaCost = runCost * Time.deltaTime;
			if (stamina > staminaCost && IsRunning (movement)) {
				speed *= runMultiplier;
			}
			return speed;
		}

		void RegenerateStamina ()
		{
			if (stamina < 100f) {
				stamina += runRegen * Time.deltaTime;
			} else if (stamina > 100f) {
				stamina = 100f;
			}
		}

		void UpdateStamina (float speed)
		{
			float cost = runCost * Time.deltaTime;
			if (stamina > cost && speed > baseSpeed) {
				stamina -= cost;
			} else {
				RegenerateStamina ();
			}
		}

		override public void Update ()
		{
			if (entity.currentState == Player.State.Dying) {
				return;
			}

			Vector2 movement = GetMovementUnitVector ();
			NotifyAnimator (movement);
			if (movement == Vector2.zero) {
				return;
			}

			float speed = GetSpeed (movement);
			UpdateStamina (speed);

			Vector2 deltaMovement = movement * speed * Time.deltaTime;
			Vector2 newPosition = entity.rigidBody.position + deltaMovement;
			entity.rigidBody.MovePosition (newPosition);
		}
	}
}
