using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement : MonoBehaviour {

    protected float speed;
    protected float sensitivity;
    CharacterController player = null;

    float moveFB; //Front and back
    float moveLR; //Left and right

    float rotX;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
        speed = GetComponent<Stats>().speed;
        sensitivity = GetComponent<Stats>().pertinence;
    }

    // Update is called once per frame
    void Update()
    {
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        transform.Rotate(0, rotX, 0);

        movement = transform.rotation * movement * Time.deltaTime;
        player.Move(movement);

    }
    
}
