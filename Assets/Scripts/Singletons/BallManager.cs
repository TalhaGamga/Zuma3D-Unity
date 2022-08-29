using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType
{
    Blue,
    Green,
    Red,
    Yellow
}
public class BallManager : MonoBehaviour
{
    private static BallManager instance = null;
    public static BallManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("BallManager").AddComponent<BallManager>();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    public Transform spawnPoint;
    GameObject ballObj;

    List<BallStateManager> tempList;

    private void Start()
    {
        StartCoroutine(IESpawnBall());
    }
    private void Update()
    {
    }

    public List<ColorType> colorTypes = new List<ColorType> { ColorType.Blue, ColorType.Green, ColorType.Red, ColorType.Yellow };
    public List<GameObject> ballPrefabs;
    public List<BallStateManager> balls;

    int counter = 10;

    public int previous;
    public GameObject tempParent;
    public GameObject CreateBall()
    {
        ballObj = ballPrefabs[(int)ChooseRandomColor()];
        return ballObj;
    }

    public ColorType ChooseRandomColor()
    {
        ColorType randomColor = colorTypes[Random.Range(0, 4)];
        return randomColor;
    }

    IEnumerator IESpawnBall()
    {
        for (int i = 0; i < counter; i++)
        {
            yield return new WaitForSeconds(0f);
            BallStateManager ballStateManager = Instantiate(CreateBall(), spawnPoint.position, Quaternion.identity).GetComponent<BallStateManager>();
            ballStateManager.tag = "ActiveFollowerBall";
            balls.Add(ballStateManager);
            ballStateManager.InitState(BallState.ActiveFollowPath);
        }
    }

    public IEnumerator ImpactBallStack(BallStateManager ball, int index)
    {
        tempList = new List<BallStateManager>();

        previous = index - 1;

        if (previous >= 0)
        {
            while (balls[previous].color == ball.color)
            {
                tempList.Add(balls[previous]);

                previous--;

                if (previous < 0)
                {
                    break;
                }
            }
        }


        while (balls[index].color == ball.color)
        {
            tempList.Add(balls[index]);
            index++;
            if (index > balls.Count - 1)
            {
                break;
            }
        }


        if (tempList.Count > 3)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < tempList.Count; i++)
            {
                balls.Remove(tempList[i]);
                Destroy(tempList[i].gameObject);
            }

            tempList.Clear();
            if (previous >= 0)
            {
                if (balls.Count > 2 && previous + 1 < balls.Count)
                {
                    if (balls[previous].color == balls[previous + 1].color)
                    {
                        StartCoroutine(ImpactBallStack(balls[previous], previous));
                    }
                    else
                    { 
                        for (int i = previous + 1; i < balls.Count; i++)
                        {
                            balls[i].SwitchState(BallState.PassiveFollowPath);
                            balls[i].tag = "PassiveFollowerBall";
                        }
                    }
                }
            }
        }
    }

    public void StartImpactBallStack(BallStateManager ball, int index)
    {
        StartCoroutine(ImpactBallStack(ball, index));
    }
}
