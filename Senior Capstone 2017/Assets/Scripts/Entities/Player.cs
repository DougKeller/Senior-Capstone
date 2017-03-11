using EntityControllers.PlayerControllers;

namespace Entities {
  public class Player : Entities.Entity {
    protected override void InitializeControllers() {
      controllers.Add (new Movement (this));
      controllers.Add (new Combat (this));
    }
  }
}
