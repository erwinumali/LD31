using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelInstantiationManager : MonoBehaviour {

	public readonly int TUNNEL_RENDER_NEAREST_Y = 1;
	public readonly int TUNNEL_RENDER_FARTHEST_Y = -100;
	
	public GameObject tunnelSegment = null;
	public int segmentAmount = 6;
	public float unitsPerSecond = 50.0f;

	private GameObject gameCamera;
	private List<GameObject> segmentList = new List<GameObject>();
	private GameObject currTunnelSegment;
	private float segmentSeparation;
	private Vector3 temp;
	private Vector3 initPos;
	// Use this for initialization
	void Start () {
		if(tunnelSegment == null){
			Debug.LogError("TunnelSegment not set!");
		}
		
		gameCamera = GameObject.Find("Main Camera");
		//segmentList = new ArrayList<GameObject>(segmentAmount);
		
		segmentSeparation = (float)TUNNEL_RENDER_FARTHEST_Y / (float)segmentAmount;
		for(int i = 0; i < segmentAmount; i++){
			initPos = new Vector3(	gameCamera.transform.position.x, 
									gameCamera.transform.position.y + segmentSeparation * i, 
									gameCamera.transform.position.z	);
			segmentList.Add((GameObject)GameObject.Instantiate(tunnelSegment, initPos, Quaternion.identity));
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	/*	if (segmentList.Count > 0) {
			for(int i = 0; i < segmentList.Count; i++){
				currTunnelSegment = segmentList[i]; 
				if(gameCamera.transform.position.y - currTunnelSegment.transform.position.y <= TUNNEL_RENDER_NEAREST_Y){
					RepositionObjectToY(currTunnelSegment, gameCamera.transform.position.y + segmentSeparation * i);
				}
								
				RepositionObjectToY(currTunnelSegment, currTunnelSegment.transform.position.y + (unitsPerSecond * Time.deltaTime));
			}
		} else {
			Debug.LogError("TunnelSegment list was not instantiated properly");
		}
	*/
	}
	
	public float GetSegmentSeparation(){
		return segmentSeparation;
	}
	
	public GameObject GetGameCamera(){
		return gameCamera;
	}	
	
	private void RepositionObjectToY(GameObject go, float y){
		go.transform.position = new Vector3(go.transform.position.x, y, go.transform.position.x);
	}
	

}
