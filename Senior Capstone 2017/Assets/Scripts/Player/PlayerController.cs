public abstract class PlayerController : EntityController {
	protected Player player {
		get { return (Player) entity; }
	}

  public PlayerController(Player parent) : base(parent) {}
}
