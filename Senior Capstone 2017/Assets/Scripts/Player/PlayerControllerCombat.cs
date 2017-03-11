using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCombat : PlayerController {
	public int hitpoints;

	public PlayerControllerCombat (Player parent) : base(parent) {
		hitpoints = 20;
	}

	override public void Update () {
		if (hitpoints <= 0) {
			player.currentState = Player.State.Dying;
		}
	}

  override public void OnCollisionEnter2D (Collision2D collision) {}
}
