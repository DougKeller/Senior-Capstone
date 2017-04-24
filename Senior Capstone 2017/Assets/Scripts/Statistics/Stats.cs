using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Statistics
{
  public class Stats : MonoBehaviour
  {
    public Entity entity;

    public int hitpoints;
    public float speed;
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public float stamina;

		private int experience;

    private float attackCooldown;
    private float healthProgress;
    private float healthRegen;

    public Stats ()
    {
      stamina = 100f;
      healthProgress = 0f;
      healthRegen = 1 / 10f;

			experience = 0;

			hitpoints = MaxHitpoints ();
    }

    public bool CanAttack ()
    {
      return attackCooldown <= 0f;
    }

    public void Attack ()
    {
      attackCooldown = attackSpeed;
    }

    void RegenerateHealth ()
    {
			if (hitpoints < MaxHitpoints ()) {
        healthProgress += healthRegen * Time.deltaTime;
        if (healthProgress >= 1f) {
          healthProgress = 0f;
          hitpoints++;
        }
      } else {
        healthProgress = 0f;
      }
    }

    void Update ()
    {
      if (hitpoints <= 0) entity.Die ();
      if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
      RegenerateHealth ();
    }

		public int MaxHitpoints ()
		{
			return 10 + CombatLevel () - 1;
		}

		public void GiveExperience (int amount)
		{
			experience += amount;
		}

		public int ExperienceForKill ()
		{
			return MaxHitpoints () * 4;
		}

		public int TotalExperience ()
		{
			return experience;
		}

		public int CombatLevel ()
		{
			return Mathf.FloorToInt (Mathf.Sqrt(TotalExperience () / 20) + 1);
		}

		public int ExperienceForLevel (int level)
		{
			return Mathf.FloorToInt (Mathf.Pow ((level - 1), 2)) * 20;
		}

		public float PercentOfLevelComplete ()
		{
			int currentLevelExperience = ExperienceForLevel (CombatLevel ());
			int nextLevelExperience = ExperienceForLevel (CombatLevel () + 1);
			int experienceBeyondCurrent = TotalExperience () - currentLevelExperience;
			int experienceThisLevel = nextLevelExperience - currentLevelExperience;

			float amt = (float) experienceBeyondCurrent / experienceThisLevel;
			return amt;
		}
  }
}