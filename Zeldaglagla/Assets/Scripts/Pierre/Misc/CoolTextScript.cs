using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTextScript : MonoBehaviour
{
    [SerializeField]
    string defaultText; 
    int currentChar = 0;
    string currentText;
    bool reading;
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
        currentChar = 0;
        if (reading)
        {
            StopAllCoroutines();
            text.text = currentText;
            reading = false;
        }
        else
        {
            currentText = textToRead;
            text.text = "";
            StartCoroutine(ReadCoroutine(textToRead.ToCharArray()));
        }
    }

    public void Read(char[] textToRead)
    {
        StopAllCoroutines();
        currentChar = 0;
        if (reading)
        {
            text.text = currentText;
            reading = false;
        }
        else
        {
            currentText = textToRead.ToString();
            text.text = "";
            StartCoroutine(ReadCoroutine(textToRead));
        }
    }

    IEnumerator ReadCoroutine(char[] textToRead)
    {
        if (currentChar < textToRead.Length)
        {
            reading = true;
            //SOUND TEXT BLEEP
            yield return new WaitForSeconds(0.075f);
            text.text += textToRead[currentChar];
            currentChar++;
            StartCoroutine(ReadCoroutine(textToRead));
        } else
        {
            reading = false;
        }

    }
}
