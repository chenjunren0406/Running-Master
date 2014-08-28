using UnityEngine;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour {
	static public GameController instance;
	/*
	public GameObject hazard;
	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float hazardCount;
	*/
	public int goalCoins;
	public int totalTime;
	public bool onPause;
	public string NextLevel;

	private PlayerController playerController;
	private CameraController cameraController;
	private GuiController guiController;
	private AudioManager audioManager;
	private Timer timer;
	public GameObject magic_wall;
	public GameObject magic_wall2;
	public GameObject magic_wall3;

	FileInfo theSourceFile = null;
	StreamReader reader = null;
	StreamWriter writer = null;
	char[] record = new char[9];

	public void Awake(){
		instance = this;
	}

	void Start(){
		playerController = PlayerController.instance;
		cameraController = CameraController.instance;
		guiController = GuiController.instance;
		guiController.setGoalCoins (goalCoins);
		audioManager = AudioManager.instance;
		timer = Timer.instance;
		onPause = false;
	}

	public void startGame(){
		Time.timeScale = 1;
		playerController.ChangePlayerState (PlayerState.Run);
		guiController.showGUI (GUIState.InGame);
		audioManager.playBackgroundMusic(true);
		timer.StartTimer (totalTime);
		//StartCoroutine (SpawnWaves ());
	}

	public void restart(){
//		playerController.reset ();
//		cameraController.reset ();
//		guiController.reset ();
//		guiController.showGUI (GUIState.InGame);
//		playerController.ChangePlayerState (PlayerState.Run);
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevelName);
		audioManager.playBackgroundMusic(true);
		//StartCoroutine (SpawnWaves ());
	}

	public void backtoMainMenu(){
		Application.LoadLevel("MainMenu");
	}

	public void pause(){
		Time.timeScale = 0;
		guiController.showGUI (GUIState.Pause);
		timer.StopTimer ();
		onPause = true;
		audioManager.playBackgroundMusic(!onPause);
	}

	public void resume(){
		Time.timeScale = 1;
		guiController.showGUI (GUIState.InGame);
		timer.ResumeTimer ();
		onPause = false;
		audioManager.playBackgroundMusic(!onPause);
	}

	public void gameOver(){
		playerController.ChangePlayerState (PlayerState.Die);
		audioManager.playSoundEffect(SoundEffects.ObstacleCollisionSoundEffect);
		guiController.showGUI (GUIState.GameOver);
		timer.StopTimer ();
	}

	public void timeout(){
		playerController.ChangePlayerState (PlayerState.Stand);
		guiController.showGUI (GUIState.TimeOut);
		//timer.StopTimer ();
	}

	public void win(){

		playerController.ChangePlayerState (PlayerState.Win);
		audioManager.playBackgroundMusic(false);
		/*
		theSourceFile = new FileInfo (Application.dataPath + "/foo.txt");
		if ( theSourceFile != null && theSourceFile.Exists )
			reader = theSourceFile.OpenText();
		string txt = null;
		string f = null;
		txt = reader.ReadLine ();
		record = txt.ToCharArray ();
		reader.Close ();
		writer = new System.IO.StreamWriter (Application.dataPath + "/foo.txt");
		string currentLevel = Application.loadedLevelName;
		if (currentLevel == "Level 1") {
			record[1] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 2") {
			record[2] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 3") {
			record[3] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 4") {
			record[4] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 5") {
			record[5] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 6") {
			record[6] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 7") {
			record[7] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		if (currentLevel == "Level 8") {
			record[8] = '1';
			Debug.Log(new string(record));
			writer.WriteLine(new string(record));
			writer.Close();
		}
		*/
		audioManager.playSoundEffect(SoundEffects.GameOverSoundEffect);
		guiController.showGUI (GUIState.WinGame);

		timer.StopTimer ();
	}

	public void pickup(){
		GuiController.instance.setInGameScore(int.Parse(GuiController.instance.inGameScore.text)+1);
		audioManager.playSoundEffect(SoundEffects.CoinSoundEffect);
		if (int.Parse (GuiController.instance.inGameScore.text) >= goalCoins) {
			win();
		}
	}

	public void nextLevel(){
		Application.LoadLevel (NextLevel);
	}

	public string getmedal(int time){
		string medal = "";

		int[] upperBoundLeftTime = {25,60,20,10,10,25,10,35,18,18};
		int[] lowerBoundLeftTime = {10,20,10,5,5,12,5,13,10,10};

		string curlevelname = Application.loadedLevelName;
		string numOflevel = curlevelname.Substring(6);

		int relatedToBound = int.Parse (numOflevel) - 1;
		int high = upperBoundLeftTime [relatedToBound];
		int low = lowerBoundLeftTime [relatedToBound];
		int diff = high - low;
		if (time >= high)
			return "Running Master";
		else if (time >= diff * 0.8 + low)
			return "Golden Boot";
		else if(time >= diff * 0.6 + low )
			return "Sneaker";
		else if(time >= diff * 0.4 + low )
			return "Sandal";
		else if(time >= diff * 0.2 + low )
			return "Sock";
		else
			return "Bare Foot";
	}
	/*
	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			//yield return new WaitForSeconds (waveWait);
		}
	}
	*/
}
