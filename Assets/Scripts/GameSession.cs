using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

	// Configuration Parameters
	[Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
	[SerializeField] int playerLives = 2;
	[SerializeField] int pointsPerBlockDestroyed = 100;
	[SerializeField] bool isAutoPlayEnabled = false;

	// Cached References
	SceneLoader sceneLoader;

	// State Variables
	[SerializeField] int currentScore;
	[SerializeField] int currentLives;
	public static GameSession instance = null; //Static instance of GameSession

	private void Awake()
	{
		SetupSingleton();
	}

	private void SetupSingleton()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Init Game
		currentScore = 0;
		currentLives = playerLives;

		sceneLoader = FindObjectOfType<SceneLoader>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Time.timeScale = gameSpeed;
	}

	public int GetScore()
	{
		return currentScore;
	}

	public void AddToScore()
	{
		currentScore += pointsPerBlockDestroyed * (currentLives + 1);
	}

	public int GetLives()
	{
		return currentLives;
	}

	public void AddLife()
	{
		currentLives++;
	}

	public void RemoveLife()
	{
		if (currentLives <= 0)
		{
			currentLives = 0;
			sceneLoader.LoadGameOver();
		}
		else
		{
			currentLives--;
			FindObjectOfType<Paddle>().ResetPosition();
			FindObjectOfType<Ball>().ResetBall();
		}
	}

	public void EndGame()
	{
		Destroy(gameObject);
	}

	public bool IsAutoPlayEnabled()
	{
		return isAutoPlayEnabled;
	}
}
