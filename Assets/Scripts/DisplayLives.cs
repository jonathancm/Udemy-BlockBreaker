using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLives : MonoBehaviour {

	// Cached References
	GameSession gameSession;
	TextMeshProUGUI livesText;

	// Use this for initialization
	void Start()
	{
		gameSession = FindObjectOfType<GameSession>();
		livesText = GetComponent<TextMeshProUGUI>();

		if (gameSession)
		{
			livesText.text = gameSession.GetLives().ToString();
		}
		else
		{
			livesText.text = "0";
		}
	}

	private void Update()
	{
		if (gameSession)
		{
			livesText.text = gameSession.GetLives().ToString();
		}
		else
		{
			livesText.text = "0";
		}
	}
}
