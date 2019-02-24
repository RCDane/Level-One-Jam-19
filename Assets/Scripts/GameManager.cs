using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float TotalTime = 120f;
    public Text TimeLeft;
    public GameObject[] TextList;
    public GameObject Options;
    public GameObject Answers;
    public GameObject Goal;
    public GameObject SpeechBubble;
    private int i = 0;

    void Start()
    {
        InvokeRepeating("NextDialogue", 5,Random.Range(15, 25));
    }
    void Update()
    {

        TotalTime -= Time.deltaTime;  //Substracts the current time from the total time
        TimeLeft.text = "Time Left:" + Mathf.Round(TotalTime); // Prints out the time left each frame
        if (TotalTime < 0) //Function activated when time is over
        {
            TimeLeft.text = "Game over"; //Code for ending the level and pass to the next one (TO BE IMPLEMENTED)
            Goal.gameObject.SetActive(true);
        }

        //Calls for the next dialogue after 5 seconds from start, and then randomly between 5 and 15 seconds
     
        
    }

    void NextDialogue()
    {
        if (i != 0) // Code for making sure that the previous text is deactivated
        {
            TextList[i - 1].SetActive(false);
            if (TextList[i - 1].tag == "Question")
            {
                Options.gameObject.SetActive(false);
                Answers.gameObject.SetActive(false);
            }
        }
        SpeechBubble.SetActive(true);
        TextList[i].SetActive(true);
        i++;
        //To be implemented
    }
}