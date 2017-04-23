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
    public Image experienceBar;
    public Image healthBackBar;
    public Image staminaBackBar;
    public Image experienceBackBar;

    float HealthAmount ()
    {
      if (!target) return 0f;
      Statistics.Stats stats = target.GetComponent<Statistics.Stats> ();
      return stats.hitpoints / (float)stats.MaxHitpoints ();
    }

    float StaminaAmount ()
    {
      if (!target) return 0f;
      Statistics.Stats stats = target.GetComponent<Statistics.Stats> ();
      return stats.stamina / 100f;
    }

    float ExperienceAmount ()
    {
      if (!target) return 0f;
      Statistics.Stats stats = target.GetComponent<Statistics.Stats> ();
      return stats.PercentOfLevelComplete ();
    }

    void updateBar(Image backBar, Image frontBar, float amount)
    {
      float delta1 = (frontBar.fillAmount - amount) * Time.deltaTime;
      float delta2 = (backBar.fillAmount - amount) * Time.deltaTime;
      if (delta1 < 0) {
        delta1 *= 2f;
        delta2 *= 5f;
      } else {
        delta1 *= 5f;
        delta2 *= 2f;
      }
      frontBar.fillAmount -= delta1;
      backBar.fillAmount -= delta2;
    }

    void Update ()
    {
      updateBar(healthBackBar, healthBar, HealthAmount ());
      updateBar(staminaBackBar, staminaBar, StaminaAmount ());
      updateBar(experienceBackBar, experienceBar, ExperienceAmount ());
    }
  }
}