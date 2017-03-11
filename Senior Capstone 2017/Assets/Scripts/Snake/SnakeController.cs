public abstract class SnakeController : EntityController {
  protected Snake snake {
    get { return (Snake) entity; }
  }

  public SnakeController(Snake parent) : base(parent) {}
}
