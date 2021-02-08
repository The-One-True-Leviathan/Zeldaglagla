using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTextScript : MonoBehaviour
{
    [SerializeField]
    string defaultText; 
    int currentChar = 0;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Read()
    {
        Read(defaultText);
    }

    public void Read(string textToRead)
    {
        StopAllCoroutines();
        text.text = "";
        currentChar = 0;
        char[] textToReaCharred = textToRead.ToCharArray();
        StartCoroutine(ReadCoroutine(textToReaCharred));
    }

    IEnumerator ReadCoroutine(char[] textToRead)
    {
        if (currentChar < textToRead.Length)
        { 
            //SOUND TEXT BLEEP
            yield return new WaitForSeconds(0.1f);
            text.text += textToRead[currentChar];
            currentChar++;
            StartCoroutine(ReadCoroutine(textToRead));
        }

    }
}
