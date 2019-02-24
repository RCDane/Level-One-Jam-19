using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyOnHovering : MonoBehaviour
{
    public float magnification = 2;
    public GameObject Tools;
    private void OnMouseOver()
    {
            transform.localScale = new Vector3(magnification, magnification, magnification); // Increase the scale of the object on hovering
    }
    private void OnMouseExit()
    {
        if (this.name == "Tool2_3D")
        {
            transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Returns the scale of the object back to (10,10,10)
        }
    }
        void OnMouseDown()
        {
            switch (this.gameObject.name) //Check the scenarios for the 3 kind of tools that can be selected
            {
                case "Tool1_3D": //Tool 1, Scissors
                    Tools.GetComponent<Tools>().currentTool = 0;
                    break;
                case "Tool2_3D": //Tool 2, Machine
                    Tools.GetComponent<Tools>().currentTool = 1;
                    break;
                case "Tool3_3D"://Tool 3, Magic lotion
                    Tools.GetComponent<Tools>().currentTool = 2;
                    break;
            }
        }
}
