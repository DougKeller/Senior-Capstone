using System.Collections.Generic;
using UnityEngine;
using EntityControllers;

namespace Entities
{
	public abstract class Entity : MonoBehaviour
	{

		public Animator animator;
		public Rigidbody2D rigidBody;
		public new Collider2D collider;
		protected List<EntityController> controllers;
		public Statistics stats;

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

		public virtual float deathDuration { get { return 0f; } }

		protected abstract void InitializeControllers ();

		void SetPhysicsAttributes ()
		{
			Vector3 size = collider.bounds.size;
			rigidBody.mass = size.x * size.y;
			rigidBody.drag = 10 + rigidBody.mass;
		}

		void Start ()
		{
			rigidBody.freezeRotation = true;
			currentState = State.Idling;
			controllers = new List<EntityController> ();
			stats = new Statistics ();
			InitializeControllers ();
			SetPhysicsAttributes ();
		}

		void Update ()
		{
			if (stats.hitpoints <= 0) Die ();
			if (currentState == State.Dying) return;

			foreach (EntityController controller in controllers) {
				controller.Update ();
			}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
			if (currentState == State.Dying) return;
			foreach (EntityController controller in controllers) {
				controller.OnCollisionEnter2D (collision);
			}
		}

		void OnTriggerEnter2D (Collider2D collision)
		{
			if (currentState == State.Dying) return;
			foreach (EntityController controller in controllers) {
				controller.OnTriggerEnter2D (collision);
			}
		}

		public void Die ()
		{
			currentState = State.Dying;
			Destroy (rigidBody);
			Destroy (collider);
			Destroy (gameObject, deathDuration);
		}
	}
}