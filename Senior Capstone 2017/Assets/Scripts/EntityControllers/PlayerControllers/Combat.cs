using UnityEngine;
using Entities;

namespace EntityControllers.PlayerControllers
{
	public class Combat : MonoBehaviour
	{
		public Entity entity;

		void Update ()
		{
			if (Input.GetButtonDown("Fire1")) {
				Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				Debug.Log ("Fired");
				entity.FireProjectile (GetComponent<Player> ().arrowPrefab, targetPosition);
			}
		}
	}
}