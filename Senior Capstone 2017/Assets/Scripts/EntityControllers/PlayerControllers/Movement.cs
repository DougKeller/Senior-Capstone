using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers
{
	public class Movement : MonoBehaviour
	{
		public Entity entity;

		float stamina;
		float runMultiplier;
		float runCost;
		float runRegen;

		void Start ()
		{
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
				entity.animated.currentState = Animated.State.Walking;
				entity.animated.animator.SetFloat ("inputX", movement.x);
				entity.animated.animator.SetFloat ("inputY", movement.y);
			} else {
				entity.animated.currentState = Animated.State.Idling;
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
			float speed = entity.stats.speed;
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
			if (stamina > cost && speed > entity.stats.speed) {
				stamina -= cost;
			} else {
				RegenerateStamina ();
			}
		}

		void Update ()
		{
			if (entity.animated.currentState == Animated.State.Dying) {
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
