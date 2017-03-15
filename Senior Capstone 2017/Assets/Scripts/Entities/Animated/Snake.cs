using EntityControllers.Animated.SnakeControllers;

namespace Entities.Animated
{
	public class Snake : EntityAnimated
	{
		public float distance;

		protected override void InitializeControllers ()
		{
			controllers.Add (new Movement (this));
			controllers.Add (new Combat (this));
		}
	}
}
