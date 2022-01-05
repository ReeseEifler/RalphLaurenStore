using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float speed = 5f;
    void Update()
    {
        MoveUser();
    }

    void MoveUser()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) z *= 2;
        Vector3 move = transform.right * x + transform.forward * z, velocity = move * speed * Time.deltaTime;
        controller.Move(velocity);
    }
}
