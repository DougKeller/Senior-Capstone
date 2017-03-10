using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovementController : MonoBehaviour {

	public Transform target;
	Rigidbody2D rigidBody;
	Animator animator;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.freezeRotation = true;
		animator = GetComponent<Animator> ();
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

	// Update is called once per frame
	void Update () {
		Vector2 movement = Vector2.Lerp (transform.position, target.position, 0.1f);
		NotifyAnimator (movement);
		if (movement.magnitude < 1) {
			return;
		}

		rigidBody.position = movement;
	}
}
