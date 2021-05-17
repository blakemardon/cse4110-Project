using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarSceneController : MonoBehaviour
{
    public List<GameObject> spawnPositions;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPositions[PlayerPrefs.GetInt("SpawnPosition")].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
