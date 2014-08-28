using UnityEngine;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour {

	public GameObject levelSelection;
	private AudioManager audioManager;
	bool musicplay;
	char[] record = new char[9];
	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public GameObject level4;
	public GameObject level5;
	public GameObject level6;
	public GameObject level7;
	public GameObject level8;
	public GameObject level9;
	public GameObject level10;
	FileInfo theSourceFile = null;
	StreamReader reader = null;
	// Use this for initialization
//	void awake(){
//		StreamWriter writer = new System.IO.StreamWriter (Application.dataPath + "/foo.txt");
//		writer.WriteLine("100000000");
//		writer.Close();
//		}
	void Start () {
		activateObject (levelSelection,false);
		audioManager = AudioManager.instance;
		musicplay = false;
//		theSourceFile = new FileInfo (Application.dataPath + "/foo.txt");
//		if ( theSourceFile != null && theSourceFile.Exists )
//			reader = theSourceFile.OpenText();
//		string txt = null;
//		txt = reader.ReadLine ();
//		record = txt.ToCharArray ();
//		reader.Close ();
	
	}
	
	void Update(){
		if (!musicplay) {
			audioManager.playBackgroundMusic (true);
			musicplay=true;
		}
	}

	public void showLevel(){
		activateObject (levelSelection,true);
	}

	public void hideLevel(){
		activateObject (levelSelection,false);
	}

	public void enterLevel_1(){
	//	if (record [0] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 1");
	//	}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_2(){
	//	if (record [1] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 2");
	//	}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_0(){
		audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
		Application.LoadLevel ("Level 0");
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_3(){
	//	if (record [2] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 3");
	//	}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_4(){
		//if (record [3] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 4");
		//}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_5(){
	//	if (record [4] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 5");
	//	}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_6(){
	//	if (record [5] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 6");
	//	}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_7(){
		//if (record [6] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 7");
		//}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}
	public void enterLevel_8(){
		//if (record [7] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 8");
		//}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}

	public void enterLevel_9(){
		//if (record [8] == '1') {
			audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
			Application.LoadLevel ("Level 9");
		//}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
	}
	public void enterLevel_10(){
		//if (record [8] == '1') {
		audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
		Application.LoadLevel ("Level 10");
		//}
		//audioManager.playSoundEffect(SoundEffects.GUITapSoundEffect);
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
