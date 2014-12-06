using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum PlayerDirection {up, down, left, right, center};

	public float snapSpeed = 20.0f; //units per second

	private PlayerDirection currentPosition;
	
	public GameObject northPos;
	public GameObject southPos;
	public GameObject eastPos;
	public GameObject westPos;
	public GameObject centerPos;
	
	// Use this for initialization
	void Start () {
		if(northPos == null || southPos == null || eastPos == null || westPos == null || centerPos == null){
			throw new UnityException("incomplete snap points");
		}
		currentPosition = PlayerDirection.center;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
			MoveTo(currentPosition, PlayerDirection.up);
		} else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
			MoveTo(currentPosition, PlayerDirection.down);
		}
		
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
			MoveTo(currentPosition, PlayerDirection.left);
		} else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
			MoveTo(currentPosition, PlayerDirection.right);
		}
		
	}
	
	void MoveTo(PlayerDirection currentPosition, PlayerDirection direction){
		if(direction.Equals(PlayerDirection.up)){
			if(!currentPosition.Equals(PlayerDirection.down)){
				StartCoroutine(SnapMoveTo(this.transform.position, northPos));
				Debug.Log("Snap move north initiated");
				this.currentPosition = PlayerDirection.up;
			} else {
				StartCoroutine(SnapMoveTo(this.transform.position, centerPos));
				Debug.Log("Snap move center initiated");
				this.currentPosition = PlayerDirection.center;
			}
		} else if(direction.Equals(PlayerDirection.down)){
			if(!currentPosition.Equals(PlayerDirection.up)){
				StartCoroutine(SnapMoveTo(this.transform.position, southPos));
				Debug.Log("Snap move south initiated");
				this.currentPosition = PlayerDirection.down;
			} else {
				StartCoroutine(SnapMoveTo(this.transform.position, centerPos));
				Debug.Log("Snap move center initiated");
				this.currentPosition = PlayerDirection.center;
			}
		}
		
		if(direction.Equals(PlayerDirection.left)){
			if(!currentPosition.Equals(PlayerDirection.right)){
				StartCoroutine(SnapMoveTo(this.transform.position, westPos));
				Debug.Log("Snap move west initiated");
				this.currentPosition = PlayerDirection.left;
			} else {
				StartCoroutine(SnapMoveTo(this.transform.position, centerPos));
				Debug.Log("Snap move center initiated");
				this.currentPosition = PlayerDirection.center;
			}
		} else if(direction.Equals(PlayerDirection.right)){
			if(!currentPosition.Equals(PlayerDirection.left)){
				StartCoroutine(SnapMoveTo(this.transform.position, eastPos));
				Debug.Log("Snap move east initiated");
				this.currentPosition = PlayerDirection.right;
			} else {
				StartCoroutine(SnapMoveTo(this.transform.position, centerPos));
				Debug.Log("Snap move center initiated");
				this.currentPosition = PlayerDirection.center;
			}
		}
	}
	
	IEnumerator SnapMoveTo(Vector3 currentPosition, GameObject snapPoint){
		float i = 0.0f;
		float rate = snapSpeed;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			this.transform.position = Vector3.Lerp(currentPosition, snapPoint.transform.position, i);
			yield return null;
		}
	}
}
