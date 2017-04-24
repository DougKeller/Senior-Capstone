using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	public Transform player;
	public Transform[] entities;

	float waveLength;
	float waveCooldown;

	void Start () {
		waveLength = 10f;
		waveCooldown = 0f;
	}

	public int TimeToNextWave ()
	{
		return Mathf.CeilToInt(waveLength - waveCooldown);
	}

	Vector3 RandomLocationFromPlayer (float distance)
	{
		float randomX = Random.value - 0.5f;
		float randomY = Random.value - 0.5f;
		Vector3 direction = new Vector3 (randomX, randomY, 0f);
		return player.transform.position + direction.normalized * distance;
	}

	void SpawnWave ()
	{
		Transform entity = entities [0];
		Vector3 position = RandomLocationFromPlayer (5f);
		MonoBehaviour.Instantiate (entity, position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (waveCooldown > waveLength) {
			waveCooldown = 0f;
			SpawnWave ();
		} else {
			waveCooldown += Time.deltaTime;
		}
	}
}
