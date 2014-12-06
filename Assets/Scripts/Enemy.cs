using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

	public float hp = 20.0f;
	
	private Bullet bulletInfo;
	private Vector3 resetPosition;
	// Use this for initialization
	void Start () {
		resetPosition = new Vector3(0, 20.0f, 0);
		this.rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0){
			this.rigidbody.useGravity = true;
		}
	}
	
	void OnCollisionEnter(Collision col){
		Debug.Log("collide!");
		if(col.collider.tag == "bulletbasic"){
			this.hp -= 1.0f;
			col.transform.position = resetPosition;
		}
	}
}
