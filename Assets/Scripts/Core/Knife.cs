using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Knife : NetworkBehaviour
{
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    BoxCollider2D colider;

    bool isHited;
    Transform mts;

    private void Awake()
    {
        mts = transform;
        rigid.isKinematic = true;
    }

    public void OnEnable()
    {
        rigid.isKinematic = false;
        rigid.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasAuthority)
            return;

        if (isHited)
            return;

        if (collision.transform.CompareTag("Boss"))
        {
            isHited = true;
            rigid.velocity = Vector2.zero;
            rigid.bodyType = RigidbodyType2D.Static;
            mts.SetParent(GameObject.FindGameObjectWithTag("Boss").transform, true);
            CMD_VaCham(true, this.mts.localPosition, this.mts.localRotation);
        }
        else if (collision.transform.CompareTag("Knife"))
        {
            isHited = true;
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = Random.Range(20f, 50f) * 25f;
            colider.enabled = false;
            rigid.AddForce(new Vector2(Random.Range(-5f, 5f), -30f), ForceMode2D.Impulse);
            CMD_VaCham(false, this.mts.localPosition, this.mts.localRotation);
        }
    }

    [Command]
    private void CMD_VaCham(bool isSuccess, Vector2 location, Quaternion rotation)
    {
        RPC_KnifeResult(isSuccess, location, rotation);
    }

    [ClientRpc]
    private void RPC_KnifeResult(bool isSuccess, Vector2 location, Quaternion rotation)
    {
        if (hasAuthority)
            return;

        if (isSuccess)
        {
            rigid.velocity = Vector2.zero;
            rigid.bodyType = RigidbodyType2D.Static;
            mts.SetParent(GameObject.FindGameObjectWithTag("Boss").transform, true);
            mts.localPosition = location;
            mts.localRotation = rotation;
        }
        else
        {
            rigid.velocity = Vector2.zero;
            rigid.angularVelocity = Random.Range(20f, 50f) * 25f;
            colider.enabled = false;
            rigid.AddForce(new Vector2(Random.Range(-5f, 5f), -30f), ForceMode2D.Impulse);
        }
    }

}
