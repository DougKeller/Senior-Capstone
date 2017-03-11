public class Player : Entity {
  protected override void InitializeControllers() {
    controllers.Add (new PlayerControllerMovement (this));
    controllers.Add (new PlayerControllerCombat (this));
  }
}
