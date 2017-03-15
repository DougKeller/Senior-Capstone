using UnityEngine;

namespace GUI
{
  class Bar : MonoBehaviour
  {
		public GameObject target;
    public GameObject backLeft, backMiddle, backRight;
		public GameObject barLeft, barMiddle, barRight;
		private Entities.Statistics stats;
		private SpriteRenderer blSprite, bmSprite, brSprite;
		private SpriteRenderer lSprite, mSprite, rSprite;

		void Start ()
		{
			stats = target.GetComponent<Entities.Statistics> ();
			blSprite = backLeft.GetComponent<SpriteRenderer> ();
			bmSprite = backMiddle.GetComponent<SpriteRenderer> ();
			brSprite = backRight.GetComponent<SpriteRenderer> ();
			lSprite = barLeft.GetComponent<SpriteRenderer> ();
			mSprite = barMiddle.GetComponent<SpriteRenderer> ();
			rSprite = barRight.GetComponent<SpriteRenderer> ();
		}

		Bounds BoundsFor (GameObject obj)
		{
			return obj.GetComponent<SpriteRenderer> ().bounds;
		}

    void Update ()
    {
			float length = stats.hitpoints * 8.5f / stats.maxHitpoints;
			barMiddle.transform.localScale = new Vector3 (length, 1, 1);
    }
  }
}