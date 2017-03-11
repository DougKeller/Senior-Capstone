using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

  public Animator animator;
  public Rigidbody2D rigidBody;

  protected List<EntityController> controllers;

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

  protected virtual void InitializeControllers () {}

  void Start () {
    rigidBody.freezeRotation = true;
    currentState = State.Idling;
    controllers = new List<EntityController> ();
    InitializeControllers ();
  }

  void Update () {
    foreach (EntityController controller in controllers) {
      controller.Update ();
    }
  }
}
