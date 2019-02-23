using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CameraMovement : MonoBehaviour
    {
        public int rotationSpeed;
        public Transform endMarkerR = null; // create an empty gameobject and assign in inspector
        public Transform endMarkerL = null;
        public Transform endMarkerF = null;


    void Update()
        {
        if (Input.GetKeyDown(KeyCode.D))
        { 
            transform.position = Vector3.Lerp(transform.position, endMarkerR.position, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, endMarkerR.rotation, Time.time * rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        { 
            transform.position = Vector3.Lerp(transform.position, endMarkerL.position, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, endMarkerL.rotation, Time.time * rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        { 
            transform.position = Vector3.Lerp(transform.position, endMarkerF.position, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, endMarkerF.rotation, Time.time * rotationSpeed);
        }
    }
    }

