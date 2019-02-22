using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelHair : MonoBehaviour
{
    private Vector4[,,] voxels;

    [SerializeField]
    private Vector3Int size;

    private GameObject[] voxelObjects;
    [SerializeField]
    private GameObject testObject;
    // Start is called before the first frame update
    void Start()
    {
        voxels = new Vector4[size.x,size.y,size.z];
        voxelObjects = new GameObject[size.x * size.y * size.z];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j< size.y; j++)
            {
                for (int k = 0; k < size.z; k++)
                {
                    voxelObjects[i] = Instantiate(testObject, new Vector3(i,j,k),Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
