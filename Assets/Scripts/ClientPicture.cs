using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPicture : MonoBehaviour
{
    public float magnification = 2;
    public GameObject CloseToCamera;
    public GameObject OP;
    private void OnMouseUp()
    {
        transform.position = OP.transform.position;
    }
    void OnMouseDown()
    {
        transform.position = CloseToCamera.transform.position;
    }
}
