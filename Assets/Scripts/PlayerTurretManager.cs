using UnityEngine;
using System.Collections;

public class PlayerTurretManager : MonoBehaviour {

	public enum BulletType {basic};

	public GameObject bullet;
	public float bullet1Cooldown = 0.03f;

	private GameObject[] bulletArray;
	private int bulletInstances = 100;

	private float bullet1Timer = 0.0f;

	private Vector3 pos;
	private Vector3 dir;
	private float angle;
	
	private GameObject currentBullet;
	private Bullet currentBulletComponent;
	
	private int bulletCounter = 0;
	// Use this for initialization
	void Start () {
		if(bullet == null){
			throw new UnityException("no bullet");
		} else if(bullet.GetComponent<Bullet>() == null){
			throw new UnityException("no bullet component in prefab!");
		} else {
			bulletArray = new GameObject[bulletInstances];
			for(int i = 0; i < bulletInstances; i++){
				bulletArray[i] = (GameObject) GameObject.Instantiate(bullet, new Vector3(0.0f, 13.0f, 0.0f), Quaternion.identity);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		bullet1Timer += Time.deltaTime;
		
		// turret rotation
		//Debug.Log (Input.mousePosition + ", " + Camera.main.pixelWidth + "," + Camera.main.pixelHeight);
		if(	Input.mousePosition.x < Camera.main.pixelWidth && 
			Input.mousePosition.y < Camera.main.pixelHeight){
			
			pos = Camera.main.WorldToScreenPoint(transform.position);
			dir = Input.mousePosition - pos;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.down); 
		}
		
		if(Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)){
			if(bullet1Timer >= bullet1Cooldown){
				Fire(BulletType.basic);
				bullet1Timer = 0.0f;
			}
		}
	}
	
	private void Fire(BulletType bullet){
		if(bullet.Equals(BulletType.basic)){
			Fire ();
		}
	}
	
	private void Fire(){
		currentBullet = bulletArray[bulletCounter % bulletInstances];
		currentBulletComponent = bulletArray[bulletCounter % bulletInstances].GetComponent<Bullet>();
		
		while(currentBulletComponent.isFiring){
			bulletCounter += 1;
			if(bulletCounter > 65536){
				bulletCounter = 0;
			}
			currentBullet = bulletArray[bulletCounter % bulletInstances];
			currentBulletComponent = bulletArray[bulletCounter % bulletInstances].GetComponent<Bullet>();
		} 
		
		currentBullet.transform.position = this.transform.position;
		currentBulletComponent.direction = Vector3.Normalize(new Vector3(dir.x, 0.0f, dir.y));
		currentBulletComponent.isFiring = true;
		
		this.audio.Play();
	}
}
