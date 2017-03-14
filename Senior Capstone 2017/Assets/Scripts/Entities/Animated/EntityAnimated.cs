using UnityEngine;

namespace Entities.Animated
{
  public abstract class EntityAnimated : Entities.Entity
  {

    public Animator animator;

    public enum State
    {
      Idling,
      Walking,
      Dying
    }

    private State _currentState;

    public State currentState {
      get { return _currentState; }
      set {
        _currentState = value;
        animator.SetInteger ("state", (int)_currentState);
      }
    }

    protected abstract override void InitializeControllers ();

    public override void Start ()
    {
      base.Start ();
      currentState = State.Idling;
    }

    public override void Update ()
    {
      if (currentState == State.Dying) return;
      base.Update();
    }

    public override void OnCollisionEnter2D (Collision2D collision)
    {
      if (currentState == State.Dying) return;
      base.OnCollisionEnter2D(collision);
    }

    public override void OnTriggerEnter2D (Collider2D collision)
    {
      if (currentState == State.Dying) return;
      base.OnTriggerEnter2D(collision);
    }

    public override void Die ()
    {
      base.Die();
      currentState = State.Dying;
    }
  }
}
