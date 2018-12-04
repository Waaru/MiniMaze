using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour {

    public float speed = 2f;
    public float sensitivity = 2f;
    CharacterController player = null;
    public GameObject Eyes;

    float moveFB; //Front and back
    float moveLR; //Left and right

    float rotX;
    float rotY;

	// Use this for initialization
	void Start () {
        player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;
        
        //Mouvement de camera
        if (Mathf.Abs(rotX) > Mathf.Abs(rotY))
        {
            transform.Rotate(0, rotX, 0);
        }
        if (Mathf.Abs(rotX) < Mathf.Abs(rotY))
        {
            Eyes.transform.Rotate(-rotY, 0, 0);          
        }

        Vector3 CurrentRot = Eyes.transform.localRotation.eulerAngles;
        var xRotation = Mathf.Clamp(CurrentRot.x, -45, 45);
        if (xRotation < 0)
        {
            Debug.Log("hello");
            xRotation = 360 + xRotation; // e.g. 360 + -40 = 320 which is the same rotation
        }
        CurrentRot.x = xRotation;
        Eyes.transform.localRotation = Quaternion.Euler(CurrentRot);

        //Deplacement
        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = transform.rotation * movement * Time.deltaTime;
        player.Move(movement);

        //player.transform.Translate(movement * Time.deltaTime); **Autre manière moins fluide**

    }
    
}
