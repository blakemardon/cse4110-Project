using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayController : MonoBehaviour
{
    private static TextDisplayController textDisplay;
    private static List<string> displayStrings = new List<string>();

    public Text textObject;

    List<GameObject> children = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        TextDisplayController.textDisplay = this;

        for(int i = 0; i < this.transform.childCount; i++){
            children.Add(this.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack")){
            if(displayStrings.Count == 0){
                textDisplay.setChildren(false);
                Time.timeScale = 1;
            }
            else{
                textObject.text = displayStrings[0];
                displayStrings.RemoveAt(0);
            }
        }
    }

    public void setChildren(bool value){
        foreach(GameObject g in children){
            g.SetActive(value);
        }
        if(value){
            Time.timeScale = 0;
            textObject.text = displayStrings[0];
            displayStrings.RemoveAt(0);
        }
    }

    public static void DisplayText(string text){
        displayStrings.Add(text);
        textDisplay.setChildren(true);
    }

    public static void DisplayText(List<string> text)
    {
        displayStrings.Clear();
        foreach (string s in text)
        {
            displayStrings.Add(s);
        }
        textDisplay.setChildren(true);
        
    }
}
