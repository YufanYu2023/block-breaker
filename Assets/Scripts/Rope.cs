using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float minRandom = 0f;
    public float maxRandom = 4f;

    public GameObject catcher;

    public enum State
    {
       extend,
       takeback,
       still,
    }

    public State myState;

    float length = 1;
    [SerializeField]float Lspeed =1f;


    // Start is called before the first frame update
    void Start()
    {
        myState = State.still;
        StartCoroutine(RandomFactor());
    }

    // Update is called once per frame
    void Update()
    {

        if (myState == State.extend)
        {
            Extend();
        }
        else if (myState==State.takeback)
        {
            TakeBack();
        }
        else
        {
            StayStill();
        }
    }
    //延长
    private void Extend()
    {
        length += 0.15f * Lspeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        

        if (length>=8)//限制抓取范围
        {
            myState = State.takeback;
        }
    }

    //收回
    private void TakeBack()
    {
        length -= 0.3f * Lspeed;
        transform.localScale = new Vector3(transform.localScale.x,length, transform.localScale.z);
        if (length<0.3f)
        {
            myState = State.still;
            StartCoroutine(RandomFactor());
        }
    }
    //收回静止
    private void StayStill()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
    }
    //随机抓取
    IEnumerator RandomFactor()
    {
        yield return new WaitForSeconds(Random.Range(minRandom, maxRandom));
        if (!FindObjectOfType<Catcher>().GetComponent<Catcher>().throwAway)
        {
            myState = State.extend;
        }
    }

}
