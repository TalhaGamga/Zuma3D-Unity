using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class RouterManager : MonoBehaviour
{
    public PathCreator pathCreator;

    private static RouterManager instance = null;
    public static RouterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("RouterManager").AddComponent<RouterManager>();
            }
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    public float distanceTravelled = 0;
    public float speed = 1;

    private void Start()
    {
    }
    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }


}
