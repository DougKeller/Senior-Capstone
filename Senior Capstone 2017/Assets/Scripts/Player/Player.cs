public class Player : Entity {
	void Start () {
		AddController (new PlayerControllerMovement (this));
		AddController (new PlayerControllerCombat (this));
	}
}
