using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KnifeManager : NetworkBehaviour
{
    [SerializeField]
    GameObject knife;
    Transform mts;

    void Start()
    {
        mts = transform;
    }

    void Update()
    {
        if (!base.isClient)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && hasAuthority)
        {
            CMD_CreateKnife();
        }
    }

    [Command]
    private void CMD_CreateKnife()
    {
        GameObject obj = Instantiate(knife, mts.position, Quaternion.identity);
        NetworkServer.Spawn(obj, connectionToClient);
    }
}
