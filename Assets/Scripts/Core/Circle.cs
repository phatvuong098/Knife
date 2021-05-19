using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Circle : NetworkBehaviour
{
    Rigidbody rigid;
    float speed = 30;
    Transform mts;
    private float interval;

    void Start()
    {
        mts = transform;
    }

    public void Update()
    {
        mts.Rotate(0, 0, speed * Time.deltaTime);

        if (isClient)
            return;

        interval += Time.deltaTime;

        if (interval >= 3)
        {
            RPC_ChangeSpeed(Random.Range(-90, 90), NetworkTime.time);
            interval = 0;
        }
    }

    [ClientRpc]
    private void RPC_ChangeSpeed(float speed, double time)
    {
        double interval = NetworkTime.time - time;
        StartCoroutine(ChangeSpeedCoro(speed, (float)(3 - interval)));
    }

    IEnumerator ChangeSpeedCoro(float speed, float wait)
    {
        yield return new WaitForSeconds(wait);
        this.speed = speed;
    }
}
