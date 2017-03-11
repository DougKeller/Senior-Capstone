using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControllerMovement : SnakeController {
	float baseSpeed;

	public SnakeControllerMovement(Snake parent) : base(parent) {
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
			snake.currentState = Snake.State.Walking;
			snake.animator.SetFloat ("inputX", movement.x);
			snake.animator.SetFloat ("inputY", movement.y);
		} else {
			snake.currentState = Snake.State.Idling;
		}
	}

	override public void Update () {
		if (snake.currentState == Snake.State.Dying) {
			return;
		}

		Vector2 movement = GetMovementUnitVector ();
		NotifyAnimator (movement);
		if (movement == Vector2.zero) {
			return;
		}

		Vector2 deltaMovement = movement * baseSpeed * Time.deltaTime;
		Vector2 newPosition = snake.rigidBody.position + deltaMovement;
		snake.rigidBody.MovePosition (newPosition);
	}

	override public void OnCollisionEnter2D(Collision2D collision) {}
}
