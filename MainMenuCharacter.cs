using UnityEngine;
using System.Collections;

public class MainMenuCharacter : MonoBehaviour {
	
	static public MainMenuCharacter instance;
	// Use this for initialization
	private int x=0;
	void Start () {
		InvokeRepeating ("playanimation", 0, 10);	
	}
	void playanimation(){
		string[] anim = {"Idle","Talk","Attack00"};
		x = (x + 1) % 2;
		animation.Play (anim[x]);
		
	}
}