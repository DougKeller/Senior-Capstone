using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakeController {
	private Snake _snake;
	protected Snake snake {
		get { return _snake; }
	}

	public SnakeController (Snake parent) {
		_snake = parent;
	}

	public abstract void Update ();
  public abstract void OnCollisionEnter2D (Collision2D collision);
}
