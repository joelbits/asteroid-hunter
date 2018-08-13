using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	[SerializeField] Text scoreText;
	[SerializeField] Text hiScoreText;
	[SerializeField] Text menuHiScoreText;
	[SerializeField] Text menuYourScore;

	[SerializeField] int score = 0;
	[SerializeField] int hiScore = 0;
	[SerializeField] bool isPaused = false;


	void OnEnable()
	{
		EventManager.onStartGame += ResetScore;
		EventManager.onPlayerDeath += CheckNewHiScore;
		EventManager.onScorePoints += AddScore;
	}

	void OnDisable() 
	{
		EventManager.onStartGame -= ResetScore;
		EventManager.onPlayerDeath -= CheckNewHiScore;
		EventManager.onScorePoints -= AddScore;
	}


	void Start() {
		DisplayHiScore();
	}

	void ResetScore()
	{
		isPaused = false;
		score = 0;
		DisplayScore();
	}

	void AddScore(int amount)
	{
		if (!isPaused)
		{
			score += amount;
			DisplayScore();
		}
	}

	void DisplayScore()
	{
		scoreText.text = "Score: " + score.ToString();
		menuYourScore.text = "Your score: " + score.ToString();
	}

	void DisplayHiScore()
	{
		LoadHiScore();

		if (hiScoreText != null)
			hiScoreText.text = "HiScore: " + hiScore.ToString();

		if (menuHiScoreText != null)
			menuHiScoreText.text = "High score: " + hiScore.ToString();
	}
	
	void LoadHiScore()
	{
		hiScore = PlayerPrefs.GetInt("hiScore", 999);
	}

	void CheckNewHiScore()
	{
		if (score > hiScore)
		{
			PlayerPrefs.SetInt("hiScore", score);
			DisplayHiScore();
		}

	}


}
