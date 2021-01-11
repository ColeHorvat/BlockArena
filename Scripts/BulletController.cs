using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject enemyParticles;
    public AudioSource enemyDie;

    GameController gameController;
    SpriteRenderer spriteRenderer;
    BoxCollider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        bulletCollider = gameObject.GetComponent<BoxCollider2D>();
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
            //particle effect here
        }
         
        else if (other.gameObject.CompareTag("Enemy")) 
        {
            spriteRenderer.enabled = false;
            bulletCollider.enabled = false;

            Destroy(other.gameObject);
            enemyDie.Play();
            Instantiate(enemyParticles, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
            Destroy(gameObject, enemyDie.clip.length);

            gameController.playerScore++;
        }
        
    }
}
