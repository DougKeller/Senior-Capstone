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

		public void FireProjectile (Transform prefab, Vector3 target)
		{
			Vector3 start = entity.hitbox.bounds.center;
			Vector3 direction = start - target;

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
			Vector3 rotationVector = new Vector3 (0, 0, angle);

			Transform projectile = MonoBehaviour.Instantiate (prefab, start, Quaternion.identity);
			projectile.transform.rotation = Quaternion.Euler (rotationVector);

			Physics2D.IgnoreCollision (entity.hitbox, projectile.gameObject.GetComponent<Projectile> ().collider);
		}

		public void SwingSword ()
		{
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector3 target = Camera.main.ScreenToWorldPoint (mousePosition);

			Vector3 start = entity.hitbox.bounds.center;
			Vector3 direction = start - target;

			Transform projectile = MonoBehaviour.Instantiate (swordPrefab, start, Quaternion.identity);
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 180;
			Vector3 rotationVector = new Vector3 (0, 0, angle);
			projectile.transform.rotation = Quaternion.Euler (rotationVector);

			projectile.transform.SetParent (transform, false);

			Physics2D.IgnoreCollision (entity.hitbox, projectile.gameObject.GetComponent<SwordSwing> ().collider);
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
				Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				FireProjectile (arrowPrefab, targetPosition);
				entity.stats.Attack ();
				numArrows--;
			}
		}
	}
}