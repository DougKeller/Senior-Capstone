using UnityEngine;
using Entities.Animated;

namespace EntityControllers.Animated
{
  public abstract class EntityAnimatedController : EntityController
  {
    public EntityAnimatedController (EntityAnimated parent) : base (parent) {}

    protected new EntityAnimated entity {
      get { return (EntityAnimated)base.entity; }
    }
  }
}