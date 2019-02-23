using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriting : MonoBehaviour
{
    public float delay = 1f;
    [TextArea]
    public string FullText = "Your text goes here";
    public GameObject options;
    private string CurrentText = " ";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i=0; i < FullText.Length; i++)
        {
            CurrentText = FullText.Substring(0, i);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1);
        options.gameObject.SetActive(true);

    }
}
