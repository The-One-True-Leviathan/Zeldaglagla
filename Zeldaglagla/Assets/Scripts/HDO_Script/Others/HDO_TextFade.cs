using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDO_TextFade : MonoBehaviour
{

    Text text;

    string textLastFrame = "";

    [SerializeField]
    float timeToWaitBeforeFade;
    [SerializeField]
    float elapsedTime = 1;

    bool fadeStarted;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(text.text != textLastFrame && text.text != "")
        {
            StopAllCoroutines();
            textLastFrame = text.text;
            elapsedTime = timeToWaitBeforeFade;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            fadeStarted = false;
            Debug.Log("oui");
        }
        else if(text.text != "")
        {
            elapsedTime -= Time.deltaTime;
        }

        if(elapsedTime <= 0 && !fadeStarted)
        {
            fadeStarted = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a / 1.2f);
        Debug.Log("fading" + text.color.a);

        yield return new WaitForSeconds(0.1f);

        if(text.color.a > 0)
        {
            StartCoroutine(Fade());
        }
        else
        {
            text.text = "";
            fadeStarted = false;
        }
    }
}
