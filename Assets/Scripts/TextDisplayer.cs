using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    public List<string> diolouge;
    bool playerIn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn){
            if(Input.GetButtonDown("Interact")){
                Debug.Log("test");
                TextDisplayController.DisplayText(diolouge);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            playerIn = true;
        }

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }
}
