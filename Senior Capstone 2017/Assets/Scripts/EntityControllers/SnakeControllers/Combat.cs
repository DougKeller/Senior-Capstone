using UnityEngine;
using Entities;

namespace EntityControllers.SnakeControllers
{
	public class Combat : MonoBehaviour
	{
		public Entity entity;
		void Start ()
		{
			entity.stats.hitpoints = 10;
			entity.stats.attackRange = 0.5f;
			entity.stats.attackSpeed = 0.5f;
			entity.stats.damage = 1;
		}
	}
}