using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Animator animator;
	public Rigidbody2D rigidBody;

	private List<PlayerController> controllers;

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

		controllers = new List<PlayerController> ();
		controllers.Add (new PlayerControllerMovement (this));
		controllers.Add (new PlayerControllerCombat (this));

		Debug.Log (controllers);
	}

	void Update () {
		foreach (PlayerController controller in controllers) {
			controller.Update ();
		}
	}
}
