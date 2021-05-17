using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayController : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    SpriteRenderer heart1Sprite;
    SpriteRenderer heart2Sprite;
    SpriteRenderer heart3Sprite;

    // Start is called before the first frame update
    void Start()
    {
        heart1Sprite = heart1.GetComponent<SpriteRenderer>();
        heart2Sprite = heart2.GetComponent<SpriteRenderer>();
        heart3Sprite = heart3.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int health){
        setHeart(heart1Sprite, health);
        setHeart(heart2Sprite, health - 2);
        setHeart(heart3Sprite, health - 4);
    }

    void setHeart(SpriteRenderer heart, int val){
        if(val <= 0){
            heart.sprite = emptyHeart;
        }
        else if(val == 1){
            heart.sprite = halfHeart;
        }
        else{
            heart.sprite = fullHeart;
        }
    }
}
