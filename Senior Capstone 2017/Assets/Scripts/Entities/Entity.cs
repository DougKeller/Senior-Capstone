using System.Collections.Generic;
using UnityEngine;
using EntityControllers;

namespace Entities {
  public abstract class Entity : MonoBehaviour {

    public Animator animator;
    public Rigidbody2D rigidBody;
    protected List<EntityController> controllers;
    public Statistics stats;

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
      stats = new Statistics ();
      InitializeControllers ();
    }

    void Update () {
      if (currentState == State.Dying) return;
      foreach (EntityController controller in controllers) {
        controller.Update ();
      }
    }

    void OnCollisionEnter2D (Collision2D collision) {
      if (currentState == State.Dying) return;
      foreach (EntityController controller in controllers) {
        controller.OnCollisionEnter2D (collision);
      }
    }

    public void Die () {
      currentState = State.Dying;
	  rigidBody.isKinematic = true;
      Destroy (gameObject, 2);
    }
  }
}