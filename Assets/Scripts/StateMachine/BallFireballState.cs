using UnityEngine;
using DG.Tweening;
public class BallFireballState : BallBaseState
{
    public override void EnterState(BallStateManager ball)
    {

    }

    public override void UpdateState(BallStateManager ball)
    {

    }
    public override void OnCollisionEnter(BallStateManager ball, Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallStateManager>(out BallStateManager ballStateManager))
        {

            if (!BallManager.Instance.balls.Contains(ball))
            {
                //if (collision.gameObject.CompareTag("ActiveFollowerBall"))
                //{
                ContactPoint contact = collision.contacts[0];
                Vector3 collisionDir = (contact.point - collision.transform.position);
                float angle = Vector3.Angle(collisionDir, collision.transform.forward);

                int collisionIndex = BallManager.Instance.balls.IndexOf(ballStateManager); 
                int replacementIndex;

                if (angle < 90)
                {
                    replacementIndex = collisionIndex + 1;
                }

                else
                {
                    replacementIndex = collisionIndex;
                } 

                BallManager.Instance.balls.Insert(replacementIndex, ball);

                ball.gameObject.transform.DOMove(RouterManager.Instance.pathCreator.path.GetClosestPointOnPath(ball.transform.position), 0.04f);
                BallManager.Instance.StartImpactBallStack(ball, replacementIndex);

                if (collision.gameObject.CompareTag("ActiveFollowerBall"))
                {
                    ball.SwitchState(BallState.ActiveFollowPath);
                }

                else if (collision.gameObject.CompareTag("PassiveFollowerBall"))
                {
                    ball.SwitchState(BallState.PassiveFollowPath);
                }

                //}

                //else if (collision.gameObject.CompareTag("PassiveFollowerBall"))
                //{
                //    ball.SwitchState(BallState.PassiveFollowPath);
                //}
            }
        }
    }


}
