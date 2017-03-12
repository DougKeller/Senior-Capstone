using EntityControllers.SnakeControllers;

namespace Entities
{
	public class Snake : Entities.Entity
	{
		protected override void InitializeControllers ()
		{
			controllers.Add (new Movement (this));
			controllers.Add (new Combat (this));
		}
	}
}
