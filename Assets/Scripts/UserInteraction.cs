using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    [SerializeField] float rayDistance = 5f;

    [SerializeField] bool _key = false;

    Door _foundDoor;

    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(landingRay, out hit, rayDistance))
        {
            if (hit.collider.tag == "Door")
            {
                _foundDoor = hit.collider.gameObject.GetComponentInParent<Door>();
                if (!_foundDoor.showingDisplay && !_foundDoor.Transitioning()) _foundDoor.DisplayText(_key);
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                    _foundDoor.Interact();
            }
        }
        else if (_foundDoor != null) _foundDoor.ToggleEngage(false);
    }
}
