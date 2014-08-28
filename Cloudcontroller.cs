using UnityEngine;
using System.Collections;

public class Cloudcontroller : MonoBehaviour {
	
	public GameObject obj;
	public GameObject player;
	private Vector3 offset;
	private int count = 10;
	private bool showup = false;
	private int line = 0;
	private float random = 0;
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
		if (Random.Range (0, 3) < 1.1) {
			random = randomline ();
			
			Instantiate (obj, new Vector3 (offset.x + random, offset.y+3, player.transform.position.z + offset.z + 50), Quaternion.identity);
			
		}
		Invoke ("spawn", 4f);
	}
	int randomline(){
		int x = Random.Range (0, 3);
		if (x < 1)
			return -1;
		else if (x >= 1 && x < 2)
			return 0;
		else
			return 1;
	}
}
