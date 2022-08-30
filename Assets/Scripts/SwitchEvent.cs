using UnityEngine;
using System;

public class SwitchEvent : MonoBehaviour
{

    public static event Action<BallStateManager> OnPassiveAndActiveBallsCollided;
    void Start()
    {
    }

    void Update()
    {

    }
}
