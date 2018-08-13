using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject gameUi;


	void Start() {
		DelayMainMenuDisplay();
	}

	void OnEnable() {
		EventManager.onStartGame += ShowGameUI;
		EventManager.onPlayerDeath += ShowMainMenu;
		EventManager.onPauseGame += ToogleMainMenu;;
	}

	void OnDisable() {
		EventManager.onStartGame -= ShowGameUI;
		EventManager.onPlayerDeath -= ShowMainMenu;
		EventManager.onPauseGame -= ToogleMainMenu;
	}


	void ShowMainMenu()
	{
		// new WaitForSeconds(Asteroid.destructionDelay);
		Invoke("DelayMainMenuDisplay", Asteroid.destructionDelay);
	}

	void ShowGameUI()
	{
		mainMenu.SetActive(false);
		gameUi.SetActive(true);
	}


	void DelayMainMenuDisplay()
	{
		mainMenu.SetActive(true);
		gameUi.SetActive(false);
	}


	void ToogleMainMenu()
	{
		mainMenu.SetActive(!mainMenu.active);
		gameUi.SetActive(!gameUi.active);
	}

}
