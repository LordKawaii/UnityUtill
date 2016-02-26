using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayText : MonoBehaviour {
    Text charaText;
    bool displayingTxt = false;
    char[] charList;
    bool skipToEnd = false;
    public float defaultSpeed = .1f;

    void Start ()
    {
        charaText = gameObject.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && displayingTxt == true)
        {
            skipToEnd = true;
        }
    }

    public bool DisplayTxt(string txt, float txtSpeed)
    {
        if (displayingTxt)
            return false;

        displayingTxt = true;
        charaText.text = "";
        StartCoroutine(DisplayNextChar(txt, txtSpeed));
        return true;
    }

    public bool DisplayTxt(string txt)
    {
        if (displayingTxt)
            return false;
        return DisplayTxt(txt, defaultSpeed);
    }

    IEnumerator DisplayNextChar(string text, float textSpeed)
    {
        for (int i = 0; i < text.Length; i++)
        { 
            yield return new WaitForSeconds(textSpeed);
            charaText.text = charaText.text + text[i];

            if (skipToEnd)
            {
                skipToEnd = false;
                charaText.text = text;
                break;
            }
        }
        displayingTxt = false;
        yield return null;
    }

}
