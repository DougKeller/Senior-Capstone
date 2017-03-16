using UnityEngine;
using UnityEngine.UI;
using Entities;

namespace GUI
{
  class Bar : MonoBehaviour
  {
		public GameObject target;

    public Image healthBar;
    public Image staminaBar;

    float HealthAmount ()
    {
      if (!target) return 0f;
			Statistics stats = target.GetComponent<Statistics> ();
			return stats.hitpoints / (float)stats.maxHitpoints;
    }

    float StaminaAmount ()
    {
      if (!target) return 0f;
      Statistics stats = target.GetComponent<Statistics> ();
			return stats.stamina / 100f;
    }

    void Update ()
    {
			healthBar.fillAmount = HealthAmount ();
      staminaBar.fillAmount = StaminaAmount ();
    }
  }
}