using System.Collections.Generic;
using UnityEngine;
using EntityControllers;

namespace Entities
{
	public class Statistics
	{
		public int hitpoints;
		public float speed;
		public int damage;
		public float attackRange;
		public float attackSpeed;

		public Statistics ()
		{
			hitpoints = 10;
			speed = 3f;
			damage = 1;
			attackRange = 0f;
			attackSpeed = 0f;
		}
	}
}