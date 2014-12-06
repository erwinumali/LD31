using UnityEngine;
using System.Collections;

public class PlayerTurretManager : MonoBehaviour {

	private Vector3 pos;
	private Vector3 dir;
	private float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.mousePosition + ", " + Camera.main.pixelWidth + "," + Camera.main.pixelHeight);
		if(	Input.mousePosition.x < Camera.main.pixelWidth && 
			Input.mousePosition.y < Camera.main.pixelHeight){
			
			pos = Camera.main.WorldToScreenPoint(transform.position);
			dir = Input.mousePosition - pos;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.down); 
		}
		
		
	}
}
