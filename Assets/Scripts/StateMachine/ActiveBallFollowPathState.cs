using UnityEngine;
using PathCreation;
public class ActiveBallFollowPathState : BallBaseState
{
    public float distanceTravelled = 0;
    int index;

    public int multiplier = 1;
    bool canGo = true;
    public override void EnterState(BallStateManager ball)
    {
        distanceTravelled = RouterManager.Instance.pathCreator.path.GetClosestDistanceAlongPath(ball.transform.position);
    }
    float distanceToGo;
    public override void UpdateState(BallStateManager ball)
    {
        index = BallManager.Instance.balls.IndexOf(ball);

        distanceToGo = (RouterManager.Instance.distanceTravelled + index); 

        distanceTravelled = Mathf.Lerp(distanceTravelled, distanceToGo, 0.2f);
        ball.transform.position = RouterManager.Instance.pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        ball.transform.rotation = RouterManager.Instance.pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }

    public override void OnCollisionEnter(BallStateManager ball, Collision collision)
    {

    }

    //public override void ResetVar(BallStateManager ball)
    //{
    //    Debug.Log("Hello from reset var");

    //    if (canGo)
    //    {
    //        canGo = false;
    //    }
    //    else if (!canGo)
    //    {
    //        canGo = true;
    //    }

    //    if (multiplier == 0)
    //    {
    //        multiplier = 1;
    //        Debug.Log(multiplier);
    //    }
    //}
}
