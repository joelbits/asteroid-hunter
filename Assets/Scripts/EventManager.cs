using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void StartGameDelegate();
	public static StartGameDelegate onStartGame;

	public delegate void PauseGameDelegate();
	public static PauseGameDelegate onPauseGame;

	public delegate void PlayerDeathDelegate();
	public static PlayerDeathDelegate onPlayerDeath;

	public delegate void TakeDamageDelegate(float amount);
	public static TakeDamageDelegate onTakeDamage;

	public delegate void MotherTakeDamageDelegate(float amount);
	public static MotherTakeDamageDelegate onMotherTakeDamage;

	public delegate void ScorePointsDelegate(int amount);
	public static ScorePointsDelegate onScorePoints;

	public static int StartScore = 0;


	public static void StartGame()
	{
		Debug.Log("Start The Game!");

		// If we have at least one subscriber
		if (onStartGame != null)
			onStartGame();
	}


	public static void PauseGame()
	{
		Debug.Log("Toggle PAUSED.");
		if (onPauseGame != null)
			onPauseGame();
	}


	public static void EndGame()
	{
		Debug.Log("End The Game!");

		if (onPlayerDeath != null)
			onPlayerDeath();
	}


	public static void TakeDamage(float percent)
	{
		// Debug.Log("Take damage: " + percent);
		if (onTakeDamage != null)
			onTakeDamage(percent);
	}


	public static void MotherTakeDamage(float percent)
	{
		// Debug.Log("Mother Take damage: " + percent);
		if (onMotherTakeDamage != null)
			onMotherTakeDamage(percent);
	}


	public static void PlayerDeath()
	{
		Debug.Log("Player died Event!");
		if (onPlayerDeath != null)
			onPlayerDeath();
	}


	public static void ScorePoints(int amount)
	{
		// Debug.Log("Scored points: " + amount);
		if (onScorePoints != null)
			onScorePoints(amount);
	}


}
