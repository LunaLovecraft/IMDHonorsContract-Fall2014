using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;
	//This Enum keeps track of where the player is looking, so the Camera can lead them and show more of the screen they care about
	public CameraState direction = CameraState.Right; 
	float shiftIndex = 0; //This number is used to keep the camera shift from being jarring

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//If the user turns left or right, change the enum
		if (Input.GetKey (KeyCode.D)) 
		{
						direction = CameraState.Right;
				} 
		else if (Input.GetKey (KeyCode.A)) 
		{
			direction = CameraState.Left;
				}
		//call the method to handle moving the camera over time
		shiftSlow (direction); 
		//Add shiftIndex to the X-value of the camera to move it
		transform.position = new Vector3 (toFollow.position.x + shiftIndex, toFollow.position.y, transform.position.z);

		//This code is for zooming the camera
		if((Input.GetAxis ("Mouse ScrollWheel")  < 0f || Input.GetKey(KeyCode.PageDown))&& //If the user scrolls down
		   this.GetComponent<Camera>().orthographicSize < 15f) //And the camera isn't zoomed too far out already
		{
			this.GetComponent<Camera>().orthographicSize += 0.5f; //Zoom out
		}

        if ((Input.GetAxis("Mouse ScrollWheel") > 0f|| Input.GetKey(KeyCode.PageUp)) && //If the user scrolls up
		    this.GetComponent<Camera>().orthographicSize > 6.5) //And the camera isn't zoomed in too far already
		{
			this.GetComponent<Camera>().orthographicSize -= 0.5f; //Zoom in
		}

	}

	//Take into account the player's direction and shift the camera
	void shiftSlow(CameraState heading)
	{
		if (heading == CameraState.Right && shiftIndex < 5f) 
		{
			shiftIndex += 0.2f;
		} 
		else if (heading == CameraState.Left && shiftIndex > -5f) 
		{
			shiftIndex -= 0.2f;
		}
	}
}

public enum CameraState
{
	Left,
	Right,
	Up, //Up and Down not implimented yet
	Down
};