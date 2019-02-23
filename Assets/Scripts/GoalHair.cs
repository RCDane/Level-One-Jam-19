using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHair : MonoBehaviour
{
    // Just to clarify, this solution is sub-optimal. What this script does is declaring an integer "score" 100 and operate score-- each time
    // a collision is detected. Because this run in some miliseconds the function which returns the Score value (FinalScore())
    // is delayed 2 seconds with the Invoke() function. The ideal solution to this problem would be to only call the FinalScore()
    // when no more objects are colliding with the body (Still working on a solution for this, but by now this will do)

    int score = 100; // An integer data type for having the score value
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
            Invoke("FinalScore",2); // Calls the FinalScore() function two seconds after the script runtime
        }
    public int FinalScore()
    {
        if (100 >= score && score >= 60)
        {
            Debug.Log("Score =" + score); //Creates a log of the score
            return 0; // Return 0, the maximum score possible for the player if the score is between 100 and 60
        }
        else if (60 > score && score >= 30)
        { 
            Debug.Log("Score =" + score);
            return 1; // Return 1, the middle score (Or two stars) if the score is between 59 and 30
        }

        else
        {
            Debug.Log("Score =" + score);
            return 2; // Return 2, the lowest score (One star) if the score is lower than 30
        }
        
    }
    }

