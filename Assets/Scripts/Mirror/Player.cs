using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    List<GameObject> _prefabsPlayers;

    private void Awake()
    {
        UI_KnifeSelect.OnKnifeSelect += OnKnifeSelect;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        UI_KnifeSelect.ShowPanel();
    }

    private void OnKnifeSelect(string name)
    {
        if (hasAuthority)
            SpawKnifeManager(name);
    }

    [Command]
    private void SpawKnifeManager(string name)
    {
        Debug.Log(name);
        GameObject obj = Instantiate(_prefabsPlayers.Find(s => s.name == name));
        NetworkServer.Spawn(obj, connectionToClient);
    }
}

