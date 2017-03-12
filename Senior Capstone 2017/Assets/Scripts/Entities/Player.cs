using EntityControllers.PlayerControllers;

namespace Entities
{
	public class Player : Entities.Entity
	{
		public override float deathDuration { get { return 2f; } }

		protected override void InitializeControllers ()
		{
			controllers.Add (new Movement (this));
			controllers.Add (new Combat (this));
		}
	}
}
