using UnityEngine;
using System.Collections;

/*
 * The user pressed a button, perform some action
 */
public enum ClickType { StartGame, Stats, Store, EndGame, Restart, MainMenu, MainMenuRestart, Pause, Resume, NextLevel, ToggleTutorial, Missions, 
	StorePurchase, StoreNext, StorePrevious, StoreTogglePowerUps, MainMenuFromStore, EndGameFromStore, Facebook, Twitter }
public class GUIClickEventReceiver : MonoBehaviour {

	public ClickType clickType;

	public void OnClick()
	{
		bool playSoundEffect = true;
		switch (clickType) {
		case ClickType.StartGame:
			GameController.instance.startGame();
			break;
		case ClickType.Restart:
			GameController.instance.restart();
			break;
		case ClickType.MainMenu:
			GameController.instance.backtoMainMenu();
			break;
		case ClickType.Resume:
			GameController.instance.resume();
			break;
		case ClickType.Pause:
			GameController.instance.pause();
			break;
		case ClickType.NextLevel:
			GameController.instance.nextLevel();
			break;
		}
	}
	
}
