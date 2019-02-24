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
    public int currentTool;
    float timer = 0;
    //Nikolaj reference to audio
    public AudioManager audioManagerObject;

    private void Start()
    {
        if(currentTool == 1)
        {
            audioManagerObject.TrimmerStart();
            trimmerRunning = true;
        }
        cam = Camera.main;
        hair = FindObjectOfType<VoxelHair>() as VoxelHair;
    }
    bool trimmerRunning = false;
    private void Update()
    {
        if(Input.GetMouseButton(0) && timer + tools[currentTool].speed < Time.timeSinceLevelLoad)
        {
            TryRemove();
        }
        if (Input.GetMouseButtonDown(0) && currentTool == 1 && !trimmerRunning)
            audioManagerObject.TrimmerStart();
        else if (Input.GetMouseButtonDown(0) && currentTool != 1 && trimmerRunning)
            audioManagerObject.TrimmerStop();


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
            Playsound();
            timer = Time.timeSinceLevelLoad;
        }
    }
    void Playsound()
    {
        switch (currentTool)
        {
            case 0:
                audioManagerObject.ScissorCut();
                break;
            case 1:
                audioManagerObject.TrimmerCutHair();
                print("trimmer cut hair");
                break;
            case 2:
                audioManagerObject.MagicLotionUse();
                break;
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
