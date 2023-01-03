using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public Transform rope;
    public Transform worldSpace;
    public GameObject hook;
    public GameObject ball;
    public bool throwAway;
    public Transform m_Target;
    Collider2D blockcollider;

    private Rigidbody2D rb;
    private bool lunchMissle = false;

    [SerializeField] float m_Speed = 5f;
    [SerializeField] float acceleration = 200f;
 

    // Update is called once per frame
    void Update()
    {
        transform.position = rope.position;
        transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,rope.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        if (lunchMissle && blockcollider)
        {
            rb = blockcollider.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (Vector2)m_Target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, blockcollider.gameObject.transform.up).z;
            rb.angularVelocity = -rotateAmount * acceleration;
            rb.velocity = blockcollider.gameObject.transform.up * m_Speed;
            //StartCoroutine(ChangeMissle());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag!="Ball")
        {
            blockcollider = collision;
            collision.transform.parent = transform;
            hook.GetComponent<Rope>().myState = Rope.State.takeback;
            lunchMissle = false;
        }

        if (collision)
        {
            throwAway = true;
             StartCoroutine(RandomThrow());
        }
    }

    IEnumerator RandomThrow()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(.5f, 2f));
        if (blockcollider)
        {
            lunchMissle = true;
            blockcollider.transform.parent = worldSpace;
            throwAway = false;
            Destroy(blockcollider.gameObject, 5);
            //blockcollider.transform.position = ball.transform.position + offSet;
        }     
        //blockcollider.gameObject.GetComponent<Rigidbody2D>().bodyType = 0;    
    }

    IEnumerator ChangeMissle()
    {
        yield return new WaitForSeconds(2);
        lunchMissle = false;
    }
}
