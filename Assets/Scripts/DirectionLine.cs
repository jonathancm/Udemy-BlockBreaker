using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionLine : MonoBehaviour {

	// Configurable Parameters
	[SerializeField] float rotationSpeed = 40.0f;

	// Cached References
	Ball ball;
	Vector3 rotationDirZ = new Vector3(0, 0, 1);

	// Internal Variables
	float prevBallPosX;
	float ballPosX;

	void Start()
	{
		ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();

		prevBallPosX = ball.transform.position.x;
		ballPosX = ball.transform.position.x;
		ResetPosition();
	}

	private void ResetPosition()
	{
		Vector3 linePos = ball.transform.position + new Vector3(0, 1.75f, 0);
		transform.position = linePos;
		transform.rotation = Quaternion.identity;
	}

	void Update()
	{
		Vector3 ballPos = ball.transform.position;
		Vector3 dirLinePos = transform.position;

		float angleZ = transform.eulerAngles.z;
		if (angleZ >= 30.0f && angleZ <= 180.0f)
		{
			// Rotate Clockwise
			rotationDirZ = new Vector3(0, 0, -1);
		}
		else if (angleZ > 180.0f && angleZ <= 330.0f)
		{
			// Rotate Counter-Clockwise
			rotationDirZ = new Vector3(0, 0, 1);
		}
		else
		{
			// Keep rotation direction
		}

		// Translate Line
		ballPosX = ballPos.x;
		transform.position += new Vector3(ballPosX - prevBallPosX, 0,0);
		prevBallPosX = ballPos.x;

		// Rotate Line
		transform.RotateAround(ballPos, rotationDirZ, Time.deltaTime * rotationSpeed);
		
	}

	public float GetLaunchDirection()
	{
		return transform.eulerAngles.z + 90f;
	}
}
