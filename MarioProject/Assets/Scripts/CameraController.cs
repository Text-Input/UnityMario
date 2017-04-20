using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    float offsetX;
    float locationY;
    Vector3 screenpos;
	void Start () {
        offsetX = transform.position.x - player.transform.position.x;
        
	}
	
	
	void LateUpdate () {

        screenpos = Camera.main.WorldToScreenPoint(player.transform.position);

        if (screenpos.y >= Screen.height - 20) locationY = transform.position.y + 1;
        if (screenpos.y <= 20) locationY = transform.position.y - 1;

        transform.position = new Vector3(offsetX + player.transform.position.x, locationY, -10);
    }
}
