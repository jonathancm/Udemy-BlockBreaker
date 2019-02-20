using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	// Configuration Parameters
	[Header("Paddle")]
	[SerializeField] Paddle paddle = null;
	[SerializeField] GameObject directionArrow = null;

	[Header("Ball Movement")]
	[SerializeField] float maxVelocity = 15f;
	[SerializeField] float randomFactor = 0.2f;

	[Header("Special Effects")]
	[SerializeField] AudioClip[] ballSounds = null;

	// state
	Vector2 paddleToBallVector;
	bool hasStarted = false;
	GameObject dirLine;

	// Cached Components
	AudioSource audioSource;
	Rigidbody2D rigidBody2D;
	TrailRenderer trailRenderer;

    // Use this for initialization
    void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		rigidBody2D = GetComponent<Rigidbody2D>();
		trailRenderer = GetComponent<TrailRenderer>();

		paddleToBallVector = transform.position - paddle.transform.position;
		dirLine = Instantiate(directionArrow, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!hasStarted)
		{
			LockBallToPaddle();
			LaunchOnMouseClick();
		}
	}

	private void LaunchOnMouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			float launchAngle = 45;

			// Calulate Launch Vector
			if (dirLine)
			{
				launchAngle = dirLine.GetComponent<DirectionLine>().GetLaunchDirection();
			}
			rigidBody2D.velocity = CalculateLaunchVector(launchAngle, maxVelocity);

			// Update ball parameters
			hasStarted = true;
			trailRenderer.enabled = true;
			dirLine.SetActive(false);
		}
	}

	private Vector2 CalculateLaunchVector(float degreeAngle, float magnitude)
	{
		float angle = degreeAngle * Mathf.Deg2Rad;
		return new Vector2(magnitude * Mathf.Cos(angle), magnitude * Mathf.Sin(angle));
	}

	private void LockBallToPaddle()
	{
		Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
		transform.position = paddlePos + paddleToBallVector;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		float velocityX;
		float velocityY;
		Vector2 velocityTweak;
		AudioClip clip;

		if (!hasStarted)
		{
			return;
		}

		velocityX = Random.Range(-1f * randomFactor, randomFactor);
		velocityY = Random.Range(-1f * randomFactor, randomFactor);
		velocityTweak = new Vector2(velocityX, velocityY);
		rigidBody2D.velocity += velocityTweak;

		rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, maxVelocity);
		clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
		audioSource.PlayOneShot(clip);
	}

	public void ResetBall()
	{
		// Update Ball Parameters
		hasStarted = false;
		trailRenderer.enabled = false;

		// Stop ball and setup launch mode
		rigidBody2D.velocity = new Vector2(0f, 0f);
		LockBallToPaddle();
		dirLine.SetActive(true);
	}
}
