using UnityEngine;
using PathCreation;
public class PassiveBallFollowPathState : BallBaseState
{
    float offset;
    float distanceToWait;

    int index;
    public override void EnterState(BallStateManager ball)
    {

        offset = RouterManager.Instance.distanceTravelled;
        //distanceToWait = BallManager.Instance.balls.IndexOf(ball) + offset;

        distanceToWait = RouterManager.Instance.pathCreator.path.GetClosestDistanceAlongPath(ball.transform.position);
    }
    public override void UpdateState(BallStateManager ball)
    {
        //distanceToWait = Mathf.Lerp(distanceToWait, BallManager.Instance.balls.IndexOf(ball) + offset, 0.1f); 
        ball.transform.position = RouterManager.Instance.pathCreator.path.GetPointAtDistance(distanceToWait, EndOfPathInstruction.Stop);

    }

    public override void OnCollisionEnter(BallStateManager ball, Collision collision)
    {

        int index = BallManager.Instance.balls.IndexOf(ball);

        for (int i = index; i < BallManager.Instance.balls.Count; i++)
        {
            BallManager.Instance.balls[i].SwitchState(BallState.ActiveFollowPath);
            BallManager.Instance.balls[i].tag = "ActiveFollowerBall";
        }

    }

    public override void ResetVar(BallStateManager ball)
    {
    }
}
