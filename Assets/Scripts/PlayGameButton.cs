using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameButton : MonoBehaviour {

	public void ClickPlayButton()
	{
		EventManager.StartGame();
	}
}
