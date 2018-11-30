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
    [Range(-40,40)]
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

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        transform.Rotate(0, rotX, 0);
        Eyes.transform.Rotate(-rotY, 0, 0);
        
        movement = transform.rotation * movement * Time.deltaTime;
        player.Move(movement);

        //player.transform.Translate(movement * Time.deltaTime); **Autre manière moins fluide**

    }
}
