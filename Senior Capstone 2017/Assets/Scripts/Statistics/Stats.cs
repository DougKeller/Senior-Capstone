using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Statistics
{
  public class Stats : MonoBehaviour
  {
    public Entity entity;

    public int hitpoints;
    public int maxHitpoints;
    public float speed;
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public float stamina;

    private float attackCooldown;
    private float healthProgress;
    private float healthRegen;

    public SortedDictionary<Skill.Type, Skill> skills;

    public Stats ()
    {
      hitpoints = maxHitpoints = 10;
      stamina = 100f;
      healthProgress = 0f;
      healthRegen = 1 / 10f;

      skills = Skill.GenerateMap();
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
      if (hitpoints < maxHitpoints) {
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
  }
}