using UnityEngine;
using System.Collections;

/*
 * The GUI manager is a singleton class which manages the NGUI objects
 */
public enum GUIState { MainMenu, InGame, EndGame, Store, Stats, Pause, Tutorial, Missions, Inactive, WinGame, GameOver, TimeOut }
public enum TutorialType { Jump, Slide, Strafe, Attack, Turn, GoodLuck }
public class GuiController : MonoBehaviour {
	
	static public GuiController instance;
	
	public GameObject mainMenuPanel;
	public GameObject logoPanel;
	public GameObject inGamePanel;
	public GameObject endGamePanel;
	public GameObject storePanel;
	public GameObject statsPanel;
	public GameObject missionsPanel;
	public GameObject pausePanel;
	public GameObject tutorialPanel;
	
	// in game:
	public UILabel inGameScore;
	public UILabel inGameGoalCoins;
	public UILabel inGameTime;
	public Animation inGamePlayAnimation;
	public string inGamePlayAnimationName;
	
	// pause:
	public UILabel pauseScore;
	public UILabel pauseCoins;
	public UILabel pauseTime;
	
	// end game:
	public UILabel endGameScore;
	public UILabel endGameCoins;
	public UILabel endGameTime;
	public UILabel endGameMultiplier;
	public Animation endGamePlayAnimation;
	public string endGamePlayAnimationName;
	public UILabel endGamePrompt;

	
	// store:
	public GameObject storeBackToMainMenuButton;
	public GameObject storeBackToEndGameButton;
	public UILabel storePowerUpSelectionButton;
	public UILabel storeTitle;
	public UILabel storeDescription;
	public UILabel storeCoins;
	public UIButton storeBuyButton;
	private bool storeSelectingPowerUp;
	private int storeItemIndex;
	
	public Transform storePowerUpPreviewTransform;
	public Transform storeCharacterPreviewTransform;
	private GameObject storeItemPreview;
	
	// stats:
	public UILabel statsHighScore;
	public UILabel statsCoins;
	public UILabel statsPlayCount;
	
	// tutorial:
	public UILabel tutorialLabel;
	
	// missions:
	public GameObject missionsBackToMainMenuButton;
	public GameObject missionsBackToEndGameButton;
	public UILabel missionsScoreMultiplier;
	public UILabel missionsActiveMission1;
	public UILabel missionsActiveMission2;
	public UILabel missionsActiveMission3;
	public GUIClickEventReceiver missionsBackButtonReceiver;
	
	private GUIState guiState;
	private GameController gameController;

	
	public void Awake()
	{
		instance = this;	
	}
	
	public void Start ()
	{
		gameController = GameController.instance;
		guiState = GUIState.MainMenu;
		showGUI (GUIState.MainMenu);

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

	public void showGUI(GUIState state)
	{		
		switch (state) {
		case GUIState.MainMenu:
			activateObject(mainMenuPanel, true);
			activateObject(endGamePanel, false);
			activateObject(pausePanel,false);
			activateObject(inGamePanel,false);
			break;
		case GUIState.InGame:

			activateObject(mainMenuPanel, false);
			activateObject(endGamePanel, false);
			activateObject(pausePanel,false);
			activateObject(inGamePanel,true);
			break;

		case GUIState.Pause:
			pauseScore.text=inGameScore.text;
			pauseTime.text=inGameTime.text;
			activateObject(mainMenuPanel, false);
			activateObject(endGamePanel, false);
			activateObject(pausePanel,true);
			activateObject(inGamePanel,false);
			break;

		case GUIState.TimeOut:
			endGamePrompt.text="Time's up";
			showEndGamePanel();
			break;

		case GUIState.WinGame:
			endGamePrompt.text="Level Completed";
			showEndGamePanel();
			break;

		case GUIState.GameOver:
			endGamePrompt.text="Game Over";
			showEndGamePanel();
			break;
		}
		
		guiState = state;
	}
		
	public void showEndGamePanel(){
		endGameScore.text = inGameScore.text;
		endGameTime.text = inGameTime.text;
		activateObject(mainMenuPanel, false);
		activateObject(endGamePanel, true);
		activateObject(pausePanel,false);
		activateObject (inGamePanel, false);
	}

	public void reset(){
		setInGameScore (0);

	}

	public void setTimeLeft(int time){
		inGameTime.text = time.ToString ();
	}

	public void setGoalCoins(int goal)
	{
		inGameGoalCoins.text = "/ "+goal.ToString();
	}


	public void setInGameScore(int score)
	{
		inGameScore.text = score.ToString();
	}



	
	private void activateWidget(Behaviour widget, bool activate)
	{
		#if UNITY_3_5
		widget.gameObject.SetActiveRecursively(activate);
		#else
		widget.gameObject.SetActive(activate);
		#endif
	}
}
