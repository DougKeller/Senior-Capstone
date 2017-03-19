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
    public float stamina;

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
      RegenerateHealth ();
    }
  }
}