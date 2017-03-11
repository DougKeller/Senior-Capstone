using System.Collections.Generic;
using UnityEngine;
using EntityControllers;

namespace Entities {
  public abstract class Entity : MonoBehaviour {

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

    protected abstract void InitializeControllers ();

    void Start () {
      rigidBody.freezeRotation = true;
      currentState = State.Idling;
      controllers = new List<EntityController> ();
      InitializeControllers ();
    }

    void Update () {
      if (currentState == State.Dying) return;

      foreach (EntityController controller in controllers) {
        controller.Update ();
      }
    }

    void OnTriggerEnter2D (Collider2D collision) {
      if (currentState == State.Dying) return;

      foreach (EntityController controller in controllers) {
        controller.OnTriggerEnter2D (collision);
      }
    }

    public void Die () {
      currentState = State.Dying;
      Destroy (gameObject, 2);
    }
  }
}