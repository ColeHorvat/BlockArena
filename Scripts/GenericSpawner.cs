using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{

    public float startSpawnTime = 5f;
    public float midSpawnTime = 3f;
    public float finalSpawnTime = 1.5f;
    public float spawnTime;
    public GameObject[] spawners;
    private GameObject spawner;
    public GameObject enemy;

    GameObject player;
    private int randNum;

    private int enemyAmount;

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
        gameController = gameObject.GetComponent<GameController>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;

        enemyAmount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(spawnTime <= 0 && enemyAmount < 7) {
            
            randNum = Random.Range(0, spawners.Length);
            spawner = spawners[randNum];

            float dist = Vector2.Distance(player.transform.position, spawner.transform.position);

            if(dist <= 7) return;

            Instantiate(enemy, spawner.transform.position, Quaternion.identity);
            

            if(gameController.playerScore < 5) spawnTime = startSpawnTime;
            else if(gameController.playerScore > 5 && gameController.playerScore < 15) spawnTime = midSpawnTime;
            else spawnTime = finalSpawnTime;
        }
    }
}
