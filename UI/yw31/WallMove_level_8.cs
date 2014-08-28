using UnityEngine;
using System.Collections;

public class WallMove_level_8 : MonoBehaviour {
	
	private Vector3 moveDirection;
	private float speed;
	public bool moveX;
	public Vector3 original_position;
	public int diff;
	// Use this for initialization
	void Awake(){
		original_position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}
	void Start () {
		
		speed = 0.5f;
	//	original_position = new Vector3 (-115, -48, -12);
		//original_position = Transform.position;
		
		if (moveX)
			moveDirection = new Vector3 (1, 0, 0);
		else 
			moveDirection = new Vector3 (0, 0, 1);
		
		
	}

	// Update is called once per frame
	void Update () {
		if (moveX)
			diff = (int)Mathf.Abs(transform.position.x - original_position.x);
		else
		    diff = (int)Mathf.Abs(transform.position.z - original_position.z);
		if (diff >= 60)
			moveDirection = new Vector3 (-moveDirection.x, 0, -moveDirection.z);
		transform.position += new Vector3 (moveDirection.x * speed, 0, moveDirection.z * speed);
	}
	
	void OnCollisionEnter(Collision other){
		Debug.Log ("name : " + other.gameObject.name + " tag: " + other.gameObject.tag);
		if (other.gameObject.tag == "Wall") {
			moveDirection = new Vector3( -moveDirection.x, 0 , -moveDirection.z);
		}
	}
}
