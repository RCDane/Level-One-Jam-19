﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalHair : MonoBehaviour
{
    // Just to clarify, this solution is sub-optimal. What this script does is declaring an integer "score" 100 and operate score-- each time
    // a collision is detected. Because this run in some miliseconds the function which returns the Score value (FinalScore())
    // is delayed 2 seconds with the Invoke() function. The ideal solution to this problem would be to only call the FinalScore()
    // when no more objects are colliding with the body (Still working on a solution for this, but by now this will do)

    int score = 100; // An integer data type for having the score value
    public Text EndText;
    public GameObject EndMessage;
    private int extraScore;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    private void OnTriggerEnter(Collider other) //Collision detection with tagged GameObjects as "PlayerHair"
        {
            if (other.tag == "PlayerHair")
            {
            // The code substracts one from the score, logs the object collided with and destroys it
                score--;
                Debug.Log("Collision detected at" + other.transform);
                Destroy(other.gameObject);
            }
        }

    private void Start()
    {
        EndMessage.gameObject.SetActive(true);
        score = score + extraScore;
        Invoke("FinalScore",2); // Calls the FinalScore() function two seconds after the script runtime
        }
    public void FinalScore()
    {
        if (100 >= score && score >= 60)
        {
            Debug.Log("Score =" + score); //Creates a log of the score
            EndText.text = "(Not that)Badbershop!";
            Star1.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
            Star2.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
            Star3.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
        }
        else if (60 > score && score >= 30)
        { 
            Debug.Log("Score =" + score);
            EndText.text = "At least you are trying" ;
            Star1.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
            Star2.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
            Star3.GetComponent<Image>().color = new Color32(255, 255, 225, 30);
        }

        else
        {
            Debug.Log("Score =" + score);
            EndText.text = "You suck :)";
            Star1.GetComponent<Image>().color = new Color32(255, 255, 225, 200);
            Star2.GetComponent<Image>().color = new Color32(255, 255, 225, 30);
            Star3.GetComponent<Image>().color = new Color32(255, 255, 225, 30);
        }
    }
    public void IncreaseScore(int increase)
    {
        extraScore = extraScore + increase;
    }
}

