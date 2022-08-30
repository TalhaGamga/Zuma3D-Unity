using UnityEngine;

/*
  Abstract on each method means we need to define their
  functionality in classes that derive from BallBaseState
 */

public enum BallState
{
    ActiveFollowPath,
    PassiveFollowPath,
    Fireball

}

public abstract class BallBaseState
{
    public abstract void EnterState(BallStateManager ball);
    public abstract void UpdateState(BallStateManager ball);
    public abstract void OnCollisionEnter(BallStateManager ball, Collision collision);
}
