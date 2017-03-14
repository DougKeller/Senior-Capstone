using UnityEngine;

namespace Entities.Static
{
  public abstract class EntityStatic : Entities.Entity
  {

    protected abstract override void InitializeControllers ();

    public override void Start ()
    {
      base.Start ();
    }

    public override void Die ()
    {
      base.Die();
    }
  }
}
