using UnityEngine;
using System.Collections;

public class DestructorMouse : MonoBehaviour
{

    void OnMouseOver() // Triggers the following code when the mouse hovers over the object with this script attached to it
    {
        if (Input.GetMouseButton(0)) // Action started if the button pressed while hovering is the mouse left button
        {
            Destroy(this.gameObject); // Destroy the game object that has this script attached to it
        }

        if (Input.GetMouseButton(1))// Action started if the button pressed while hovering is the mouse right button
        {
            Invoke("MultiplyHair",0f);  // Calls the method "MultiplyHair" after 'delay' seconds, and then repeat every y seconds
        }
    }
    void MultiplyHair() // Creates 6 clones of this game objects separated one unit each from one of the faces
    {
        Instantiate(this.gameObject, this.transform.position + new Vector3(1, 0, 0), Quaternion.identity,this.gameObject.transform.parent);
        Instantiate(this.gameObject, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity, this.gameObject.transform.parent);
        Instantiate(this.gameObject, this.transform.position + new Vector3(0, 0, 1), Quaternion.identity, this.gameObject.transform.parent);
        Instantiate(this.gameObject, this.transform.position + new Vector3(-1, 0, 0), Quaternion.identity, this.gameObject.transform.parent);
        Instantiate(this.gameObject, this.transform.position + new Vector3(0, -1, 0), Quaternion.identity, this.gameObject.transform.parent);
        Instantiate(this.gameObject, this.transform.position + new Vector3(0, 0, -1), Quaternion.identity, this.gameObject.transform.parent);
    }
}