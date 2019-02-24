using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyOnHovering : MonoBehaviour
{
    public float magnification = 2;
    private void OnMouseOver()
    {
            transform.localScale = new Vector3(magnification, magnification, magnification);
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(10, 10, 10);
    }
}
