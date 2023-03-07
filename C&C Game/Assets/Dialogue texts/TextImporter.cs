using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextImporter : MonoBehaviour
{
    public GameObject textBox;

    public TextAsset textFile;

    public int currentLine;
    public int endAtLine;

   // public PlayerController player;

    //we are creating an array of strings so each line is taken as a separate object lets gooo
    public string[] textLines;

    // Start is called before the first frame update
    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split("/n"));

                //what doin here- getting text array- what putting in array -from text
                //file- grab text within- split into separate wherever see /n - /n is return
        }
    }

}
