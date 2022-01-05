using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public bool showingDisplay;


    [SerializeField] bool _open = false, _locked = false;
    bool _closing = false, _opening = false;
    [SerializeField] float _openAngle = 115f;
    float _changeTime = 1f, _startTime;
    [SerializeField] TextMeshPro _doorText;
    // NO REFERENCES NECESSARY
    void Update()
    {
        if (_opening) Rotate(true);
        else if (_closing) Rotate(false);
    }
    //PRIVATE
    void Close()
    {
        _closing = false;
        transform.eulerAngles = Vector3.zero;
        _open = false;
    }
    void LockedAlert()
    {
        // to do:
        // play locked sound
    }

    void Open()
    {
        _opening = false;
        transform.eulerAngles = new Vector3(0, _openAngle, 0);
        _open = true;
    }

    void Rotate(bool opening)
    {
        float timeElapsed = Time.time - _startTime,
            elapsedRatio = timeElapsed / _changeTime,
            portion = _openAngle * elapsedRatio,
            angle = opening ? portion : _openAngle - portion;
        transform.eulerAngles = new Vector3(0, angle, 0);
        if (_opening && transform.eulerAngles.y >= _openAngle) Open();
        else if (_closing && transform.eulerAngles.y > _openAngle + 1) Close();
    }

    void StartClose()
    {
        _closing = true;
        _startTime = Time.time;
        _doorText.enabled = false;
    }

    void StartOpen()
    {
        _opening = true;
        _startTime = Time.time;
        _doorText.enabled = false;
    }
    //PUBLIC
    public void DisplayText(bool key)
    {
        
        ToggleEngage(true);
        if (_locked) _doorText.text = key ? "Unlock" : "Locked";
        else _doorText.text = _open ? "Close" : "Open";
    }

    public void Interact()
    {
        if (_opening || _closing) return;
        if (_open) StartClose();
        else if (_locked) LockedAlert();
        else StartOpen();
    }

    public void ToggleEngage(bool engage)
    {
        _doorText.enabled = engage;
        showingDisplay = engage;
    }

    public bool Transitioning()
    {
        return _opening || _closing;
    }
}
