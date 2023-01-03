using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle Paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0f;


    //state
    //Vector2 paddleToBallVector;
    //Vector2 clickPosition;

    //public Paddle paddleScript;
    public List<Vector2> ballPositions;
    public bool hasClicked = false;
    public int collisonNumber = 0;
    public bool isTriangle = false;

    bool wetherIncludeEndPoints;

    //Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    MoveBlock moveBlock;


    // Use this for initialization
    void Start()
    {
        //paddleToBallVector = transform.position - Paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasClicked == false)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
        else
        {
            Destroy(Paddle1.GetComponent<Collider2D>());
        }
    }


    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
            hasClicked = true;
            //clickPosition = paddleScript.paddlePods;

        }
    }

    private void LockBallToPaddle()
    {
        Vector2 padllePods = new Vector2(Paddle1.transform.position.x, Paddle1.transform.position.y);
        transform.position = padllePods;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
              (Random.Range(0f, randomFactor),
              Random.Range(0f, randomFactor));

        if (hasClicked)
        {
            collisonNumber++;
            ballPositions.Add(gameObject.transform.position);
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;//velocity速度
        }
    }

    public void CheckTriangle()
    {
        //检测三条线是否两两相交
        if (AreLinesIntersecting(ballPositions[0], ballPositions[1], ballPositions[2], ballPositions[3], wetherIncludeEndPoints))
        {
            isTriangle = true;
        }
    }

    public bool AreLinesIntersecting(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
    {
        //To avoid floating point precision issues we can add a small value
        float epsilon = 0.00001f;

        bool isIntersecting = false;

        float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

        //Make sure the denominator is > 0, if not the lines are parallel
        if (denominator != 0f)
        {
            float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
            float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

            //Are the line segments intersecting if the end points are the same
            if (shouldIncludeEndPoints)
            {
                //Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                if (u_a >= 0f + epsilon && u_a <= 1f - epsilon && u_b >= 0f + epsilon && u_b <= 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }
            else
            {
                //Is intersecting if u_a and u_b are between 0 and 1
                if (u_a > 0f + epsilon && u_a < 1f - epsilon && u_b > 0f + epsilon && u_b < 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }
        }

        return isIntersecting;
    }


}
