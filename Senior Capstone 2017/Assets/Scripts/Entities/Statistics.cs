using UnityEngine;

namespace Entities
{
	public class Statistics : MonoBehaviour
	{
		public Entity entity;

		public int hitpoints;
		public float speed;
		public int damage;
		public float attackRange;
		public float attackSpeed;
		private float attackCooldown;

		public Statistics ()
		{
			hitpoints = 10;
			speed = 3f;
			damage = 1;
			attackRange = 0f;
			attackSpeed = 0f;
			attackCooldown = 0f;
		}

		public bool CanAttack ()
		{
			return attackCooldown <= 0f;
		}

		public void Attack ()
		{
			attackCooldown = attackSpeed;
		}

		void Update ()
		{
			if (hitpoints <= 0) entity.Die ();
			if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
		}
	}
}