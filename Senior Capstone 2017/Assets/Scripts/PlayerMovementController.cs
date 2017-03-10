using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

	Rigidbody2D rigidBody;
	Animator animator;
	float baseSpeed;
	float stamina;
	float runMultiplier;
	float runCost;
	float runRegen;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.freezeRotation = true;
		animator = GetComponent<Animator> ();

		baseSpeed = 1.5f;
		runMultiplier = 3f;
		stamina = 100f;
		runCost = 5f;
		runRegen = 1f;
	}

	Vector2 GetMovementUnitVector () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		Vector2 movement = new Vector2 (horizontal, vertical);
		movement.Normalize ();
		return movement;
	}

	void NotifyAnimator (Vector2 movement) {
		if (movement != Vector2.zero) {
			animator.SetBool ("isWalking", true);
			animator.SetFloat ("inputX", movement.x);
			animator.SetFloat ("inputY", movement.y);
		} else {
			animator.SetBool ("isWalking", false);
		}
	}

	bool IsRunning (Vector2 movement) {
		if (movement == Vector2.zero) {
			return false;
		}

		bool shiftKeyPressed = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift);
		return shiftKeyPressed;
	}

	float GetSpeed (Vector2 movement) {
		float speed = baseSpeed;
		float staminaCost = runCost * Time.deltaTime;
		if (stamina > staminaCost && IsRunning (movement)) {
			speed *= runMultiplier;
		}
		return speed;
	}

	void RegenerateStamina () {
		if (stamina < 100f) {
			stamina += runRegen * Time.deltaTime;
		} else if (stamina > 100f) {
			stamina = 100f;
		}
	}

	void UpdateStamina (float speed) {
		float cost = runCost * Time.deltaTime;
		if (stamina > cost && speed > baseSpeed) {
			stamina -= cost;
		} else {
			RegenerateStamina ();
		}
	}

	// Update is called once per frame
	void Update () {
		Vector2 movement = GetMovementUnitVector ();
		NotifyAnimator (movement);
		if (movement == Vector2.zero) {
			return;
		}

		float speed = GetSpeed (movement);
		UpdateStamina (speed);

		Vector2 deltaMovement = movement * speed * Time.deltaTime;
		Vector2 newPosition = rigidBody.position + deltaMovement;
		rigidBody.MovePosition (newPosition);
	}
}
