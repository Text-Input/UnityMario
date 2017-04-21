using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    Vector3 properCameraPositon;
    

    float offsetX;
    float locationY;
    Vector3 playerScreenPos;

    void Start () {
        offsetX = transform.position.x - player.transform.position.x;

        properCameraPositon = transform.position;
	}
	
	
	void LateUpdate () {

        playerScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);

        if (playerScreenPos.y >= Screen.height / 2) {
            properCameraPositon.y = properCameraPositon.y + playerScreenPos.y - (Screen.height / 2);
        }

        if (playerScreenPos.y <= (Screen.height / 10)) {
            properCameraPositon.y = properCameraPositon.y - playerScreenPos.y - (Screen.height / 10);
        }

        locationY = Mathf.Lerp(transform.position.y, properCameraPositon.y, Time.deltaTime);

        transform.position = new Vector3(offsetX + player.transform.position.x, locationY, -10);
    }
}
