using UnityEngine;
using System.Collections;

public class TunnelMoveManager : MonoBehaviour {

	public static int TUNNEL_RENDER_NEAREST_Y = 1;
	public static int TUNNEL_RENDER_FARTHEST_Y = -100;

	public GameObject tunnelSegment = null;
	public float unitsPerSecond = 50.0f;

	private Vector3 temp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (tunnelSegment != null) {
			if(TUNNEL_RENDER_NEAREST_Y - tunnelSegment.transform.position.y <= 1){
				RepositionObjectToY(tunnelSegment, TUNNEL_RENDER_FARTHEST_Y);
			}
							
			RepositionObjectToY(tunnelSegment, tunnelSegment.transform.position.y + (unitsPerSecond * Time.deltaTime));
			
		} else {
			Debug.LogError("No tunnelSegment assigned");
		}
	}
	
	private void RepositionObjectToY(GameObject go, float y){
		go.transform.position = new Vector3(go.transform.position.x, y, go.transform.position.x);
	}
}
