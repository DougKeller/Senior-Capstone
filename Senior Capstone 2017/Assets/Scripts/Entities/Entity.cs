using System.Collections.Generic;
using UnityEngine;
using EntityControllers;
using Entities.Static;

namespace Entities
{
	public abstract class Entity : MonoBehaviour
	{

		public Rigidbody2D rigidBody;
		public Statistics stats;
		public Collider2D hitbox;
		public virtual float deathDuration { get { return 0f; } }

		private float attackCooldown;

		protected List<EntityController> controllers;
		protected abstract void InitializeControllers ();

		void SetPhysicsAttributes ()
		{
			rigidBody.freezeRotation = true;

			Collider2D[] colliders = GetComponentsInChildren<Collider2D> ();
			hitbox = colliders [colliders.Length - 1];

			Vector3 size = hitbox.bounds.size;
			rigidBody.mass = size.x * size.x * size.y * size.y;
			rigidBody.drag = Mathf.Pow (10, rigidBody.mass);
		}

		void SetCombatAttributes ()
		{
			attackCooldown = 0f;
		}

		public virtual void Start ()
		{
			Physics2D.IgnoreLayerCollision (6, 7);
			stats = new Statistics ();

			controllers = new List<EntityController> ();
			InitializeControllers ();

			SetPhysicsAttributes ();
			SetCombatAttributes ();
		}

		public virtual void Update ()
		{
			if (stats.hitpoints <= 0) Die ();
			if (attackCooldown > 0) attackCooldown -= Time.deltaTime;

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
			Destroy (GetComponent<Collider>());
			Destroy (gameObject, deathDuration);
		}

		public int GetDamage () {
			return stats.damage;
		}

		public void AttackIfAble (GameObject enemyGameObject) {
			if (enemyGameObject.tag != "Hitbox") return;
			if (attackCooldown > 0) return;

			attackCooldown = stats.attackSpeed;
			enemyGameObject.SendMessageUpwards ("TakeDamage", this, SendMessageOptions.DontRequireReceiver);
		}

		public void AttackIfAble (Entity otherEntity) {
			Vector3 point = otherEntity.hitbox.bounds.ClosestPoint (rigidBody.position);
			float distance = Vector2.Distance (point, rigidBody.position);
			if (distance > stats.attackRange)
				return;
			AttackIfAble (otherEntity.hitbox.gameObject);
		}

		void TakeDamage (Entity offender) {
			stats.hitpoints -= offender.GetDamage ();
		}

		public void FireProjectile (Transform prefab, Vector3 target)
		{
			Vector3 start = hitbox.bounds.center;
			Vector3 direction = start - target;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
			Vector3 rotationVector = new Vector3 (0, 0, angle);

			Transform projectile = MonoBehaviour.Instantiate (prefab, start, Quaternion.identity);
			projectile.transform.rotation = Quaternion.Euler (rotationVector);

			Physics2D.IgnoreCollision (hitbox, projectile.gameObject.GetComponent<Projectile> ().hitbox);
		}
	}
}