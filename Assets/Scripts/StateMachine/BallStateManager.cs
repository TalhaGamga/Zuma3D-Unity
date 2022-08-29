using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum StateType
//{
//    FireBallState,
//    ActiveFollowPathState,
//    PassiveFollowPathState //Create passive follow path state
//}
public class BallStateManager : MonoBehaviour 
{
    public string color;

    public BallBaseState currentState; //Holds a reference to the active state in a state machine (State machines can only be in one state at a time.)
    public BallFireballState FireballState = new BallFireballState();
    public ActiveBallFollowPathState ActiveFollowPathState = new ActiveBallFollowPathState();
    public PassiveBallFollowPathState PassiveFollowPathState = new PassiveBallFollowPathState();

    //public StateType currentStateType;
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
                break;
            case BallState.PassiveFollowPath:
                currentState = PassiveFollowPathState;
                currentState.EnterState(this);
                break;
            case BallState.Fireball:
                currentState = FireballState;
                //currentState.EnterState(this);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void InitState(BallState state)
    {
        //Debug.Log(state);
        switch (state)
        {
            case BallState.ActiveFollowPath:
                currentState = ActiveFollowPathState;

                //currentStateType = StateType.ActiveFollowPathState;
                //currentState.EnterState(this);
                break;
            case BallState.Fireball:
                currentState = FireballState;
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
