using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMechanic : MonoBehaviour
{
    public Transform firePos;
    GameObject fireBall;
    ColorType randomColor;
    GameObject ballObj;
    private void Start()
    {
        StartCoroutine(IESpawnBall());
    }

    private void Update()
    {
        Fire();
    }

    GameObject CreateBall()
    {
        ballObj = BallManager.Instance.ballPrefabs[(int)ChooseRandomColor()];
        return ballObj;
    }

    ColorType ChooseRandomColor()
    {
        ColorType randomColor = BallManager.Instance.colorTypes[Random.Range(0, 4)];
        return randomColor;
    }

    IEnumerator IESpawnBall()
    {
        yield return new WaitForSeconds(.5f);
        fireBall = Instantiate(CreateBall(), firePos.position, Quaternion.identity, transform);
        fireBall.tag = "FireBall";
        fireBall.GetComponent<BallStateManager>().InitState(BallState.Fireball);
        //Debug.Log(fireBall);
    }

    void Fire()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (!fireBall)
            {
                return;
            }

            fireBall.transform.SetParent(null);
            Rigidbody rb = fireBall.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20, ForceMode.Impulse);
            fireBall = null;

            StartCoroutine(IESpawnBall());
        }
    }
}
