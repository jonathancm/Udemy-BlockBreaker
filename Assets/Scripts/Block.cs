using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	// Configuration parameters
	[SerializeField] AudioClip breakSound = null;
	[SerializeField] GameObject blockSparklesVFX = null;
	[SerializeField] Sprite[] hitSprites = null;

	// Cached References
	LevelLogic level;
	GameSession gameSession;

	// State Variables
	[SerializeField] int timesHit; //TODO: serialized for debug purposes

	private void Start()
	{
		level = FindObjectOfType<LevelLogic>();
		gameSession = FindObjectOfType<GameSession>();

		CountBreakableBlocks();
	}

	private void CountBreakableBlocks()
	{
		if (tag == "Breakable")
		{
			level.AddBlockToCount();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(tag == "Breakable")
		{
			HandleHit();
		}
	}

	private void HandleHit()
	{
		int maxHits = 0;

		timesHit++;
		maxHits = hitSprites.Length;
		if (timesHit >= maxHits)
		{
			DestroyBlock();
		}
		else
		{
			ShowNextHitSprite();
		}
	}

	private void ShowNextHitSprite()
	{
		int spriteIndex = timesHit;

		if (hitSprites[spriteIndex] != null)
		{
			GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
		{
			Debug.LogError("Block sprite is missing from array for " + gameObject.name);
		}
	}

	private void DestroyBlock()
	{
		// User feedback
		PlayBlockDestroySFX();
		TriggerSparklesVFX();
		gameSession.AddToScore();

		// Level Logic
		level.RemoveBlockFromCount();

		Destroy(gameObject);
	}

	private void PlayBlockDestroySFX()
	{
		AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
	}

	private void TriggerSparklesVFX()
	{
		GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
		Destroy(sparkles,2);
	}
}
