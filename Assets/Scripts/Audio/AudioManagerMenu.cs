using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    //[Header("FMOD One Shot Events")]
    [Header("FMOD One Shot Events")]

    [SerializeField]
    [FMODUnity.EventRef]
    private string ambienceLoop;

    [SerializeField]
    [FMODUnity.EventRef]
    private string newCustomerEnter;

    [SerializeField]
    [FMODUnity.EventRef]
    private string ui_Click;
    

    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ambienceLoop);
    }

    //Call when new customer enters...
    public void NewCustomerEnter()
    {
        FMODUnity.RuntimeManager.PlayOneShot(newCustomerEnter);
    }

    //Call when clicking stuff in the UI
    public void UIClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ui_Click);
    }



}
