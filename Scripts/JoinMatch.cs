using UnityEngine;
using TMPro;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using System;

public class JoinMatch : MonoBehaviour
{
    private TextMeshProUGUI buttontext;
    private MatchInfoSnapshot match;
    private void Awake()
    {
        buttontext = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(MatchInfoSnapshot match, Transform panelTransform, string name)
    {
        this.match = match;
        if (!string.IsNullOrEmpty(name))
        {
            buttontext.text = name + " "+match.currentSize+"/"+match.maxSize;
        }
        else
        {
            buttontext.text = "Default Server" + " " + match.currentSize + "/" + match.maxSize;
        }
        transform.SetParent(panelTransform);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    public void ConnectMatch()
    {
        FindObjectOfType<CustomNetworkManager>().JoinMatch(match);
        GetComponentInParent<Canvas>().gameObject.active = false;
    }
}
