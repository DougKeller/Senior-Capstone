using UnityEngine;
using Entities;

namespace Items.Weapons {
	public class Weapon {
		public int baseDamage;
		public float attackSpeed;
		public float attackRange;

		public Entity wielder;

		private float attackCooldown;

		public Weapon (Entity entity)
		{
			wielder = entity;
		}

		public void Update () {
			if (attackCooldown > 0) {
				attackCooldown -= Time.deltaTime;
			}
		}

		public bool CanAttack () {
			return attackCooldown <= 0;
		}

		public void Attack () {
			attackCooldown = attackSpeed;
		}

		public void AttackIfAble (GameObject enemyGameObject) {
			if (enemyGameObject.tag != "Hitbox") return;
			if (!CanAttack ()) return;

			enemyGameObject.SendMessageUpwards ("TakeDamage", this, SendMessageOptions.DontRequireReceiver);
		}

		public void AttackIfAble (Entity otherEntity) {
			Vector3 point = otherEntity.hitbox.bounds.ClosestPoint (wielder.rigidBody.position);
			float distance = Vector2.Distance (point, wielder.hitbox.bounds.center);
			if (distance > attackRange)
				return;
			AttackIfAble (otherEntity.hitbox.gameObject);
		}

		public void AttackIfAble () {
			if (!CanAttack ()) return;
			// create instance of attack thingy
		}

		public void FireProjectile (Transform prefab, Vector3 target)
		{
			Vector3 start = wielder.hitbox.bounds.center;
			Vector3 direction = start - target;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
			Vector3 rotationVector = new Vector3 (0, 0, angle);

			Transform projectile = MonoBehaviour.Instantiate (prefab, start, Quaternion.identity);
			projectile.transform.rotation = Quaternion.Euler (rotationVector);

			Physics2D.IgnoreCollision (wielder.hitbox, projectile.gameObject.GetComponent<Projectile> ().collider);
		}
	}
}
