using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    public int SpawnDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        //check if this is my locally controlled object
        if (isLocalPlayer == false)
        {
            //this is not mine
            return;
        }
        CmdSpawnPlayer();
    }

    public GameObject PlayerPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    public GameObject myPlayer;

    public string PlayerName = "Anon";

    [Command]
    void CmdSpawnPlayer()
    {
        GameObject obj = Instantiate(PlayerPrefab);
        myPlayer = obj;
        NetworkServer.SpawnWithClientAuthority(obj, connectionToClient);
        RpcAssignPlayerObj(myPlayer);
    }


    [ClientRpc]
    void RpcAssignPlayerObj(GameObject obj)
    {
        myPlayer = obj;
        AssignSpawn();
    }

    void AssignSpawn()
    {
        if (connectionToServer.connectionId == 0)
        {
            myPlayer.GetComponent<PlayerController>().setSpawn(new Vector3(-SpawnDistance, 0, 0));
            return;
        }
        myPlayer.GetComponent<PlayerController>().setSpawn(new Vector3(SpawnDistance, 0, 0));
    }


}
