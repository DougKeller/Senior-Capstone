using UnityEngine;

public abstract class EntityController {
  private Entity _entity;
  protected Entity entity {
    get { return _entity; }
  }

  public EntityController (Entity parent) {
    _entity = parent;
  }

  public abstract void Update ();
  public abstract void OnCollisionEnter2D (Collision2D collision);
}
