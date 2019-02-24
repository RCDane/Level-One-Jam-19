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

    [SerializeField]
    [FMODUnity.EventRef]
    private string newCustomerEnter;

    [SerializeField]
    [FMODUnity.EventRef]
    private string correctAnswer;

    [SerializeField]
    [FMODUnity.EventRef]
    private string wrongAnswer;

    [SerializeField]
    [FMODUnity.EventRef]
    private string chairRotate;

    [SerializeField]
    [FMODUnity.EventRef]
    private string ui_Click;

    [SerializeField]
    [FMODUnity.EventRef]
    private string frenchCorrectAnswer;

    [SerializeField]
    [FMODUnity.EventRef]
    private string frenchWrongAnswer;

    [SerializeField]
    [FMODUnity.EventRef]
    private string hitmanCorrectAnswer;

    [SerializeField]
    [FMODUnity.EventRef]
    private string hitmanWrongAnswer;


    [Header("FMOD Events with parameters")]

    [SerializeField]
    [FMODUnity.EventRef]
    private string musicEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string scissorCutEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string trimmerCutEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string magicLotionEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string frenchSpeakingEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string playerReplyingEvent;

    [SerializeField]
    [FMODUnity.EventRef]
    private string hitmanSpeakingEvent;

    [Header("FMOD Parameters")]

    [SerializeField]
    private string haircutProgressParameter;

    [SerializeField]
    private string trimmerCuttingHairparameter;

    //------ Her laves mine FMOD instances ------
    private FMOD.Studio.EventInstance scissorCutInstance;
    private FMOD.Studio.EventInstance trimmerCutInstance;
    private FMOD.Studio.EventInstance magicLotionInstance;
    private FMOD.Studio.EventInstance musicInstance;
    private FMOD.Studio.EventInstance frenchSpeakingInstance;
    private FMOD.Studio.EventInstance playerReplyingInstance;
    private FMOD.Studio.EventInstance hitmanSpeakingInstance;

    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ambienceLoop);
        MusicStart();
    }

    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    ScissorCut();
        //    print("space key was pressed");
        //}

    }

    //------ Methods ------

    // Call when the scissor is cutting
    //Remember to send the position (see how in the ScissorCutTestScript
    public void ScissorCut()
    {
        scissorCutInstance = FMODUnity.RuntimeManager.CreateInstance(scissorCutEvent);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(scissorCutInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        scissorCutInstance.start();
        scissorCutInstance.release();
    }

    //Call when rotating the chair...   
    public void ChairRotate()
    {
        FMODUnity.RuntimeManager.PlayOneShot(chairRotate);
    }

    //Call when new customer enters...
    public void NewCustomerEnter()
    {
        FMODUnity.RuntimeManager.PlayOneShot(newCustomerEnter);
    }

    //Call when selectring the correct answer...
    public void CorrectAnswerUISound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(correctAnswer);
    }

    //Call when selectring the wronganswer...
    public void WrongAnswerUISound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(wrongAnswer);
    }

    //Call when clicking stuff in the UI
    public void UIClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(wrongAnswer);
    }

    //Call when the trimmer tool is selected
    public void TrimmerStart()
    {
        trimmerCutInstance = FMODUnity.RuntimeManager.CreateInstance(trimmerCutEvent);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(trimmerCutInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        trimmerCutInstance.start();
        trimmerCutInstance.release();
    }

    //call when the trimmer is cutting hair
    public void TrimmerCutHair()
    {
        trimmerCutInstance.setParameterValue(trimmerCuttingHairparameter, 1.0f);

    }

    //Call when the trimmer is NOT trimming hair
    public void TrimmerIdle()
    {
        trimmerCutInstance.setParameterValue(trimmerCuttingHairparameter, 0.0f);
    }

    //Call when the trimmer is not active anymore
    public void TrimmerStop()
    {
        trimmerCutInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //Call every time lotion is applied
    public void MagicLotionUse()
    {
        magicLotionInstance = FMODUnity.RuntimeManager.CreateInstance(trimmerCutEvent);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(magicLotionInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        magicLotionInstance.start();
        magicLotionInstance.release();
    }


    public void MusicStart()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
        musicInstance.release();
    }

    /* Call when client starts speaking
    3D sound, needs to be attached to the mouth game object*/
    public void FrenchSpeakStart(GameObject objectToAttachTo)
    {
        frenchSpeakingInstance = FMODUnity.RuntimeManager.CreateInstance(frenchSpeakingEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(frenchSpeakingInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        frenchSpeakingInstance.start();
        frenchSpeakingInstance.release();
    }

    //Call when question is over
    public void FrenchSpeakStop()
    {
        frenchSpeakingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //Call when answering correctly to a the French customer (should preferably get the gameObject from the mouth)
    public void FrenchCorrectAnswerReply()
    {
        FMODUnity.RuntimeManager.PlayOneShot(frenchCorrectAnswer);
        //FMODUnity.RuntimeManager.PlayOneShot(objectToAttachTo, frenchCorrectAnswer);
    }

    //Call when answering wrongto a the French customer (should preferably get the gameObject from the mouth)
    public void FrenchWrongAnswerReply()
    {
        FMODUnity.RuntimeManager.PlayOneShot(frenchWrongAnswer);
        //FMODUnity.RuntimeManager.PlayOneShot(objectToAttachTo, frenchWrongAnswer);

    }

    /* Call when the player is replying
    3D sound need to get the gameObject from the mouth */
    public void PlayerReplyStart(GameObject objectToAttachTo)
    {
        playerReplyingInstance = FMODUnity.RuntimeManager.CreateInstance(playerReplyingEvent);
        playerReplyingInstance.start();
        playerReplyingInstance.release();
    }

    //Call when reply is over
    public void PlayerReplyStop()
    {
        playerReplyingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    /* Call when client starts speaking
    3D soundneed to get the gameObject from the mouth */
    public void HitmanSpeakStart(GameObject objectToAttachTo)
    {
        hitmanSpeakingInstance = FMODUnity.RuntimeManager.CreateInstance(hitmanSpeakingEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(hitmanSpeakingInstance, objectToAttachTo.transform, objectToAttachTo.GetComponent<Rigidbody>());
        hitmanSpeakingInstance.start();
        hitmanSpeakingInstance.release();
    }

    //Call when question is over
    public void HitmanSpeakStop()
    {
        hitmanSpeakingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //Call when answering correctly to Hitman himself (should preferably get the gameObject from the mouth)
    public void HitmanCorrectAnswerReply()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hitmanCorrectAnswer);
        //FMODUnity.RuntimeManager.PlayOneShot(objectToAttachTo, hitmanCorrectAnswer);
    }

    //Call when answering wrong to a Hitman himself (should preferably get the gameObject from the mouth)
    public void HitmanWrongAnswerReply()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hitmanWrongAnswer);
        //FMODUnity.RuntimeManager.PlayOneShotAttached(objectToAttachTo, hitmanWrongAnswer);
    }

    //This method should be called as the haircut progresses, so the haircurProgressPercentage parameter is at 100 when the time is over.
    public void UpdateHaircutProgress(float haircurProgressPercentage)
    {
        musicInstance.setParameterValue(haircutProgressParameter, haircurProgressPercentage);
    }
}
