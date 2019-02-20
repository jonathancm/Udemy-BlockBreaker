using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour {

	// Cached References
	GameSession gameSession;
	TextMeshProUGUI scoreText;

	// Use this for initialization
	void Start()
	{
		gameSession = FindObjectOfType<GameSession>();
		scoreText = GetComponent<TextMeshProUGUI>();

		if(gameSession)
		{
			scoreText.text = gameSession.GetScore().ToString();
		}
		else
		{
			scoreText.text = "0";
		}
	}

	private void Update()
	{
		if (gameSession)
		{
			scoreText.text = gameSession.GetScore().ToString();
		}
		else
		{
			scoreText.text = "0";
		}
	}
}
