using UnityEngine;
using Entities.Static;

namespace EntityControllers.Static
{
  public abstract class EntityStaticController : EntityController
  {
    public EntityStaticController (EntityStatic parent) : base (parent) {}

    protected new EntityStatic entity {
      get { return (EntityStatic)base.entity; }
    }
  }
}