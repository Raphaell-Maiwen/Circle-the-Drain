using System.Collections;
using UnityEngine;

public class DumpsterTriggerZone : InteractableTriggerZone
{
    [SerializeField] private Transform _dumpster;
    [SerializeField] private Transform _dumpsterOpenAnchor;
    [SerializeField] private Transform _meme;
    [SerializeField] private Transform[] _memeWaypointsArray;
    [SerializeField] private float _memeSpeed;

    protected override void OnInteractPressed(string str)
    {
        _interactMessenger.OnInteractPressed?.Invoke(null);
        _dumpster.position = _dumpsterOpenAnchor.position;
        _dumpster.rotation = _dumpsterOpenAnchor.rotation;

        StartCoroutine(DumpMeme());

        GetComponent<BoxCollider>().enabled = false;
        OnPlayerExit();
    }

    IEnumerator DumpMeme()
    {
        foreach (var wp in _memeWaypointsArray)
        {
            var direction = wp.position - _meme.position;

            while (direction.sqrMagnitude > 0.2f)
            {
                direction = wp.position - _meme.position;

                _meme.position += direction.normalized * Time.deltaTime * _memeSpeed;
                yield return null;
            }
        }

        //What to add to have it right on last frame?
    }
}
