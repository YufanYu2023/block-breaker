using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    //config params
    [Range(0.1f,10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPreBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    //[SerializeField]  public bool isAutoPlayEnabled;

    //state varibals
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        
        int gameStatutsCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatutsCount > 1)
        {
            gameObject.SetActive(false);//singleton问题的解决方案（destroy在所有之后执行，有可能在一瞬间有两个gameobject）
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }



    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

 

	
	// Update is called once per frame
	void Update () {
        Time.timeScale = gameSpeed;
        scoreText.text = currentScore.ToString();
	}

    public void AddToScore()
    {
        currentScore = currentScore + pointsPreBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void destroyGameStatus()
    {
        Destroy(gameObject);
    }

   /* public bool IsAutoPlayEnabled()
    {

            return isActiveAndEnabled;
    } */


}


