using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallStateManager : MonoBehaviour
{
    public string color;

    public BallBaseState currentState; //Holds a reference to the active state in a state machine (State machines can only be in one state at a time.)
    public BallFireballState FireballState = new BallFireballState();
    public ActiveBallFollowPathState ActiveFollowPathState = new ActiveBallFollowPathState();
    public PassiveBallFollowPathState PassiveFollowPathState = new PassiveBallFollowPathState();


    void Start()
    {
        color = this.GetComponent<Renderer>().material.color.ToString();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(BallState state)
    {
        switch (state)
        {

            case BallState.ActiveFollowPath:
                currentState = ActiveFollowPathState;
                currentState.EnterState(this);
                gameObject.tag = "ActiveFollowerBall";

                break;
            case BallState.PassiveFollowPath:
                currentState = PassiveFollowPathState;
                currentState.EnterState(this);
                gameObject.tag = "PassiveFollowerBall";
                break;
            case BallState.Fireball:
                currentState = FireballState;
                //currentState.EnterState(this);
                gameObject.tag = "FireBall";

                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void InitState(BallState state)
    {
        switch (state)
        {
            case BallState.ActiveFollowPath:
                currentState = ActiveFollowPathState;
                gameObject.tag = "ActiveFollowerBall";
                //currentStateType = StateType.ActiveFollowPathState;
                //currentState.EnterState(this);
                break;
            case BallState.PassiveFollowPath:
                currentState = PassiveFollowPathState;
                currentState.EnterState(this);
                gameObject.tag = "PassiveFollowerBall";
                break;
            case BallState.Fireball:
                currentState = FireballState;
                gameObject.tag = "FireBall";
                //currentState.EnterState(this);
                break;
        }
    }

    //public void ReplacementFireBall()
    //{
    //    currentState.EnterState(this);
    //}

    //public void ResetVar()
    //{
    //    currentState.ResetVar(this);
    //}
}
