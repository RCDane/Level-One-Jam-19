using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMovement : MonoBehaviour
{
    public AudioManager audioManagerObject;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 90, 0);
            audioManagerObject.ChairRotate();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, -90, 0);
            audioManagerObject.ChairRotate();
        }
    }
}
