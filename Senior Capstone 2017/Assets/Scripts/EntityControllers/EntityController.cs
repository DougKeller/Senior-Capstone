using UnityEngine;
using Entities;

namespace EntityControllers
{
	public abstract class EntityController
	{
		private Entity _entity;

		protected Entity entity {
			get { return _entity; }
		}

		public EntityController (Entity parent)
		{
			_entity = parent;
		}

		public virtual void Update () {}
		public virtual void OnCollisionEnter2D (Collision2D collision) {}
		public virtual void OnTriggerEnter2D (Collider2D collision) {}
	}
}
