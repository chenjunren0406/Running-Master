using UnityEngine;
using System.Collections;

public class FingerEventListener : MonoBehaviour {


	void OnTap(TapGesture gesture) { 
		//girlcontroller.jump ();
	}
	void OnSwipe(SwipeGesture gesture) { 
		FingerGestures.SwipeDirection direction = gesture.Direction;
		if (direction == FingerGestures.SwipeDirection.Up)
			PlayerController.instance.jump ();
		else if (direction == FingerGestures.SwipeDirection.Left) {
						// camera and charcter rotate to left
			PlayerController.instance.turndirection("left");
			CameraController.instance.turnDirection("left");
				} 
		else if (direction == FingerGestures.SwipeDirection.Right) {
						//camera and charcter rotate to right
			PlayerController.instance.turndirection("right");
			CameraController.instance.turnDirection("right");
				}
		else if (direction == FingerGestures.SwipeDirection.Down){
			PlayerController.instance.turndirection("down");
			CameraController.instance.turnDirection("down");
				}
	}
}
