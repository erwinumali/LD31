using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]

public class Bullet : MonoBehaviour {

	public bool isFiring;
	public float damage = 1.0f;
	
	public Vector3 direction = Vector3.zero;
	public Quaternion rotationSpeed = Quaternion.identity;
	public float speed = 30.0f;
	
	public float maxDuration = 1.0f;
	public float timeAlive;

	// Use this for initialization
	void Start () {
	
		isFiring = false;
		damage = 1.0f;
		timeAlive = 0.0f;
		//rigidbody.velocity = Vector3.zero;
		collider.isTrigger = true;
		transform.rotation = Random.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFiring){
			collider.isTrigger = false;
			//Debug.Log(direction + ", " + Input.mousePosition);
			timeAlive += Time.deltaTime;
			transform.position = transform.position + ((direction * speed) * Time.deltaTime);
			//this.rigidbody.AddForce(direction);
			//transform.rotation = Quaternion.Dot(transform.rotation, rotationSpeed);
			if(timeAlive > maxDuration){
				Reset ();
			}
		}
	}
	
	public void Reset(){
		isFiring = false;
		timeAlive = 0.0f;
		transform.position = new Vector3(0, 20.0f, 0);
		//rigidbody.velocity = Vector3.zero;
		collider.isTrigger = true;
		transform.rotation = Random.rotation;
	}
	
}
