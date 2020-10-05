using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class CustomNetworkManager : NetworkManager
{
    private float nextRefreshTime;
    [SerializeField]
    public GameObject Canvas;
    public CustomGameManager CGM;


    public void StartHost()
    {
        StartMatchMaker();
        matchMaker.CreateMatch("testname", 2, true, "", "", "", 0, 0, OnMatchCreated);
        Canvas.SetActive(false);
    }

    public void StartLanHost()
    {
        StartHost();
    }

    private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
    {
        base.StartHost(responseData);
        RefreshMatchList();
    }

    private void FixedUpdate()
    {
        if (Time.time >= nextRefreshTime)
        {
            RefreshMatchList();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //CGM.CmdResetScore();
            singleton.StopClient();
            Canvas.SetActive(true);
        }
    }

    internal void JoinMatch(MatchInfoSnapshot match)
    {
        if(matchMaker == null)
            StartMatchMaker();

        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedInfo, MatchInfo responseData)
    {
        StartClient(responseData);
    }

    private void RefreshMatchList()
    {
        nextRefreshTime = Time.time + 2f;
        if (matchMaker == null)
            StartMatchMaker();

        matchMaker.ListMatches(0, 10, "", true, 0, 0, ListMatchesComplete);
    }

    private void ListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        AvailableMatchesList.HandleNewMatchList(responseData);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
