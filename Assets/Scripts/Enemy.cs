using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

	public float hp = 20.0f;
	public bool isAlive = false;
	
	private Bullet bulletInfo;
	private Vector3 resetPosition;
	private Vector3 poolPosition;
	private float initialHP;
	
	private GameObject player;
	
	private float fireDelay = 2.0f;
	private Vector3 previousPosition;
	
	private Vector3 randomPosition;
	private bool isMoving;
	// Use this for initialization
	void Start () {
		initialHP = hp;
		resetPosition = new Vector3(0, 20.0f, 0);
		poolPosition = new Vector3(0, 25.0f, 0);
		
		player = GameObject.FindGameObjectWithTag("Player");
		previousPosition = this.transform.position;
		isMoving = false;
		//Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0 && isAlive){
			Die();
		} else if (isAlive) {
		
			if(!isMoving){
				randomPosition = new Vector3(Random.Range(-2.0f, 2.0f) + previousPosition.x, 0, Random.Range(-2.0f, 2.0f) + previousPosition.z);
				//StartCoroutine(SeekAndFire());
				StartCoroutine(MoveAround(randomPosition));
			}
		}
		BoundCheck();
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "bulletbasic"){
			this.hp -= 1.0f;
			//Debug.Log("collide! " + hp);
			col.transform.position = resetPosition;
			
		}
	}
	
	IEnumerator SeekAndFire(){
		
		float i = 0;
		while(i < fireDelay){
			i += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("FIRE ENEMY");
		
	}
	
	IEnumerator MoveAround(Vector3 toPosition){
		if(Random.Range (0.0f, 1.0f) <= 1.0f){
			isMoving = true;
			float i = 0;
			previousPosition = this.transform.position;
			
			while(i < 2.0f){
				i += Time.deltaTime;
				this.transform.position = Vector3.Lerp(previousPosition, toPosition, i);
				yield return null;
			}
			isMoving = false;
		}
	}
	
	void BoundCheck(){
		if(this.transform.position.y < -80.0f){
			this.rigidbody.useGravity = false;
			this.rigidbody.velocity = Vector3.zero;
			this.rigidbody.angularVelocity = Vector3.zero;
			this.transform.position = poolPosition;
			isAlive = false;
			
			// hint: this is bad
			GameObject.Find("_EnemySpawnManager").GetComponent<EnemySpawnManager>().ReduceEnemyCountBy(1);
		}
	}
	
	public void Spawn(){
		this.rigidbody.useGravity = false;
		this.transform.rotation = Quaternion.identity;
		
		hp = initialHP;
		isAlive = true;
	}
	
	public void Die(){
		this.rigidbody.useGravity = true;
		this.rigidbody.AddTorque(Random.insideUnitSphere * 10.0f);
		//this.rigidbody.AddExplosionForce(Random.Range(10.0f, 50.0f), transform.position, 30.0f);
		this.rigidbody.AddForce(new Vector3(Random.Range(-80.0f, 80.0f), Random.Range(-80.0f, 80.0f), Random.Range(-80.0f, 80.0f)));
		
	}
}
