using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

	Rigidbody2D rigidBody;
	Animator animator;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		Vector2 movement = new Vector2 (horizontal, vertical);
		movement.Normalize ();

		if (movement != Vector2.zero) {
			animator.SetBool ("isWalking", true);
			animator.SetFloat ("inputX", movement.x);
			animator.SetFloat ("inputY", movement.y);
		} else {
			animator.SetBool ("isWalking", false);
		}

		Vector2 position = rigidBody.position + movement * Time.deltaTime;
		rigidBody.MovePosition (position);
	}
}
