using EntityControllers.Animated.PlayerControllers;
using UnityEngine;

namespace Entities.Animated
{
	public class Player : EntityAnimated
	{
		public Transform arrowPrefab;
		public override float deathDuration { get { return 2f; } }

		protected override void InitializeControllers ()
		{
			controllers.Add (new Movement (this));
			controllers.Add (new Combat (this));
		}
	}
}
