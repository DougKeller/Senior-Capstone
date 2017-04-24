using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Statistics;

public class MonsterSpawner : MonoBehaviour {

	public Transform player;
	public Transform[] entities;
	public Text countdownText;
	public List<Transform> currentlySpawnedEnemies;

	float waveLength;
	float waveCooldown;
	int currentWave;

	void Start () {
		waveLength = 5f;
		waveCooldown = 0f;
		currentWave = 0;
		currentlySpawnedEnemies = new List<Transform> ();
	}

	private bool DeadEnemies (Transform entity)
	{
		return entity == null;
	}

	void RemoveDeadEnemies ()
	{
		currentlySpawnedEnemies.RemoveAll (DeadEnemies);
	}

	bool EnemiesAreAlive ()
	{
		return currentlySpawnedEnemies.Count > 0;
	}

	Vector3 RandomLocationFromPlayer (float distance)
	{
		float randomX = Random.value - 0.5f;
		float randomY = Random.value - 0.5f;
		Vector3 direction = new Vector3 (randomX, randomY, 0f);
		return player.transform.position + direction.normalized * distance;
	}

	List<int> EnemiesForPlayer ()
	{
		Stats playerStats = player.GetComponent<Stats> ();
		List<int> enemies = new List<int> ();
		enemies.Add (playerStats.CombatLevel ());
		return enemies;
	}

	void SpawnWave ()
	{
		List<int> enemyCounts = EnemiesForPlayer ();
		for (int i = 0; i < enemyCounts.Count; i++) {
			int count = enemyCounts [i];
			Transform entity = entities [i];

			for (int j = 0; j < count; j++) {
				Vector3 position = RandomLocationFromPlayer (5f);
				Transform enemy = MonoBehaviour.Instantiate (entity, position, Quaternion.identity);
				currentlySpawnedEnemies.Add (enemy);
			}
		}
	}

	int TimeToNextWave ()
	{
		return Mathf.CeilToInt(waveLength - waveCooldown);
	}
	
	// Update is called once per frame
	void Update ()
	{
		RemoveDeadEnemies ();

		if (EnemiesAreAlive ()) {
			countdownText.text = "Wave: " + currentWave;
			return;
		}

		if (waveCooldown > waveLength) {
			waveCooldown = 0f;
			currentWave++;
			SpawnWave ();
		} else {
			waveCooldown += Time.deltaTime;
		}

		countdownText.text = "" + TimeToNextWave () + " Seconds Until Next Wave...";
	}
}
