﻿using System.Collections;
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

	// Update is called once per frame
	void Update () {
		mainCamera.orthographicSize = Screen.height / (100f * cameraZoom);

		Vector3 lerp = Vector2.Lerp (transform.position, target.position, speed);
		lerp.z = -10;
		transform.position = lerp;
	}
}
