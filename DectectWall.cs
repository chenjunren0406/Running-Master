using UnityEngine;
using System.Collections;

public class DectectWall : MonoBehaviour {
	/*
	 *  1 is left, 2 is rigth
	 */
	private Vector3 fd;
	public int distance;
	// Use this for initialization
	void Start () {	

	}
	
	// Update is called once per frame
	void Update () {
		fd = PlayerController.instance.facedirection;
		transform.position = PlayerController.instance.transform.position + new Vector3(-fd.z * distance,10,fd.x*distance);

		transform.rotation = PlayerController.instance.transform.rotation;
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.name == "Plane" && PlayerController.instance.isOn == true)
			;//CameraController.instance.vibrate();
	}
}
