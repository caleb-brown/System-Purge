using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

    public TextAsset textfile;  //A block of text that will be "put" into the box.
    public string[] textLines;  //An array of lines that will be put on screen line by line.


	// Use this for initialization
	void Start () {

        if(textfile != null)
        {
            textLines = (textfile.text.Split('\n'));
        }
	
	}
	

}
