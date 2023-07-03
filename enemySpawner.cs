using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public List<Transform> enemySpawnPoint;
    public GameObject enemyPrefab;
    public int enemiesToSpawn = 10;
    public float spawnTimer = 1.0f;
    private float currentTime = 0.0f;
    private int waveNumber = 0;
    private bool startWave = false;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
    }

    public void StartWave()
    {
        startWave = true;
        waveNumber++;
    }

    // Update is called once per frame
    void Update()
    { 
        if(startWave == true)
        {

        if(enemiesToSpawn > 0)
        {
            if(Time.time > currentTime)
            {
                int ranI = Random.Range(0, 3);
                Instantiate(enemyPrefab, enemySpawnPoint[ranI].position, Quaternion.identity);
                currentTime = Time.time + spawnTimer;
                enemiesToSpawn--;
            }
        }

        }


        if(enemiesToSpawn == 0)
        {
            startWave = false;
            waveNumber++;
            StartCoroutine(ToNextWave());
            enemiesToSpawn = waveNumber * 5 + 5;
        }
    }

    IEnumerator ToNextWave()
    {
        yield return new WaitForSeconds(5.0f);
        startWave = true;
    }
}
