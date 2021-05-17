using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneController : MonoBehaviour
{
    public GameObject inventory;
    public List<GameObject> spawnPositions;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SpawnPosition")){
            PlayerPrefs.SetInt("SpawnPosition", 0);
            PlayerPrefs.Save();
        }
        player.transform.position = spawnPositions[PlayerPrefs.GetInt("SpawnPosition")].transform.position;
        if(PlayerPrefs.GetInt("SpawnPosition") == 0){
            List<string> temp = new List<string>() {
                "WASD to move",
                "Space to attack",
                "E to interact",
                "shift to dash"
            };
            TextDisplayController.DisplayText(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory")){
            if(inventory.activeSelf){
                inventory.SetActive(false);
            }
            else{
                inventory.SetActive(true);
            }
        }
    }
}
