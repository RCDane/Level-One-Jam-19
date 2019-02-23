using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelObj : MonoBehaviour
{
    public int index;
    public Vector3Int position;

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
