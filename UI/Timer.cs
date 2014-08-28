using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	static public Timer instance;
	private GuiController guiController;
	bool TimerOn;
	int secondLeft;

	public void Awake()
	{
		instance = this;	
	}

	void Start(){
		TimerOn = false;
		guiController = GuiController.instance;
	}

	public void StartTimer(int totalTime){
		secondLeft=totalTime;
		TimerOn=true;
		StartCoroutine(DoCountDown());
	}

	public void StopTimer(){
		TimerOn = false;
		StopCoroutine("DoCountDown");
	}

	public void ResumeTimer(){
		TimerOn = true;
		StartCoroutine (DoCountDown ());
	}

	void Update(){
		if (TimerOn) {
			guiController.setTimeLeft (secondLeft);				
			if (secondLeft==0) GameController.instance.timeout();
				}
	}

	IEnumerator DoCountDown(){
		while(secondLeft > 0){
			yield return new WaitForSeconds(1f);
			secondLeft--;
		}
	}
}
