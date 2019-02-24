using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorCutTestScript : MonoBehaviour
{
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            audioManager.ScissorCut();
            print("c key was pressed");
        }

    }
}
