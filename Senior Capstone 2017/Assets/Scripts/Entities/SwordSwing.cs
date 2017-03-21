using UnityEngine;

namespace Entities
{
	public class SwordSwing : MonoBehaviour
	{
		public Entity entity;
		public new Collider2D collider;

		public void SetPositionAndRotation ()
		{
		}

		void Start () {
			Invoke ("Die", 0.25f);
			SetPositionAndRotation ();
		}

		void Update () {
			SetPositionAndRotation ();
		}

		void Die ()
		{
			entity.Die ();
		}

		void OnTriggerEnter2D (Collider2D collider) {
			entity.AttackIfAble (collider.gameObject);
			entity.Die ();
		}
	}
}
