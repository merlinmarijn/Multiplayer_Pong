using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private int[] score = new int[2];
    public GameObject BallPrefab;
    private GameObject BallActive;
    public List<TextMeshProUGUI> ScoreText;
    public AudioSource Audio;
    bool CanReset = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (BallActive == null && NetworkServer.connections.Count > 1)
        {
            CmdSpawnBall();
            CanReset = true;
        }
        if (CanReset) {
            foreach (NetworkConnection Connection in NetworkServer.connections)
            {
                if (Connection == null)
                {
                    NetworkManager.singleton.client.Disconnect();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Destroy(BallActive);
                }
            }
            score[0] = 0;
            score[1] = 0;
            CanReset = false;
        }
    }
    GameObject BallObj;

    [Command]
    void CmdSpawnBall()
    {
        GameObject obj = Instantiate(BallPrefab);
        BallActive = obj;
        NetworkServer.Spawn(obj);
        //BallActive.GetComponent<PongBall>().GM = this;
    }

    [Command]
    public void CmdDeleteBall()
    {
        RpcDeleteBall();
    }

    [ClientRpc]
    public void RpcDeleteBall()
    {
        Destroy(BallActive);
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
    }

    [ClientRpc]
    public void RpcGOAL(int ID)
    {
        score[ID] += 1;
        ScoreText[ID].text = "Score: " + score[ID];
        Audio.Play();
    }
}
