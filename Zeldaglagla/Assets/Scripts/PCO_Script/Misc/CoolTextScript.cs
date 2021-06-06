using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTextScript : MonoBehaviour
{
    /* Script pour afficher les textes lettre par lettre.
     * Mettez-le sur un objet UI contenant un cartouche de texte, pr�cisez un texte par d�faut et normalement vous devriez �tre parr�s.
     * Vous pouvez ensuite utiliser Read() avec un string ou un char[] (au choix), ou le laisser vide pour afficher le texte par d�faut.
     */


    [SerializeField]
    public string defaultText; 
    int currentChar = 0;
    string currentText;
    bool reading;
    [SerializeField]
    float waitBetweenLetters = 0.02f;
    Text text;

    [SerializeField]
    Image portrait;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        
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
            //Si vous voulez �mettre un son lorsqu'une lettre appara�t, ajoutez-le par ici !
            yield return new WaitForSeconds(waitBetweenLetters);
            text.text += textToRead[currentChar];
            currentChar++;
            StartCoroutine(ReadCoroutine(textToRead));
        } else
        {
            reading = false;

        }

    }
}
