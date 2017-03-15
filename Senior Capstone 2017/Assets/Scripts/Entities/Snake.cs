using EntityControllers.SnakeControllers;
using UnityEngine;

namespace Entities
{
	public class Snake : MonoBehaviour
	{
    public Entity entity;
		public float distance;

    void Start ()
    {
      distance = 5f;
    }
	}
}
