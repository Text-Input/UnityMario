using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	Vector3 properCameraPositon;
	Vector3 playerScreenPos;

	float offsetX;
	float locationY;

	void Start () {
		offsetX = transform.position.x - player.transform.position.x;
	}
	
	void LateUpdate () {
		properCameraPositon = transform.position; //I moveed the properCameraPosition set to the update. Without resetting the value each time, it would accumulate

		//get player position in the "real world" to a camera location
		playerScreenPos = Camera.main.WorldToScreenPoint (player.transform.position);

		if (playerScreenPos.y >= Screen.height / 2) { //player is at the correct height
			//the camera position
			properCameraPositon.y += playerScreenPos.y - (Screen.height / 2);
		} else if (playerScreenPos.y <= (Screen.height / 10)) { //player is at correct height
			//how low below the threshold is the player?
			properCameraPositon.y += playerScreenPos.y - (Screen.height / 10); //this gives a negative number, so we don't need to subtract it
		}
		locationY = Mathf.Lerp (transform.position.y, properCameraPositon.y, Time.deltaTime); //interpolate y value
		
		transform.position = new Vector3 (offsetX + player.transform.position.x, //keep x distance
			locationY, //set the interpolated Y value
			transform.position.z);
	}
}
