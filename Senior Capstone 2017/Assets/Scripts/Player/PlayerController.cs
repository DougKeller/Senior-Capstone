using System;

public abstract class PlayerController {
	private Player _player;
	protected Player player {
		get { return _player; }
	}

	public PlayerController (Player parent) {
		_player = parent;
	}

	public abstract void Update ();
}
