﻿using UnityEngine;

namespace Entities.Static
{
  public class Projectile : EntityStatic
  {
    protected override void InitializeControllers () {}

    public override void Start () {
      base.Start ();
      stats.speed = 1f;
    }

    public override void Update () {
      Vector2 deltaMovement = transform.up * stats.speed * Time.deltaTime;
      Vector2 newPosition = rigidBody.position + deltaMovement;
      rigidBody.MovePosition (newPosition);
    }

    public override void OnTriggerEnter2D (Collider2D collider) {
      collider.gameObject.GetComponent<Entities.Animated.Player> ().stats.hitpoints -= 10;
      Die ();
    }
  }
}