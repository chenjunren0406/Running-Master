using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	
	public GameObject obj;
	public GameObject player;
	private Vector3 offset;
	private int distance = 1;
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
				//transform.position = player.transform.position + PlayerController.instance.facedirection * distance;
		}
	void spawn(){
		if (Random.Range (0, 3) < 1.5) {
						//random = randomline ();
						random=0;
						Vector3 startpos = player.transform.position + PlayerController.instance.facedirection * distance;
						while (count-- > 0){
						Debug.Log(startpos);
						startpos += PlayerController.instance.facedirection * count;
						Instantiate (obj,startpos,Quaternion.identity);
						}	
						count = 10;
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

