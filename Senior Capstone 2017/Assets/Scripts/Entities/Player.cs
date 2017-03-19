using EntityControllers.PlayerControllers;
using UnityEngine;

namespace Entities
{
	public class Player : MonoBehaviour
	{
    public Entity entity;
		public Transform arrowPrefab;

		void Start ()
		{
			entity.deathDuration = 2f;
			entity.combat.currentWeapon = new Items.Weapons.Bow (this);
		}


		//        Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		//        Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		//        entity.combat.FireProjectile (GetComponent<Player> ().arrowPrefab, targetPosition);
		//        entity.combat.Attack ();
	}
}
