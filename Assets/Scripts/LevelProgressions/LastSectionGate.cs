using System;
using System.Collections.Generic;
using UnityEngine;

public class LastSectionGate : InteractableTriggerZone
{
    [SerializeField] private HauntedHouseProgress _progress;
    [SerializeField] private List<GameObject> _toActivate;
    [SerializeField] private List<GameObject> _toDeactivate;
    [SerializeField] private string _doorOpenSFX;

    private void Awake()
    {
        _zoneChannel.GetMessage = () => _progress.AreAllBooksRead
            ? ""
            : $"There are {_progress.BooksRemaining} books left to read.";
    }
    
    private void OnEnable()
    {
        _progress.OnAllBooksRead += OpenDoor;
    }

    private void OnDisable()
    {
        _progress.OnAllBooksRead -= OpenDoor;
    }

    private void OpenDoor()
    {
        foreach (GameObject go in _toActivate)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in _toDeactivate)
        {
            go.SetActive(false);
        }
        
        AudioManager.Instance.PlaySound(_doorOpenSFX);
    }

    protected override void OnInteractPressed(string str)
    {
        //Nothing!
    }
}




















