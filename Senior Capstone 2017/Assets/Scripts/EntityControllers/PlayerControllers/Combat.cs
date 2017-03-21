using UnityEngine;
using UnityEngine.UI;
using Entities;

namespace EntityControllers.PlayerControllers
{
	public class Combat : MonoBehaviour
	{
		public Entity entity;
		public AttackMode attackMode;

		public Transform arrowPrefab;
		public Transform swordPrefab;

		public int numArrows;

		public Text arrowQuantity;
		public Text currentWeapon;
		public Text level;

		public enum AttackMode {
			Bow, Fire, Sword
		}

		void Start ()
		{
			numArrows = 30;
			attackMode = AttackMode.Bow;
		}

		void CheckForWeaponChange ()
		{
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				attackMode = AttackMode.Sword;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				attackMode = AttackMode.Bow;
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				attackMode = AttackMode.Fire;
			}
		}

		void SetLabels ()
		{
			arrowQuantity.text = "x" + numArrows;
			currentWeapon.text = attackMode.ToString ();
			level.text = "Level " + entity.stats.CombatLevel ();
		}

		void Update ()
		{
			if (entity.stats.hitpoints <= 0) return;
			CheckForWeaponChange ();

			if (Input.GetButton ("Fire1") && entity.stats.CanAttack ()) {
				switch (attackMode) {
				case AttackMode.Bow:
					MakeRangedAttack ();
					break;
				case AttackMode.Sword:
					MakeMeleeAttack ();
					break;
				}
			}
			SetLabels ();
		}

		public Transform InstantiatePrefabInMouseDirection (Transform prefab)
		{
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector3 target = Camera.main.ScreenToWorldPoint (mousePosition);

			Vector3 start = entity.hitbox.bounds.center;
			Vector3 direction = start - target;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
			Vector3 rotationVector = new Vector3 (0, 0, angle);

			Transform instance = MonoBehaviour.Instantiate (prefab, start, Quaternion.identity);
			instance.transform.rotation = Quaternion.Euler (rotationVector);

			return instance;
		}

		public void FireProjectile ()
		{
			Transform projectile = InstantiatePrefabInMouseDirection (arrowPrefab);
			Projectile instance = projectile.gameObject.GetComponent<Projectile> ();
			Physics2D.IgnoreCollision (entity.hitbox, instance.collider);
			instance.source = entity;
		}

		public void SwingSword ()
		{
			Transform sword = InstantiatePrefabInMouseDirection (swordPrefab);
			sword.transform.SetParent (transform);
			Sword instance = sword.gameObject.GetComponent<Sword> ();
			Physics2D.IgnoreCollision (entity.hitbox, instance.collider);
			instance.source = entity;
		}

		private void MakeMeleeAttack ()
		{
			SwingSword ();
			entity.stats.Attack ();
		}

		private void MakeRangedAttack ()
		{
			bool hasArrows = numArrows > 0;
			if (numArrows > 0) {
				FireProjectile ();
				entity.stats.Attack ();
				numArrows--;
			}
		}
	}
}