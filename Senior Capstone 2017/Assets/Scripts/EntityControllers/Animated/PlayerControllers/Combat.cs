using UnityEngine;
using Entities.Animated;
using Entities.Static;

namespace EntityControllers.Animated.PlayerControllers
{
	public class Combat : EntityAnimatedController
	{
		public Combat (Player parent) : base (parent) {}

		override public void Update ()
		{
			if (Input.GetButtonDown("Fire1")) {
				Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				entity.FireProjectile (((Player)entity).arrowPrefab, targetPosition);
			}
		}
	}
}