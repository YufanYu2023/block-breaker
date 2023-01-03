using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    //BallLiner ballLiner;
    //Ball ball;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //ballLiner = GetComponent<BallLiner>();
        //ball = GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoveingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0);
        }

    }

    bool isMoveingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    public void Stop()
    {
        moveSpeed = 0;
        gameObject.SetActive(false); ;
    }

}
