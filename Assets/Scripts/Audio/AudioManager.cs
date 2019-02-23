using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[Header("FMOD One Shot Events")]
    [Header("FMOD One Shot Events")]

    [SerializeField]
    [FMODUnity.EventRef]
    private string ambienceLoop;

    [Header("FMOD Events with parameters")]

    [SerializeField]
    [FMODUnity.EventRef]
    private string musicEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string scissorCutEvent;

    [Header("FMOD Parameters")]

    [SerializeField]
    [FMODUnity.EventRef]
    private string haircutProgress;

    //------ Her laves mine FMOD instances ------
    private FMOD.Studio.EventInstance scissorCutInstance;


    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ambienceLoop);
    }

    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    ScissorCut();
        //    print("space key was pressed");
        //}

    }

    //------ Methods for FMOD instances ------


    //Husk at sende gameObject med som argument
    public void ScissorCut(GameObject objectToAttachTo)
    {
        scissorCutInstance = FMODUnity.RuntimeManager.CreateInstance(scissorCutEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(scissorCutInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        scissorCutInstance.start();
        scissorCutInstance.release();
    }
}
