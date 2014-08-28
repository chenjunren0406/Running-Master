using UnityEngine;
using System.Collections;


public enum PlayerState { Run, Die, Win, Stand}
public class PlayerController : MonoBehaviour {

	static public PlayerController instance;
	public GameObject tutorial_tip_Left;
	public GameObject tutorial_tip_Back;
	public GameObject tutorial_tip_Right;
	public const int HERO_UP= 0;
	public const int HERO_RIGHT= 1;
	public const int HERO_DOWN= 2;
	public const int HERO_LEFT= 3;
	public int flag = 0; //0 means the player on the ground, 1 means the player on the top
	public PlayerState playerState;

	public float keyboardinput;

	public int state = 0;
	public int backState = 0;
	public bool anotherAnimation = false;

	public Vector3 facedirection = new Vector3(0,0,1);
	
	public float speed;
	public float maxspeed ;
	private Vector3 startPosition;
	private Quaternion startRotation;
	public bool isOn = false;

	/*
	 * lock the input at the beginning
	 */
	private int countDown = 30;
	private bool checkpoint = false;
	private int collisionAduio = 60;

	public void Awake() {
		instance = this;	
		//GameController.instance.magic_wall.SetActive = false;
	}
	
	void Start () {
		playerState = PlayerState.Stand;
		state = HERO_DOWN;
		startPosition = transform.position;
		startRotation = transform.rotation;
		activateObject (tutorial_tip_Left,false);
		activateObject (tutorial_tip_Right,false);
		activateObject (tutorial_tip_Back,false);
		GameController.instance.magic_wall.SetActive (false);
		GameController.instance.magic_wall2.SetActive (false);
		GameController.instance.magic_wall3.SetActive (false);

	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);
			GameController.instance.pickup ();
		}

		else if (other.gameObject.tag == "magic coin") {

			if(other.gameObject.name == "magic coin1"){
				other.gameObject.SetActive(false);
				GameController.instance.magic_wall.SetActive(true);
				transform.position = new Vector3(transform.position.x, transform.position.y + 43.7203f, transform.position.z);
				transform.rotation = Quaternion.Euler(0, -90, 0);
				facedirection = new Vector3(-1, 0, 0);
				Vector3 newCameraPosition = new Vector3(transform.position.x + 30, transform.position.y + 1, transform.position.z );
				CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
			}

			else if(other.gameObject.name == "magic coin3"){
				other.gameObject.SetActive(false);
				GameController.instance.magic_wall2.SetActive(true);
				GameController.instance.magic_wall3.SetActive(true);
				transform.position = new Vector3(transform.position.x, transform.position.y + 43.7203f, transform.position.z);
				transform.rotation = Quaternion.Euler(0, 0, 0);
				facedirection = new Vector3(0, 0, 1);
				Vector3 newCameraPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 30);
				CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
			}
			else if(other.gameObject.name == "magic coin2"){
				other.gameObject.SetActive(false);
				GameController.instance.magic_wall.SetActive(false);
				transform.position = new Vector3(transform.position.x, transform.position.y - 43.7203f, transform.position.z);
				transform.rotation = Quaternion.Euler(0,0,0);
				facedirection = new Vector3(0,0,1);
				Vector3 newCameraPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 30 );
				CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
			}
			else if(other.gameObject.name == "magic coin4"){
				other.gameObject.SetActive(false);
				GameController.instance.magic_wall2.SetActive(false);
				GameController.instance.magic_wall3.SetActive(false);
				transform.position = new Vector3(transform.position.x, transform.position.y - 43.7203f, transform.position.z);
				transform.rotation = Quaternion.Euler(0,90,0);
				facedirection = new Vector3(1, 0, 0);
				Vector3 newCameraPosition = new Vector3(transform.position.x - 30, transform.position.y + 1, transform.position.z);
				CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
			}
		}
	
		else if (other.gameObject.tag == "tip_trigger_Left") {
				activateObject (tutorial_tip_Left, true);
				
		} else if (other.gameObject.tag == "tip_trigger_Back") {
				activateObject (tutorial_tip_Left, false);
				activateObject (tutorial_tip_Back, true);
				
		} else if (other.gameObject.tag == "tip_trigger_Right") {
				if (checkpoint)
					activateObject (tutorial_tip_Back, false);
				activateObject (tutorial_tip_Right, true);
				
		} else if (other.gameObject.tag == "tip_trigger_End") {
				
				activateObject (tutorial_tip_Right, false);
				if (playerState == PlayerState.Run && checkpoint)
					GameController.instance.win ();
		} else if (other.gameObject.tag == "checkpoint") {
				checkpoint=true;
				
		}
			

	}

	void OnTriggerStay(Collider other)
	{
		
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);

			GameController.instance.pickup ();
			
		} 	
		
	}

	void OnCollisionEnter(Collision other){

				if (other.collider.tag == "Wall") {
		
						if (playerState == PlayerState.Run) {
								slowdown ();
						}
				
						if (collisionAduio > 60) {

								AudioManager.instance.playSoundEffect (SoundEffects.ObstacleCollisionSoundEffect);

								collisionAduio = 0;	
						}
				} else if (other.collider.tag == "go down wall") {
						if (other.collider.name == "go down wall1") {
								transform.position = new Vector3 (transform.position.x, transform.position.y - 43.7203f, transform.position.z);
								Vector3 newCameraPosition = new Vector3 (CameraController.instance.transform.position.x, CameraController.instance.transform.position.y - 43.7203f, CameraController.instance.transform.position.z);
								CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
						}
				} else if (other.collider.tag == "special wall") {
						if (other.collider.name == "special wall1" || other.collider.name == "special wall3") {
								if (facedirection.z != 0) {
										if (flag == 0) {
												flag = 1;
												transform.position = new Vector3 (transform.position.x, transform.position.y + 97, transform.position.z);
												transform.rotation = Quaternion.Euler (0, 180, 180);
												facedirection = new Vector3 (-facedirection.x, -facedirection.y, -facedirection.z);
										} else if (flag == 1) {
												flag = 0;
												transform.position = new Vector3 (transform.position.x, transform.position.y - 97, transform.position.z - 10);
												transform.rotation = Quaternion.Euler (0, 180, 0);
												facedirection = new Vector3 (-facedirection.x, -facedirection.y, -facedirection.z);
										}
										Vector3 newCameraPosition = new Vector3 (transform.position.x - 7, transform.position.y + 1, transform.position.z + 55 * facedirection.z);
										CameraController.instance.transform.position = Vector3.MoveTowards (
						CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
							}
						} else if (other.collider.name == "special wall2") {
								if (facedirection.x != 0) {
										if (flag == 0) {
												flag = 1;
												transform.position = new Vector3 (transform.position.x, transform.position.y + 97, transform.position.z);
												transform.rotation = Quaternion.Euler (0, 270, 180);
												facedirection = new Vector3 (-facedirection.x, -facedirection.y, -facedirection.z);
										} else if (flag == 1) {
												flag = 0;
												transform.position = new Vector3 (transform.position.x, transform.position.y - 97, transform.position.z);
												transform.rotation = Quaternion.Euler (0, 270, 0);
												facedirection = new Vector3 (-facedirection.x, -facedirection.y, -facedirection.z);
										}
										Vector3 newCameraPosition = new Vector3 (transform.position.x - 30 * facedirection.x, transform.position.y + 1, transform.position.z);
										CameraController.instance.transform.position = Vector3.MoveTowards (
						CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
							}
						}

				} else if (other.collider.tag == "Transmission Door") {
						if (other.collider.name == "cylindrical generator1") {
								transform.position = new Vector3 (502.8274f, 20.01962f, -220.6519f);
								transform.rotation = Quaternion.Euler (0, 0, 0);
								facedirection = new Vector3 (0, 0, 1);

								Vector3 newCameraPosition = new Vector3 (501.9421f, 58.51643f, -362.1351f);

								CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 2000 * Time.deltaTime);

						} else if (other.collider.name == "cylindrical generator2") {
				transform.position = new Vector3 (688.9636f, 13.29855f, -1104.244f);
								transform.rotation = Quaternion.Euler (0, 90, 0);
								facedirection = new Vector3 (1, 0, 0);

								Vector3 newCameraPosition = new Vector3 (1298.912f, 58.51643f, -989.025f);
								CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 2000 * Time.deltaTime);

						}
				}
		}




	void OnCollisionStay(Collision other){

		if (other.collider.tag == "Wall") {
		
			if (playerState == PlayerState.Run){
				slowdown();
			
			}
		}
		else if (other.collider.tag == "go down wall") {
			if(other.collider.name == "go down wall1"){
				transform.position = new Vector3(transform.position.x, transform.position.y - 43.7203f, transform.position.z);
				Vector3 newCameraPosition = new Vector3( CameraController.instance.transform.position.x, CameraController.instance.transform.position.y - 43.7203f, CameraController.instance.transform.position.z);
				CameraController.instance.transform.position = Vector3.MoveTowards (
					CameraController.instance.transform.position, newCameraPosition, 200000 * Time.deltaTime);
			}
		}
	}

	IEnumerator DelayedDisable(Object tobeDestroy) {
		yield return new WaitForSeconds(0.01f);
		Destroy (tobeDestroy);
	}

	void Update () {
		if (!GameController.instance.onPause)
		switch (playerState) {
		case PlayerState.Stand:
			break;
		case PlayerState.Die:
			break;
		case PlayerState.Run: 
			move ();
			
			if (!animation.IsPlaying("Jump_NoBlade")) {
				anotherAnimation = false;
			}
			break;
		}

	
		keyboardinput = Input.GetAxis ("Horizontal");

		collisionAduio++;

		if (countDown > 0 && isOn == true)
			countDown--;
	}

	public void slowdown(){
		speed = maxspeed * 0.1f;
	}

	public void ChangePlayerState(PlayerState state){
		switch (state) {
		case PlayerState.Stand:
			isOn = false;
			animation.Play("Idle");
			break;
		case PlayerState.Die:
			if (playerState==PlayerState.Run) animation.Play("Death");
			isOn = false;
			break;
		case PlayerState.Run:
			isOn = true;
			break;
		case PlayerState.Win:
			if (playerState==PlayerState.Run) animation.Play("GanamStyle");
			isOn = false;
			break;
		}
		playerState = state;
	}

	public void reset(){
		transform.position = startPosition;
		transform.rotation = startRotation;
		facedirection = Vector3.forward;
		playerState = PlayerState.Stand;
	}

	void FixedUpdate(){
		switch (playerState) {
		case PlayerState.Stand:
			break;
		case PlayerState.Die:
			break;
		case PlayerState.Run:
			Vector3 tiltdirection = new Vector3();
			if(flag == 0) tiltdirection = new Vector3 (facedirection.z, 0, -facedirection.x);
			else if(flag == 1)tiltdirection = new Vector3 (-facedirection.z, 0, facedirection.x);
			//Vector3 tiltdirection = new Vector3 (facedirection.z, 0, -facedirection.x);

			keyboardinput = keyboardinput == 0 ? Input.acceleration.x : keyboardinput;

			if (keyboardinput > 0.05 && isOn == true) {
				float gravity = Mathf.Pow(keyboardinput,2) > 0.16f ? 0.16f: Mathf.Pow(keyboardinput,2);
				Vector3 transformValue = transform.position +  tiltdirection * 20;
				this.transform.position = Vector3.MoveTowards(this.transform.position,transformValue,Time.deltaTime * 1600 * gravity);
			} 
			else if (keyboardinput < -0.05 && isOn == true) {
				float gravity = Mathf.Pow(keyboardinput,2)> 0.16f ? 0.16f: Mathf.Pow(keyboardinput,2);
				Vector3 transformValue =  transform.position  - tiltdirection * 20;
				this.transform.position = Vector3.MoveTowards(this.transform.position,transformValue,Time.deltaTime * 1600 * gravity);
			}
			break;
			
			
		}
	}

	/*
	 * move a little step when running
	 */
	public void move(){
		if(!anotherAnimation)
			animation.Play("Run00");

		if (speed < maxspeed)
			speed += maxspeed * 0.01f;


	
		Vector3 transformValue = new Vector3 ();

		transformValue = facedirection * speed;

		transform.Translate (transformValue * Time.deltaTime, Space.World);
	
	}
	public void jump(){
		if (isOn == true && countDown <= 0) {
						//rigidbody.AddForce (new Vector3 (0, 100, 0), ForceMode.Acceleration);
			            
				//		anotherAnimation = true;
		//	transform.position = new Vector3(transform.position.x, transform.position.y + 34, transform.position.z);
		//	transform.position = new Vector3(transform.position.x + facedirection.x * 70, transform.position.y - 70, transform.position.z + facedirection.z * 70);

				//		animation.Play ("Jump_NoBlade");
			           // transform.position = new Vector3(transform.position.x + facedirection.x * 70, transform.position.y + facedirection.y * 70, transform.position.z + facedirection.z * 70);
				}
	}

	public void turndirection(string direction){
		Vector3 newdirection = new Vector3 ();
		if (direction == "left"&& isOn == true && countDown <= 0) {
			transform.Rotate (0, -90, 0);
			if(flag == 0){
				newdirection = new Vector3 (-facedirection.z, 0, facedirection.x);
			}
			else if(flag == 1){
				newdirection = new Vector3 (facedirection.z, 0, -facedirection.x);
			}
			facedirection = newdirection;

		} else if (direction == "right"&& isOn == true && countDown <= 0) {
			transform.Rotate (0, 90, 0);
			if(flag == 0){
				newdirection = new Vector3 (facedirection.z, 0, -facedirection.x);
			}
			else if(flag == 1){
				newdirection = new Vector3 (-facedirection.z, 0, facedirection.x);
			}
			facedirection = newdirection;

		} else if (direction == "down"&& isOn == true && countDown <= 0) {
					transform.Rotate(0,180,0);
					newdirection = new Vector3(-facedirection.x,0,-facedirection.z);
					facedirection = newdirection;
				}
	}

	private void activateObject(GameObject obj, bool activate)
	{
		activeRecursively(obj.transform, activate);
	}
	
	private void activeRecursively(Transform obj, bool active)
	{
		foreach (Transform child in obj) {
			activeRecursively(child, active);
		}
		obj.gameObject.SetActive(active);
	}

	
}
