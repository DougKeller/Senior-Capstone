using System.Collections.Generic;
using UnityEngine;
using EntityControllers;

namespace Entities
{
	public abstract class Entity : MonoBehaviour
	{

		public Rigidbody2D rigidBody;
		public new Collider2D collider;
		protected List<EntityController> controllers;
		public Statistics stats;

		public virtual float deathDuration { get { return 0f; } }

		protected abstract void InitializeControllers ();

		void SetPhysicsAttributes ()
		{
			Vector3 size = collider.bounds.size;
			rigidBody.mass = size.x * size.y;
			rigidBody.drag = Mathf.Pow (10, rigidBody.mass);
		}

		public virtual void Start ()
		{
			rigidBody.freezeRotation = true;
			controllers = new List<EntityController> ();
			stats = new Statistics ();
			InitializeControllers ();
			SetPhysicsAttributes ();
		}

		public virtual void Update ()
		{
			if (stats.hitpoints <= 0) Die ();

			foreach (EntityController controller in controllers) {
				controller.Update ();
			}
		}

		public virtual void OnCollisionEnter2D (Collision2D collision)
		{
			foreach (EntityController controller in controllers) {
				controller.OnCollisionEnter2D (collision);
			}
		}

		public virtual void OnTriggerEnter2D (Collider2D collision)
		{
			foreach (EntityController controller in controllers) {
				controller.OnTriggerEnter2D (collision);
			}
		}

		public virtual void Die ()
		{
			Destroy (rigidBody);
			Destroy (collider);
			Destroy (gameObject, deathDuration);
		}
	}
}