using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject obj;
	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position;
		spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (offset.x,offset.y,player.transform.position.z + offset.z);
	}

	void spawn(){
		Instantiate (obj, transform.position, Quaternion.identity);

		Invoke("spawn", 1f);
	}
}
