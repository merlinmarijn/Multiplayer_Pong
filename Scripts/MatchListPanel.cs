using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking.Match;
using TMPro;

public class MatchListPanel : MonoBehaviour
{
    [SerializeField]
    private JoinMatch joinMatchButtonPrefab;
    [SerializeField]
    private TMP_InputField ServerName;
    private void Awake()
    {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        clearButtons();
        createNewJoinMatchButtons(matches);
    }

    private void clearButtons()
    {
        var buttons = GetComponentsInChildren<JoinMatch>();
        foreach(var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }

    private void createNewJoinMatchButtons(List<MatchInfoSnapshot> matches)
    {
        foreach(var match in matches)
        {
            var button = Instantiate(joinMatchButtonPrefab);
            button.Initialize(match, transform, ServerName.text);
        }
    }

}
