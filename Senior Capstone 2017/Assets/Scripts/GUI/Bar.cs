using UnityEngine;
using UnityEngine.UI;
using Entities;

namespace GUI
{
  class Bar : MonoBehaviour
  {
		public GameObject target;
		public int property;

		float FillAmount ()
		{
			if (!target)
				return 0f;
			
			Statistics stats = target.GetComponent<Statistics> ();
			switch (property) {
			case 1:
				return stats.hitpoints / (float)stats.maxHitpoints;
			case 2:
				return stats.stamina / 100f;
			}
			return 0f;
		}

    void Update ()
    {
			GetComponent<Image> ().fillAmount = FillAmount ();
    }
  }
}