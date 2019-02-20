using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
	// Debug Parameters
	[SerializeField] int breakableBlocks;

	// Cached Reference
	GameSession gameSession;
	SceneLoader sceneLoader;

	public void Start()
	{
		sceneLoader = FindObjectOfType<SceneLoader>();
		gameSession = FindObjectOfType<GameSession>();
	}

	public void AddBlockToCount()
	{
		breakableBlocks++;
	}

	public void RemoveBlockFromCount()
	{
		breakableBlocks--;
		if (breakableBlocks <= 0)
		{
			gameSession.AddLife();
			sceneLoader.LoadNextScene();
		}
	}
}
