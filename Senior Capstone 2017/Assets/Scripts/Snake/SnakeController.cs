public abstract class SnakeController : EntityController {
  protected Snake snake {
    get { return entity; }
  }

  public SnakeController(Snake parent) : base(parent) {}
}
