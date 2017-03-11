using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float speed;
	public float cameraZoom;
	Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera> ();
	}

	Vector3 targetPosition () {
		return target.position + new Vector3 (1, -1, 0);
	}

	// Update is called once per frame
	void Update () {
		mainCamera.orthographicSize = Screen.height / 64f / cameraZoom;

		if (target) {
			Vector3 lerp = Vector2.Lerp (transform.position, targetPosition(), speed);
			lerp.z = -10;
			transform.position = lerp;
		}
	}
}
