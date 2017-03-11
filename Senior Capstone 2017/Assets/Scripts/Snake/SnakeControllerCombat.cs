using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControllerCombat : SnakeController {
	public int hitpoints;

	public SnakeControllerCombat (Snake snake) : base(snake) {
		hitpoints = 20;
	}

	override public void Update () {
		if (hitpoints <= 0) {
			snake.currentState = Snake.State.Dying;
		}
	}

	override public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			snake.currentState = Snake.State.Dying;
			MonoBehaviour.Destroy (snake.gameObject);
		}
	}
}
