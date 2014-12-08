using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

	public float enemySpawnRate = 0.2f;
	public int initialMaxSpawn = 10;
	public float difficultyIncreaseInterval = 60.0f;
	public float enemySpawnIncreaseRate = 0.3f;
	public float maxSpawnIncreaseRate = 0.3f;

	public GameObject enemy;

	private float spawnTick = 1.0f;
	private float gameTimer;
	private float difficultyTimer;
	
	private int enemyCounter;
	private int enemySpawnedCounter;
	
	private GameObject[] enemyArray;
	private Enemy[] enemyComponentArray;
	private Vector3 enemyPoolPosition = new Vector3(0, 30.0f, 0);
	
	private readonly float TOLERANCE = 0.01f;
	// Use this for initialization
	void Start () {
	
		if(enemy == null){
			throw new UnityException("no enemy prefab set!");
		}
		
		gameTimer = 0;
		difficultyTimer = 0;
		enemyCounter = 0;
		enemySpawnedCounter = 0;
		
		enemyArray = new GameObject[(int)(initialMaxSpawn * 3)];
		enemyComponentArray = new Enemy[enemyArray.Length];
		
		for(int i = 0; i < enemyArray.Length; i++){
			enemyArray[i] = (GameObject)GameObject.Instantiate(enemy, enemyPoolPosition, Quaternion.identity);
			enemyComponentArray[i] = enemyArray[i].GetComponent<Enemy>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		gameTimer += Time.deltaTime;
		difficultyTimer += Time.deltaTime;
		
		if(Mathf.Repeat(gameTimer, spawnTick) < TOLERANCE){
			Debug.Log("enemy tick! Enemies on field = " + enemySpawnedCounter + " vs max = " + initialMaxSpawn);
			if(Random.Range(0, 1.0f) <= enemySpawnRate && enemySpawnedCounter < initialMaxSpawn){
				while(enemyComponentArray[enemyCounter % enemyComponentArray.Length].isAlive){
					enemyCounter += 1;
				}
				
				Debug.Log("spawn tick success! Used mob on slot " + (enemyCounter % enemyComponentArray.Length));
				
				enemyArray[enemyCounter % enemyComponentArray.Length].transform.position = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
				enemyComponentArray[enemyCounter % enemyComponentArray.Length].Spawn();
				enemySpawnedCounter += 1;
			}
		}
		
		if(difficultyTimer >= difficultyIncreaseInterval){
			Debug.Log("Difficulty increase!");
			enemySpawnRate *= (1 + enemySpawnIncreaseRate);
			initialMaxSpawn = (int) ((float)initialMaxSpawn * (1 + maxSpawnIncreaseRate));
			difficultyTimer = 0;
		}
	}
	
	public void ReduceEnemyCountBy(int amount){
		if(enemySpawnedCounter - amount >= 0){
			enemySpawnedCounter -= amount;
		} else {
			Debug.LogError("Enemy count attempted to go below zero! Ignored");
		}
	}	
}
