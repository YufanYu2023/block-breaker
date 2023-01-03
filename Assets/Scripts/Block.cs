using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakingSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitsSprites;

    //cahed reference
    Level level;

    //state varibales
    [SerializeField] int timesHit;//for bug


    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void PlayBlockDestroySXF()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakingSound, Camera.main.transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)//检测碰撞
    {
        if (tag =="Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitsSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            showNextHitSprites();
        }
    }

    private void showNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitsSprites[spriteIndex]!= null)
        {
            GetComponent<SpriteRenderer>().sprite = hitsSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is mising from array" +gameObject.name);
        }

    }

    private void DestroyBlock ()
    {
        PlayBlockDestroySXF();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }


}
