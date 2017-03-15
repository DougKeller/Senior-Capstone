using UnityEngine;

namespace Entities
{
  public class Animated : MonoBehaviour
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

    void Start ()
    {
      currentState = State.Idling;
    }

		public bool IsDying ()
		{
			return currentState == State.Dying;
		}
  }
}
