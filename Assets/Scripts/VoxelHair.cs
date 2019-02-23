using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoxelHair : MonoBehaviour
{
    [SerializeField]
    private Voxel[,,] voxels;

    [SerializeField]
    private Vector3Int size;

    private GameObject[] voxelObjects;

    private List<VoxelObj> activeVoxels;
    private Stack<VoxelObj> inActiveVoxels;
    [SerializeField]
    private GameObject testObject;
    [SerializeField]
    private int amountOfVoxels;
    // Start is called before the first frame update
    void Start()
    {
        PrepareVoxelHair();
    }

    void PrepareVoxelHair()
    {
        voxels = new Voxel[size.x, size.y, size.z];
        activeVoxels = new List<VoxelObj>(1000);
        inActiveVoxels = new Stack<VoxelObj>(20000);
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                for (int k = 0; k < size.z; k++)
                {
                    if(i==0 || j == 0 || k == 0 || i+1 == size.x || j + 1 == size.y|| k + 1 == size.y)
                        voxels[i, j, k] = new Voxel(new Vector4(i, j, k, 1f), i + size.y * j + size.y * size.z * k, new Vector3Int(i, j, k),true);
                    else
                        voxels[i, j, k] = new Voxel(new Vector4(i, j, k, 1f), i + size.y * j + size.y*size.z * k, new Vector3Int(i, j, k),true);

                }
            }
        }
        CreateVoxelPool();
        Optimize();
    }
    void CreateVoxelPool()
    {
        GameObject tempObj;
        inActiveVoxels = new Stack<VoxelObj>(10000);
        for(int i = 0; i < amountOfVoxels; i++)
        {
            tempObj = Instantiate(testObject);
            tempObj.SetActive(false);
            inActiveVoxels.Push(tempObj.GetComponent<VoxelObj>());
        }
    }
    Queue<Vector3Int> togoThrough;
    List<Vector3Int> position;
    bool[,,] alreadyGoneThrough;
    void Optimize()
    {
        //togoThrough = new Queue<Vector3Int>(300);
        //togoThrough.Enqueue(new Vector3Int(0, 0, 0));
        //position = new List<Vector3Int>(100);
        //alreadyGoneThrough = new bool[size.x, size.y, size.z];
        //alreadyGoneThrough[0, 0, 0] = true;

        //int count = 0;
        //do
        //{
        //    count++;
        //    Vector3Int next = togoThrough.Dequeue();
        //    if (hasEmptyNear(next))
        //    {
        //        position.Add(next);
        //    }

        //} while (togoThrough.Count > 0);
        position = new List<Vector3Int>(size.x*size.y*size.z);
        //List<Vector3Int> nonPosition = new List<Vector3Int>();
        alreadyGoneThrough = new bool[size.x, size.y, size.z];
        alreadyGoneThrough[0, 0, 0] = true;
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                for (int k = 0; k < size.x; k++)
                {
                    if(hasEmptyNear(new Vector3Int(i,j,k)) && voxels[i,j,k].hasHair)
                    {
                        position.Add(new Vector3Int(i,j,k));
                    }
                }


        for (int i = 0; i < position.Count; i++)
        {
            ActivateVoxel(position[i]);
        }
        for(int i = 0; i<activeVoxels.Count; i++)
        {
            int x = activeVoxels[i].position.x;
            int y = activeVoxels[i].position.y;
            int z = activeVoxels[i].position.z;

            if (!voxels[x,y,z].isActive)
            {
                DeactivateVoxel(activeVoxels[i]);
            }
        }
        
    }

    public void RemoveHair(Vector3 position,float distance)
    {
        List<Voxel> VoxelsToGoThrough = new List<Voxel>(300);
        int counter = 0;
        Vector3 pos1 = position;
        //for (int i = 0; i < activeVoxels.Count; i++)
        //{
        //    Vector3 pos2 = activeVoxels[i].transform.position;
        //    if((pos2 - pos1).magnitude < distance)
        //    {
        //        VoxelsToGoThrough.Add(activeVoxels[i]);
        //    }
        //}
        for (int i = 0; i < voxels.GetLength(0); i++)
        {
            for (int j = 0; j < voxels.GetLength(1); j++)
            {
                for (int k = 0; k < voxels.GetLength(2); k++)
                {
                    if (voxels[i, j, k].hasHair)
                    {
                        Vector3 voxelPos = voxels[i, j, k].pos;
                        if (distance * distance > Vector3.SqrMagnitude(pos1 - voxelPos))
                        {
                            VoxelsToGoThrough.Add(voxels[i, j, k]);
                            //VoxelsToGoThrough.Add(GetActiveVoxel(voxels[i, j, k].voxelIndex).GetComponent<VoxelObj>());
                        }
                    }
                }
            }
        }
        print(VoxelsToGoThrough.Count);
        List<Voxel> neighBors;
        for (int i = 0; i < VoxelsToGoThrough.Count; i++)
        {

            //neighBors = GetNeighbors(VoxelsToGoThrough[i].pos);
            //for (int j = 0; j < neighBors.Count; j++)
            //{
            //if(!neighBors.Contains(voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z]))
            //{
            //RemoveVoxel();
            DeactivateVoxel(GetActiveVoxel(VoxelsToGoThrough[i].voxelIndex));
            voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z].isActive = false;
            voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z].hasHair = false;
            //DeactivateVoxel()
            //}
            //}
        }
        Optimize();
    }
    void ActivateVoxel(Vector3Int position)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        if (voxels[x, y, z].isActive)
            return;
        voxels[x, y, z].isActive = true;
        VoxelObj vox = inActiveVoxels.Pop();
        vox.index = voxels[x, y, z].voxelIndex;
        vox.transform.position = position;
        vox.GetComponent<VoxelObj>().position = position;
        voxels[x, y, z].info.x = position.x;
        voxels[x, y, z].info.y = position.y;
        voxels[x, y, z].info.z = position.z;
        voxels[x, y, z].hasHair = true;
        //voxels[x, y, z].info.w = 1;
        vox.position = position;
        activeVoxels.Add(vox);
        vox.Activate();
    }

    void RemoveVoxel(Vector3Int position)
    {
        int x = position.x;
        int y = position.y;
        int z = position.z;
        if (voxels[x, y, z].isActive)
        {
            GameObject obj = GetActiveVoxel(voxels[x, y, z].voxelIndex);
            DeactivateVoxel(obj);
            voxels[x, y, z].isActive = false;
        }
        voxels[x, y, z].isActive = false;
        voxels[x, y, z].hasHair = false;
    }

    void DeactivateVoxel(GameObject obj)
    {
        if(obj != null)
        {
            obj.SetActive(false);
            inActiveVoxels.Push(obj.GetComponent<VoxelObj>());
        }

    }
    void DeactivateVoxel(VoxelObj obj)
    {
        activeVoxels.Remove(obj);
        inActiveVoxels.Push(obj);
        obj.Deactivate();
    }
    GameObject GetActiveVoxel(int id)
    {
        for (int i = 0; i<activeVoxels.Count; i++)
        {
            if (activeVoxels[i].index == id)
            {
                GameObject obj = activeVoxels[i].gameObject;
                //activeVoxels.RemoveAt(i);
                return obj;
            }
        }
        return null;
    }
    List<Voxel> neighBors = new List<Voxel>(6);
    List<Voxel> GetNeighbors(Vector3Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        int z = pos.z;
        neighBors.Clear();
        if(size.x > x + 1)
        {
            neighBors.Add(voxels[x + 1, y, z]);
        }
        if (0 <= x - 1)
        {
            neighBors.Add(voxels[x - 1, y, z]);
        }
        if (size.y > y + 1)
        {
            neighBors.Add(voxels[x, y + 1, z]);
        }
        if (0 <= y - 1)
        {
            neighBors.Add(voxels[x, y-1, z]);
        }
        if (size.z > z + 1)
        {
            neighBors.Add(voxels[x, y, z+1]);
        }
        if (0 <= z - 1)
        {
            neighBors.Add(voxels[x, y, z-1]);
        }
        return neighBors;
    }
    public void AddHair(Vector3 point, float size)
    {
        List<Voxel> VoxelsToGoThrough = new List<Voxel>(300);
        int counter = 0;
        Vector3 pos1 = point;
        //for (int i = 0; i < activeVoxels.Count; i++)
        //{
        //    Vector3 pos2 = activeVoxels[i].transform.position;
        //    if((pos2 - pos1).magnitude < distance)
        //    {
        //        VoxelsToGoThrough.Add(activeVoxels[i]);
        //    }
        //}
        for (int i = 0; i < voxels.GetLength(0); i++)
        {
            for (int j = 0; j < voxels.GetLength(1); j++)
            {
                for (int k = 0; k < voxels.GetLength(2); k++)
                {
                    if (!voxels[i, j, k].hasHair)
                    {
                        Vector3 voxelPos = voxels[i, j, k].pos;
                        if (size * size > Vector3.SqrMagnitude(pos1 - voxelPos))
                        {
                            VoxelsToGoThrough.Add(voxels[i, j, k]);
                            //VoxelsToGoThrough.Add(GetActiveVoxel(voxels[i, j, k].voxelIndex).GetComponent<VoxelObj>());
                        }
                    }
                }
            }
        }
        print(VoxelsToGoThrough.Count);
        List<Voxel> neighBors;
        for (int i = 0; i < VoxelsToGoThrough.Count; i++)
        {

            //neighBors = GetNeighbors(VoxelsToGoThrough[i].pos);
            //for (int j = 0; j < neighBors.Count; j++)
            //{
            //if(!neighBors.Contains(voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z]))
            //{
            //RemoveVoxel();
            //ActivateVoxel(VoxelsToGoThrough[i].pos);
            //voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z].isActive = true;
            
            voxels[VoxelsToGoThrough[i].pos.x, VoxelsToGoThrough[i].pos.y, VoxelsToGoThrough[i].pos.z].hasHair = true;
            //DeactivateVoxel()
            //}
            //}
        }
        Optimize();
    }
    bool hasEmptyNear(Vector3Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        int z = pos.z;
        bool hasEmptyNeighbor = false;
        if (size.x > x + 1)
        {
            if (!voxels[x+1, y, z].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= x - 1)
        {
            if (!voxels[x-1, y, z].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (size.y > y + 1)
        {
            if (!voxels[x, y+1, z].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= y - 1)
        {
            if (!voxels[x, y-1, z].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (size.z > z + 1)
        {
            if (!voxels[x, y, z+1].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        if (0 <= z - 1)
        {
            if (!voxels[x, y, z-1].hasHair)
                hasEmptyNeighbor = true;
        }
        else
        {
            hasEmptyNeighbor = true;
        }
        return hasEmptyNeighbor;
    }

    private void OnGUI()
    {
        bool pressed = GUI.Button(new Rect(100, 100, 100, 100), "optimize");
        if (pressed)
            Optimize();
    }
}
[System.Serializable]
public struct Voxel
{
    public Vector4 info;
    public bool hasHair;
    public Vector3Int pos;
    public int voxelIndex;
    public bool isActive;

    public Voxel(Vector4 info, int voxelIndex, Vector3Int pos,bool hasHair)
    {
        this.hasHair = hasHair;
        isActive = false;
        this.info = info;
        this.pos = pos;
        this.voxelIndex = voxelIndex;
    }
}


