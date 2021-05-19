using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace LearnMirror
{
    public class MyNetworkManager : NetworkManager
    {
        GameObject circle;
        private UI_Connect ui_Connect;

        public override void Awake()
        {
            base.Awake();
            ui_Connect = GetComponent<UI_Connect>();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, player);

            if (numPlayers == 2)
            {
                circle = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Circle"));
                NetworkServer.Spawn(circle);
            }
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);

            ui_Connect.OnDisconnect();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            ui_Connect.OnConnected();
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            if (circle != null)
                NetworkServer.Destroy(circle);

            base.OnServerDisconnect(conn);
        }
    }
}