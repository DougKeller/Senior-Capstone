using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers
{
	public class Movement : MonoBehaviour
	{
		public Entity entity;

		float runMultiplier;
		float runCost;
		float runRegen;
		float stamina {
			get {
				return entity.stats.stamina;
			}
			set {
				entity.stats.stamina = value;
			}
		}

		void Start ()
		{
			runMultiplier = 2f;
			runCost = 20f;
			runRegen = 10f;
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
			if (Input.GetKey(KeyCode.LeftShift) && entity.animated.currentState == Animated.State.Walking)
				return;
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
			}
		}

		void Update ()
		{
			if (entity.animated.currentState == Animated.State.Dying) {
				return;
			}
			RegenerateStamina ();

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
