using EntityControllers.PlayerControllers;
using UnityEngine;

namespace Entities
{
	public class Player : MonoBehaviour
	{
    public Entity entity;
		public Transform arrowPrefab;

		void Start ()
		{
			entity.deathDuration = 2f;
		}
	}
}
