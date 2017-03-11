using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

  public Animator animator;
  public Rigidbody2D rigidBody;

  private List<EntityController> controllers;

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

  protected void AddController(EntityController controller) {
    controllers.Add (controller);
  }

  void Start () {
    rigidBody.freezeRotation = true;
    currentState = State.Idling;
    controllers = new List<EntityController> ();
  }

  void Update () {
    foreach (EntityController controller in controllers) {
      controller.Update ();
    }
  }
}
