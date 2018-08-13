using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipShieldUI : MonoBehaviour {

	[SerializeField] RectTransform barRectTransform;
	float maxWidth;


	void Awake()
	{
		maxWidth = barRectTransform.rect.width;
	}


	void OnEnable() 
	{
		EventManager.onMotherTakeDamage += UpdateMotherShieldDisplay;
		EventManager.onStartGame += ResetShield;
	}


	void OnDisable() 
	{
		EventManager.onTakeDamage -= UpdateMotherShieldDisplay;
		EventManager.onStartGame -= ResetShield;
	}


	void UpdateMotherShieldDisplay(float percentage)
	{
		barRectTransform.sizeDelta = new Vector2(maxWidth * percentage, 10);
	}


	void ResetShield()
	{
		barRectTransform.sizeDelta = new Vector2(200, 10);
	}

}
