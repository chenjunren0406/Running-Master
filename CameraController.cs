using UnityEngine;
public class CameraController : MonoBehaviour {
	
	static public CameraController instance;
	
	// Use this for initialization
	public GameObject player;
	public int offset  = -1;
	public bool isvirbate = false;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private Vector3 height = new Vector3 (0, 0, 0);
	private int rotateSmooth = 500;
	private int moveSmooth = 500;
	private Quaternion playerRotation;
	private Vector3 newCameraPosition;
	public int viberateStrength;
	public void Awake() {
		instance = this;	
	}
	void Start () {
		startPosition = transform.position;
		startRotation = transform.rotation;
		height.y = transform.position.y-PlayerController.instance.transform.position.y;
	}
	
	void Update()
	{
		playerRotation = PlayerController.instance.transform.rotation;
		if(PlayerController.instance.flag == 0)
			newCameraPosition = player.transform.position + PlayerController.instance.facedirection * offset + height;
		else if(PlayerController.instance.flag == 1)
			newCameraPosition = player.transform.position + PlayerController.instance.facedirection * offset - height;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		//transform.position = player.transform.position + PlayerController.instance.facedirection * offset + height;
		//transform.rotation = new Quaternion (0, player.transform.position.y, 0) + rotationOffset;
		if ((transform.position - newCameraPosition).sqrMagnitude > 0.1f)
			transform.position = Vector3.MoveTowards (transform.position, newCameraPosition, moveSmooth * Time.deltaTime);
		if (Quaternion.Angle (transform.rotation, playerRotation) > 0.1f)
			transform.rotation = Quaternion.RotateTowards (transform.rotation, playerRotation, rotateSmooth * Time.deltaTime);

	}
	
	public void reset(){
		transform.rotation = startRotation;
		transform.position = startPosition;
	}
	
	
	public void turnDirection(string direction){
		if (direction == "left") {
			
			
			//transform.Rotate (0, -90, 0,Space.World);
			
			//transform.RotateAround(GameController.instance.transform.position,Vector3.up,Time.deltaTime*20);
			//transform.rotation +=Quaternion.FromToRotation(Vector3.forward,Vector3.left);
			
		} else if (direction == "right") {
			//transform.Rotate (0, 90, 0,Space.World);
			//transform.rotation +=Quaternion.FromToRotation(Vector3.forward,Vector3.right);
		}
		else if (direction == "down") {
			//transform.Rotate (0, 180, 0,Space.World);
			//transform.rotation +=Quaternion.FromToRotation(Vector3.forward,Vector3.right);
		}
	}
	public void vibrate(){
			var randx = Random.Range(-viberateStrength,viberateStrength);
			var randy = Random.Range(-viberateStrength,viberateStrength);
			var randz = Random.Range(-viberateStrength,viberateStrength);
			
			transform.position += new Vector3(randx,randy,randz);
	}
}

