using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float xMin = 1f;
    [SerializeField] float xMax = 15f;


    //cached refer
   // GameSession theGameSession;
    //Ball theBall;
    public Vector2 paddlePods;

    // Use this for initialization
    void Start() {
        //theGameSession = FindObjectOfType<GameSession>();
        //theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    public void Update()
    {
        paddlePods = new Vector2(transform.position.x, transform.position.y);
        paddlePods.x = Mathf.Clamp(GetXPos(), xMin, xMax);
        transform.position = paddlePods;
    }
    private float GetXPos()
    {/*
        if(theGameSession.isActiveAndEnabled == true) 
        {
            return theBall.transform.position.x;
        }
        else
        {*/
           return Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
        
    }



}
