using System.Collections.Generic;

namespace Statistics
{
  public class Skill
  {
    private bool hasLeveledUp;
    public Type type;
    private float experience;

    public int level
    {
      get {
        return (int)experience / 10;
      }
    }

    public enum Type {
      Constitution, Strength
    }

    public Skill (Type skillType)
    {
      type = skillType;
      hasLeveledUp = false;
      experience = 0f;
    }

    public void GiveExperience (float amount)
    {
      int previousLevel = level;
      experience += amount;
      if (level > previousLevel) {
        hasLeveledUp = true;
      }
    }

    public bool CheckForLevelUp ()
    {
      bool result = hasLeveledUp;
      hasLeveledUp = false;
      return result;
    }

    public static SortedDictionary<Type, Skill> GenerateMap ()
    {
      SortedDictionary<Type, Skill> map = new SortedDictionary<Type, Skill> ();
      map[Type.Constitution] = new Skill(Type.Constitution);
      map[Type.Strength] = new Skill(Type.Strength);
      return map;
    }
  }
}