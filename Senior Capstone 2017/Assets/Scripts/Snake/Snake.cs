using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	public Animator animator;
	public Rigidbody2D rigidBody;

	private List<SnakeController> controllers;

	public enum State {
		Idling, Walking, Dying
	}

	private State _currentState;
	public State currentState {
		get {
			return _currentState;
		}
		set {
			_currentState = value;
			animator.SetInteger ("state", (int) _currentState);
		}
	}

	void Start () {
		rigidBody.freezeRotation = true;
		currentState = State.Idling;

		controllers = new List<SnakeController> ();
		controllers.Add (new SnakeControllerMovement (this));
		controllers.Add (new SnakeControllerCombat (this));
	}

	void Update () {
		foreach (SnakeController controller in controllers) {
			controller.Update ();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		foreach (SnakeController controller in controllers) {
			controller.OnCollisionEnter2D (collision);
		}
	}
}
