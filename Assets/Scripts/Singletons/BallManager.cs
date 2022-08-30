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

    private void OnDisable()
    {
    }

    public Transform spawnPoint;
    GameObject ballObj;

    List<BallStateManager> tempList;

    private void Start()
    {
        SpawnBall();
    }
    private void Update()
    {
    }

    public List<ColorType> colorTypes = new List<ColorType> { ColorType.Blue, ColorType.Green, ColorType.Red, ColorType.Yellow };
    public List<GameObject> ballPrefabs;
    public List<BallStateManager> balls;

    public List<BallStateManager> passiveFollowerBalls;

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

    void SpawnBall()
    {
        for (int i = 0; i < counter; i++)
        {
            BallStateManager ballStateManager = Instantiate(CreateBall(), spawnPoint.position, Quaternion.identity).GetComponent<BallStateManager>();
            balls.Add(ballStateManager);
            ballStateManager.InitState(BallState.ActiveFollowPath);
        }
    }

    public int deletedNum;
    public float passiveBallOffset;
    public bool isPassiveBallCorrupted = false;
    public IEnumerator ImpactBallStack(BallStateManager ball, int index)
    {
        tempList = new List<BallStateManager>();
        tempList.Clear();
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

            deletedNum = tempList.Count;

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
                        if (!isPassiveBallCorrupted)
                        {
                            passiveBallOffset = RouterManager.Instance.distanceTravelled;
                            isPassiveBallCorrupted = true;
                        }

                        for (int i = previous + 1; i < balls.Count; i++)
                        {
                            balls[i].SwitchState(BallState.PassiveFollowPath);
                        }
                    }
                }
            }
        }
    }

    //public void ClearInterval(int first, int last)
    //{
    //    for (int i = first + 1; i < last; i++)
    //    {
    //        balls.RemoveAt(i);
    //    }
    //}

    public void StartImpactBallStack(BallStateManager ball, int index)
    {
        StartCoroutine(ImpactBallStack(ball, index));
    }

    public void SwitchBallStateToFollow(BallStateManager ball)
    {
        int index = balls.IndexOf(ball);
        for (int i = index; i < balls.Count; i++)
        {
            balls[i].SwitchState(BallState.ActiveFollowPath);
        }
    }

}
