using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    public string sceneName;
    public int spawnPosition;
    public bool isActive;

    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<BoxCollider2D>();
        if(isActive){
            collider.isTrigger = true;
        }
        else {
            collider.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Player"){
            PlayerPrefs.SetInt("SpawnPosition", spawnPosition);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName);
        }
    }

    void SetActive(bool active){
        if (active)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }
}
