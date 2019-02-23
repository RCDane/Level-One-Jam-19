using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoxelHair : MonoBehaviour
{
    private Voxel[,,] voxels;

    [SerializeField]
    private Vector3Int size;

    private GameObject[] voxelObjects;

    private List<GameObject> activeVoxels;
    private Stack<GameObject> inActiveVoxels;
    [SerializeField]
    private GameObject testObject;
    [SerializeField]
    private int amountOfVoxels;
    // Start is called before the first frame update
    void Start()
    {
        voxels = new Voxel[size.x,size.y,size.z];
        voxelObjects = new GameObject[size.x * size.y * size.z];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j< size.y; j++)
            {
                for (int k = 0; k < size.z; k++)
                {
                    //voxelObjects[i+j*size.y+k*size.z] = Instantiate(testObject, new Vector3(i,j,k),Quaternion.identity);
                    //voxelObjects[i + j * size.y + k * size.z].SetActive(false);
                    
                    voxels[i, j, k] = new Voxel(new Vector4(i, j, k,1f), 1);
                }
            }
        }
        CreateVoxelPool();
        Optimize();
    }

    void CreateVoxelPool()
    {
        GameObject tempObj;
        inActiveVoxels = new Stack<GameObject>(125);
        for(int i = 0; i < amountOfVoxels; i++)
        {
            tempObj = Instantiate(testObject);
            tempObj.SetActive(false);
            inActiveVoxels.Push(tempObj);
        }
    }
    Queue<Vector3Int> togoThrough;
    List<Vector3Int> toActivate;
    bool[,,] alreadyGoneThrough;
    void Optimize()
    {
        togoThrough = new Queue<Vector3Int>(300);
        togoThrough.Enqueue(new Vector3Int(0, 0, 0));
        toActivate = new List<Vector3Int>(100);
        alreadyGoneThrough = new bool[size.x, size.y, size.z];
        alreadyGoneThrough[0, 0, 0] = true;

        int count = 0;
        do
        {
            count++;
            Vector3Int next = togoThrough.Dequeue();
            if (hasEmptyNear(next))
            {
                toActivate.Add(next);
                //alreadyGoneThrough[next.x,next.y,next.z] = true;
            }
            //alreadyGoneThrough[next.x, next.y, next.z] = true;

        } while (togoThrough.Count > 0);
        
        for (int i = 0; i < toActivate.Count; i++)
        {
            GameObject vox = inActiveVoxels.Pop();
            vox.transform.position = toActivate[i];
            vox.SetActive(true);
        }
    }

    bool hasEmptyNear(Vector3Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        int z = pos.z;
        bool hasEmptyNeighbor = false;
        if (size.x > x + 1)
        {
            if (voxels[x+1, y, z].info.w != -1)
            {
                if (!alreadyGoneThrough[x+1,y,z])
                {
                    togoThrough.Enqueue(new Vector3Int(x + 1, y, z));
                    alreadyGoneThrough[x + 1, y, z] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= x - 1)
        {
            if (voxels[x-1, y, z].info.w != -1)
            {
                if (!alreadyGoneThrough[x-1, y, z])
                {
                    togoThrough.Enqueue(new Vector3Int(x - 1, y, z));
                    alreadyGoneThrough[x - 1, y, z] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (size.y > y + 1)
        {
            if (voxels[x, y+1, z].info.w != -1)
            {
                if (!alreadyGoneThrough[x, y+1, z])
                {
                    togoThrough.Enqueue(new Vector3Int(x, y + 1, z));
                    alreadyGoneThrough[x, y + 1, z] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= y - 1)
        {
            if (voxels[x, y-1, z].info.w != -1)
            {
                if (!alreadyGoneThrough[x, y-1, z])
                {
                    togoThrough.Enqueue(new Vector3Int(x, y - 1, z));
                    alreadyGoneThrough[x, y - 1, z] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (size.z > z + 1)
        {
            if (voxels[x, y, z+1].info.w != -1)
            {
                if (!alreadyGoneThrough[x, y, z + 1])
                { 
                    togoThrough.Enqueue(new Vector3Int(x, y, z + 1));
                    alreadyGoneThrough[x, y, z + 1] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= z - 1)
        {
            if (voxels[x, y, z-1].info.w != -1)
            {
                if (!alreadyGoneThrough[x, y, z - 1])
                {
                    togoThrough.Enqueue(new Vector3Int(x, y, z - 1));
                    alreadyGoneThrough[x, y, z - 1] = true;
                }
            }
            else
            {
                hasEmptyNeighbor = true;
            }
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        return hasEmptyNeighbor;
    }
}

public struct Voxel
{
    public Vector4 info;
    public int voxelIndex;
    public Voxel(Vector4 info, int voxelIndex)
    {
        this.info = info;
        this.voxelIndex = voxelIndex;
    }
}


