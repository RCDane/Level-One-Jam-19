using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    ToolType tool;

    VoxelHair hair;
    Camera cam;
    [SerializeField]
    LayerMask hairLayer;
    
    [SerializeField]
    Tool[] tools;
    [SerializeField]
    int currentTool;
    float timer = 0;
    private void Start()
    {
        cam = Camera.main;
        hair = FindObjectOfType<VoxelHair>() as VoxelHair;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && timer + tools[currentTool].speed < Time.timeSinceLevelLoad)
        {
            TryRemove();
        }
    }
    void TryRemove()
    {
        RaycastHit hit;
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out hit, Mathf.Infinity, hairLayer))
        {
            VoxelObj voxel = hit.transform.GetComponent<VoxelObj>();
            if(!tools[currentTool].addingOrRemoving)
                hair.RemoveHair(hit.point, tools[currentTool].size);
            else
                hair.AddHair(hit.point, tools[currentTool].size);

            timer = Time.timeSinceLevelLoad;
        }
    }
}
[System.Serializable]
public class Tool
{
    public string name;
    public float size;
    public float speed;
    public bool addingOrRemoving = false;
}

public enum ToolType
{
    Scissor
}
