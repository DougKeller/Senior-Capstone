using UnityEngine;

namespace Entities
{
	public class Sword : MonoBehaviour
	{
		public Entity entity;
		public new Collider2D collider;

		public void SetPositionAndRotation ()
		{
			transform.localPosition = transform.up + new Vector3 (1f, -1.25f, 0);
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
		}
	}
}
