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
	// Use this for initialization
	void Start () {
		initialHP = hp;
		resetPosition = new Vector3(0, 20.0f, 0);
		poolPosition = new Vector3(0, 25.0f, 0);
		//Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0 && isAlive){
			Die();
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
		
	}
}
