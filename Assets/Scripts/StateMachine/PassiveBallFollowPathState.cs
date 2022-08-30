using UnityEngine;
using PathCreation;

public class PassiveBallFollowPathState : BallBaseState
{
    float offset;
    float distanceToWait;

    public override void EnterState(BallStateManager ball)
    {
        offset = BallManager.Instance.passiveBallOffset;
        distanceToWait = offset + BallManager.Instance.deletedNum + BallManager.Instance.balls.IndexOf(ball);
    }

    public override void UpdateState(BallStateManager ball)
    {
        distanceToWait = Mathf.Lerp(distanceToWait, offset + BallManager.Instance.deletedNum + BallManager.Instance.balls.IndexOf(ball), 0.08f);
        ball.transform.position = RouterManager.Instance.pathCreator.path.GetPointAtDistance(distanceToWait, EndOfPathInstruction.Stop);
    }

    public override void OnCollisionEnter(BallStateManager ball, Collision collision)
    {
        if (collision.gameObject.CompareTag("ActiveFollowerBall"))
        {
            BallManager.Instance.SwitchBallStateToFollow(ball);
            BallManager.Instance.isPassiveBallCorrupted = false;
        }
    }
}
