using UnityEngine;
using System.Collections;

public class TunnelBorderMover : MonoBehaviour {

	private string timName = "_TunnelManager";
	private TunnelInstantiationManager tim;
	private float unitsPerSecond;
	private GameObject gameCamera;

	// Use this for initialization
	void Start () {
		tim = GameObject.Find(timName).GetComponent<TunnelInstantiationManager>();
		
		if(tim == null){
			Debug.LogError(timName + " not found");
		}
		
		unitsPerSecond = tim.unitsPerSecond;
		gameCamera = tim.GetGameCamera();
	}
	
	// Update is called once per frame
	void Update () {
		unitsPerSecond = tim.unitsPerSecond;
		
		if(gameCamera.transform.position.y - transform.position.y <= tim.TUNNEL_RENDER_NEAREST_Y){
			RepositionObjectToY(this.gameObject, gameCamera.transform.position.y + tim.TUNNEL_RENDER_FARTHEST_Y);
			
		}
		
		RepositionObjectToY(this.gameObject, transform.position.y + (unitsPerSecond * Time.deltaTime));
	
	}
	
	private void RepositionObjectToY(GameObject go, float y){
		go.transform.position = new Vector3(go.transform.position.x, y, go.transform.position.x);
	}
}
