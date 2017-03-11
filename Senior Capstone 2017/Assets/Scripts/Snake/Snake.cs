public class Snake : Entity {
  protected override void InitializeControllers() {
    controllers.Add (new SnakeControllerMovement (this));
    controllers.Add (new SnakeControllerCombat (this));
  }
}
