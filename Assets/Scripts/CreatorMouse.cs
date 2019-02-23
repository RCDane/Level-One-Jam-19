using UnityEngine;
using System.Collections;

public class DeleteOnClick : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Instantiate(this.gameObject, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}