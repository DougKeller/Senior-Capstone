using UnityEngine;
using Items.Weapons;

namespace Entities {
	public class Combat : MonoBehaviour {
		public Entity entity;
		public Weapon currentWeapon;

		public int GetDamage () {
			return currentWeapon.baseDamage + entity.stats.damage;
		}

		void Update ()
		{
			if (currentWeapon == null) return;

			if (Input.GetButton ("Fire1")) {
				currentWeapon.AttackIfAble ();
			}
		}

		void TakeDamage (Entity offender)
		{
			entity.stats.hitpoints -= offender.combat.GetDamage ();
		}
	}
}