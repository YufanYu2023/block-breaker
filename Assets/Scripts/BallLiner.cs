using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLiner : MonoBehaviour {

    LineRenderer lineRenderer;
    //public EdgeCollider2D edgeCollider;
    GameObject cloneLine;


    public GameObject movingBlock;
    public GameObject linePrefab;
    public GameObject theBall;
    public Ball ballScript;
    MoveBlock moveBlock;

    public bool shouldStop = false;
    public Vector2 intersrctPoint;

    Vector2 ballStart;
    Vector2 ballNext;
    Vector2 blockPosition;


    List<Vector2> ballPositions;

    [SerializeField] float startWidth = 0.1f;
    [SerializeField] float endWidth = 0.2f;
    //[SerializeField] float clearTime = 0.5f;

    // Use this for initialization
    void Start ()
    {
        moveBlock = movingBlock.GetComponent<MoveBlock>();
        CreateLine();
      // ballPositions.Clear();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ballScript.hasClicked)
        {
            if (ballScript.collisonNumber<4 && ballScript.collisonNumber>=1)
            {
                ballNext = theBall.transform.position;
                if (ballStart != ballNext)
                {
                    UpdateLine();
                }
                ballStart = ballNext;
            }
            else if(ballScript.collisonNumber>=4)
            {
                ballScript.CheckTriangle();
                if(ballScript.isTriangle == true)//形成三角形
                {
                    //edgeCollider.points = ballScript.ballPositions.ToArray();
                    ClearLines();                   
                    IsBlockInside();
                }
                else if(ballScript.isTriangle == false)
                {
                    ClearLineRightAway();
                }

            }
        }

    }

    private void CreateLine()
    {
        ballStart = theBall.transform.position;
        cloneLine = (GameObject)Instantiate(linePrefab, theBall.transform.position, theBall.transform.rotation);
        lineRenderer = cloneLine.GetComponent<LineRenderer>();
        lineRenderer.startWidth = startWidth; lineRenderer.endWidth = endWidth;
        lineRenderer.positionCount = 0;

        //edgeCollider = cloneLine.GetComponent<EdgeCollider2D>();

    }

    private void UpdateLine()
    {       
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, theBall.transform.position);

       // Debug.Log(ballScript.collisonNumber);
      //Debug.Log(ballScript.ballPositions[ballScript.collisonNumber - 1]);
    }

    private void ClearLines()
    {
        if (ballScript.collisonNumber >= 8)
        {
        lineRenderer.positionCount = 0;
        ballScript.collisonNumber = 0;
        ballScript.isTriangle = false;
        ballScript.ballPositions.Clear();
        //edgeCollider.points = ballScript.ballPositions.ToArray();
        }

        
    }

    private void ClearLineRightAway()
    {
        lineRenderer.positionCount = 0;
        ballScript.collisonNumber = 0;
        ballScript.ballPositions.Clear();
    }

    //移动砖块是否在三角形内
    private void IsBlockInside()
    { 
        if (moveBlock)
        {
            intersrctPoint = IntersectionPoint(ballScript.ballPositions[0], ballScript.ballPositions[1], ballScript.ballPositions[2], ballScript.ballPositions[3]);
            blockPosition = moveBlock.transform.position;
            if (pointInTriangle(intersrctPoint.x, intersrctPoint.y,
                ballScript.ballPositions[1].x, ballScript.ballPositions[1].y,
                ballScript.ballPositions[2].x, ballScript.ballPositions[2].y,
                blockPosition.x, blockPosition.y))
            {
                moveBlock.Stop();
            }
        }
    }

    //计算线段交点
    public Vector2 IntersectionPoint(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        // Line AB represented as a1x + b1y = c1  
        float a1 = B.y - A.y;
        float b1 = A.x - B.x;
        float c1 = a1 * (A.x) + b1 * (A.y);

        // Line CD represented as a2x + b2y = c2  
        float a2 = D.y - C.y;
        float b2 = C.x - D.x;
        float c2 = a2 * (C.x) + b2 * (C.y);

        float determinant = a1 * b2 - a2 * b1;

        float x = (b2 * c1 - b1 * c2) / determinant;
        float y = (a1 * c2 - a2 * c1) / determinant;
        return new Vector2(x, y);

    }

    //点是否在三角形内
    public bool IsinsideTriangle(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
    {
        float s1 = Area(a, b, c);
        float s2 = Area(point, a, b);
        float s3 = Area(point, a, c);
        float s4 = Area(point, b, c);
        //需考虑边界情况
        if (s2 == 0 || s3 == 0 || s4 == 0) return false;
        //不能用“==”判断两个浮点类型的值是否相等，可使用如下，差小于等于某个精度值即可。
        if (s1 - (s2 + s3 + s4) <= 0.00001f) return true;
        return false;
    }
    float Area(Vector2 a, Vector2 b, Vector2 c)//计算三角形面积
    {
        //海伦公式：p=(a+b+c)/2; S = √[p(p-a)(p-b)(p-c)] //这里a,b,c代表边长
        float dab = Vector2.Distance(a, b);
        float dac = Vector2.Distance(a, c);
        float dbc = Vector2.Distance(b, c);
        float half = (dab + dac + dbc) / 2;
        return Mathf.Sqrt(half * (half - dab) * (half - dac) * (half - dbc));
    }

    bool pointInTriangle(float x1, float y1, float x2, float y2, float x3, float y3, float x, float y)
    {
        float denominator = ((y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3));
        float a = ((y2 - y3) * (x - x3) + (x3 - x2) * (y - y3)) / denominator;
        float b = ((y3 - y1) * (x - x3) + (x1 - x3) * (y - y3)) / denominator;
        float c = 1 - a - b;

        return 0 <= a && a <= 1 && 0 <= b && b <= 1 && 0 <= c && c <= 1;
    }
}
