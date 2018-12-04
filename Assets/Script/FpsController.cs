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
        Debug.Log(Eyes.transform.localRotation.x);
        if (Eyes.transform.localRotation.x < 0.25 && Eyes.transform.localRotation.x > -0.25) {
         
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
        }
        else {
            if(Eyes.transform.localRotation.x >= 0.25) {
                Eyes.transform.localRotation = new Quaternion(0.25f, Eyes.transform.localRotation.y, Eyes.transform.localRotation.z,1);
            }
            else {
                Eyes.transform.localRotation = new Quaternion(-0.25f, Eyes.transform.localRotation.y, Eyes.transform.localRotation.z, 1);
            }
        }
        /*
         * Methode avec quaternion ou euler mais ne fonctionne pas
        Vector3 CurrentRot = Eyes.transform.localEulerAngles;
        Debug.Log(rotY);
        var xRotation = ClampAngle(rotY, CurrentRot.x, -45, 45);
        //CurrentRot.x = xRotation;
        Eyes.transform.localEulerAngles = new Vector3(xRotation,
            Eyes.transform.localEulerAngles.y,
            Eyes.transform.localEulerAngles.z);
        */

        //Deplacement
        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = transform.rotation * movement * Time.deltaTime;
        player.Move(movement);
        //player.transform.Translate(movement * Time.deltaTime); **Autre manière moins fluide**

    }

    public static float ClampAngle(float posnega, float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        if (posnega >= 0) return -Mathf.Clamp(angle, min, max);
        else return Mathf.Clamp(angle, min, max);

    }

}
