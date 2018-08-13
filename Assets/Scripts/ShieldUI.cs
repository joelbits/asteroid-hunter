using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUI : MonoBehaviour {

	[SerializeField] RectTransform barRectTransform;
	float maxWidth;


	void Awake()
	{
		maxWidth = barRectTransform.rect.width;
	}


	void OnEnable() 
	{
		EventManager.onTakeDamage += UpdateShieldDisplay;
		EventManager.onStartGame += ResetShield;
	}


	void OnDisable() 
	{
		EventManager.onTakeDamage -= UpdateShieldDisplay;
		EventManager.onStartGame -= ResetShield;
	}


	void UpdateShieldDisplay(float percentage)
	{
		barRectTransform.sizeDelta = new Vector2(maxWidth * percentage, 10);
	}


	void ResetShield()
	{
		Debug.Log("ResetShield!");
		barRectTransform.sizeDelta = new Vector2(200, 10);
	}

}
