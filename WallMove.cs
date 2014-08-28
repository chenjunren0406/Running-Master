using UnityEngine;
using System.Collections;

public class WallMove : MonoBehaviour {

	private Vector3 moveDirection;
	private float speed;
	public bool moveX;
	// Use this for initialization
	void Start () {

		speed = 1f;

		if (moveX)
			moveDirection = new Vector3 (1, 0, 0);
		else 
			moveDirection = new Vector3 (0, 0, 1);

		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (moveDirection.x * speed, 0, moveDirection.z * speed);
	}

	void OnCollisionEnter(Collision other){
		Debug.Log ("name : " + other.gameObject.name + " tag: " + other.gameObject.tag);
		if (other.gameObject.tag == "Wall") {
			moveDirection = new Vector3( -moveDirection.x, 0 , -moveDirection.z);
		}
	}
}
