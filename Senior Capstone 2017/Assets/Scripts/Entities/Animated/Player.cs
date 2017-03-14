using EntityControllers.Animated.PlayerControllers;

namespace Entities.Animated
{
	public class Player : EntityAnimated
	{
		public override float deathDuration { get { return 2f; } }

		protected override void InitializeControllers ()
		{
			controllers.Add (new Movement (this));
			controllers.Add (new Combat (this));
		}
	}
}
