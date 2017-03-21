using UnityEngine;

namespace Entities
{
	public class Sword : MonoBehaviour
	{
		public Entity entity;
		public new Collider2D collider;
		public Entity source;

		public void SetPositionAndRotation ()
		{
			transform.localPosition = transform.up + new Vector3 (1f, -1.25f, 0);
		}

		void Start () {
			Invoke ("Die", 0.35f);
			SetPositionAndRotation ();
		}

		void Update () {
			SetPositionAndRotation ();
		}

		void Die ()
		{
			entity.Die ();
			if (source != null) {
				source.stats.GiveExperience (entity.stats.TotalExperience ());
			}
		}

		void OnTriggerEnter2D (Collider2D collider) {
			entity.AttackIfAble (collider.gameObject);
		}
	}
}
