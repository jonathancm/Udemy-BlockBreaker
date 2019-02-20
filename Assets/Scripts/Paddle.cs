using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

	// Configuration Parameters
	[SerializeField] float screenWidth = 16f;
	[SerializeField] float screenPosMin = 1f;
	[SerializeField] float screenPosMax = 15f;

	// Cached Variables
	GameSession gameSession;
	Ball ball;
	Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start()
	{
		gameSession = FindObjectOfType<GameSession>();
		ball = FindObjectOfType<Ball>();
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
		paddlePos.x = Mathf.Clamp(GetXPos(), screenPosMin, screenPosMax);

		transform.position = paddlePos;
	}

	private float GetXPos()
	{
		if (gameSession.IsAutoPlayEnabled())
		{
			// Ball position
			return ball.transform.position.x;
		}
		else
		{
			// Mouse position in screen unit
			return Input.mousePosition.x / Screen.width * screenWidth; ;
		}
	}

	public void ResetPosition()
	{
		Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
		paddlePos.x = Mathf.Clamp(GetXPos(), screenPosMin, screenPosMax);

		transform.position = paddlePos;
	}

	public Vector2 GetPaddleForce(float ballContactPosX)
	{
		Vector2 addedForce;
		float relativePosition;

		relativePosition = ballContactPosX - transform.position.x;

		addedForce = new Vector2(relativePosition, 0f);

		return addedForce;
	}

	public Vector2 GetVelocity()
	{
		return rigidBody2D.velocity;
	}
}