using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	// Cached references
	GameSession gameSession;

	private void Start()
	{
		gameSession = FindObjectOfType<GameSession>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		gameSession.RemoveLife();
    }
}
