using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {

    [SerializeField] float clockWaitTime = 2f;
    [SerializeField]float counterclockWaitTime = 3f;

    public GameObject hook;
    public GameObject cather;

    public bool clockWise = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckDirction();
        Spin();
	}

    private void Spin()
    {
        if (hook)
        {
            if (hook.GetComponent<Rope>().myState != Rope.State.extend)
            {
                int direction = Random.Range(-1, 1);
                float speed = Random.Range(.1f, .85f);
                if (clockWise)
                {
                    Vector3 ClockSpinTweak = new Vector3(0, 0, 3);
                    transform.Rotate(ClockSpinTweak * speed);

                }
                if (!clockWise)
                {
                    Vector3 ClockSpinTweak = new Vector3(0, 0, -4);
                    transform.Rotate(ClockSpinTweak * speed);
                }
            }
            else if (hook.GetComponent<Rope>().myState == Rope.State.extend|| hook.GetComponent<Rope>().myState == Rope.State.takeback)//收回或抓取时停止旋转
            {
                Vector3 ClockSpinTweak = new Vector3(0, 0, 0);
                transform.Rotate(ClockSpinTweak * 0f);
            }
        }
        else
        {
            int direction = Random.Range(-1, 1);
            float speed = Random.Range(.1f, .85f);
            if (clockWise)
            {
                Vector3 ClockSpinTweak = new Vector3(0, 0, 3);
                transform.Rotate(ClockSpinTweak * speed);

            }
            if (!clockWise)
            {
                Vector3 ClockSpinTweak = new Vector3(0, 0, -4);
                transform.Rotate(ClockSpinTweak * speed);
            }
        }
    }

    IEnumerator ClockSpin()
    {
        yield return new WaitForSeconds(clockWaitTime);
        clockWise = false;
    }

    IEnumerator counterClockSpin()
    {
        yield return new WaitForSeconds(counterclockWaitTime);
        clockWise = true;
    }

    private void CheckDirction()
    {
        if (clockWise)
        {
            StartCoroutine(ClockSpin());
        }
        if (!clockWise)
        {
            StartCoroutine(counterClockSpin());
        }
    }
}
