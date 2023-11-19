using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _winEvent;
    [SerializeField] private TextMeshProUGUI _wintext;

    private void Start()
    {
        _wintext.enabled = false;
    }

    private void OnEnable()
    {
        _winEvent.Subscribe(OnWin);
    }

    private void OnDisable()
    {
        _winEvent.Unsubscribe(OnWin);
    }

    private void OnWin()
    {
        _wintext.enabled = true;
    }
}
