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
		public Image healthBackBar;
		public Image staminaBackBar;

    float HealthAmount ()
    {
      if (!target) return 0f;
			Statistics.Stats stats = target.GetComponent<Statistics.Stats> ();
			return stats.hitpoints / (float)stats.maxHitpoints;
    }

    float StaminaAmount ()
    {
      if (!target) return 0f;
      Statistics.Stats stats = target.GetComponent<Statistics.Stats> ();
			return stats.stamina / 100f;
    }

    void Update ()
		{
			float healthAmount = HealthAmount ();
			float healthDelta1 = (healthBar.fillAmount - healthAmount) * Time.deltaTime;
			float healthDelta2 = (healthBackBar.fillAmount - healthAmount) * Time.deltaTime;
			if (healthDelta1 < 0) {
				healthDelta1 *= 2f;
				healthDelta2 *= 5f;
			} else {
				healthDelta1 *= 5f;
				healthDelta2 *= 2f;
			}
			healthBar.fillAmount -= healthDelta1;
			healthBackBar.fillAmount -= healthDelta2;

			float staminaAmount = StaminaAmount ();
			float staminaDelta1 = (staminaBar.fillAmount - staminaAmount) * Time.deltaTime;
			float staminaDelta2 = (staminaBackBar.fillAmount - staminaAmount) * Time.deltaTime;
			if (staminaDelta1 < 0) {
				staminaDelta1 *= 2f;
				staminaDelta2 *= 5f;
			} else {
				staminaDelta1 *= 5f;
				staminaDelta2 *= 2f;
			}
			staminaBar.fillAmount -= staminaDelta1;
			staminaBackBar.fillAmount -= staminaDelta2;
    }
  }
}