using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //X-values are # of crabtopus to spawn in wave, Y-values are Sheel
    [SerializeField] Vector2[] waveMap;

    [SerializeField] Image waveSliderImage;
    [SerializeField] TMP_Text enemyCountText;

    [SerializeField] StoreManager storeManager;
    [SerializeField] GameObject crabtopusPrefab;
    [SerializeField] GameObject sheelPrefab;
    [SerializeField] Vector2 xBounds;
    [SerializeField] Vector2 zBounds;
    [SerializeField] float spawnHeight;
    
    [SerializeField] float spawnDelay = 1f;
    float spawnDelayTimer;

    bool fighting;
    int currentWave = 0;
    public int enemyCount;
    float totalEnemiesThisWave;
    [SerializeField] float startDelay = 20f;
    [SerializeField] float startDelayTimer = 5f;

    void Update()
    {
        if(fighting)
        {
            enemyCountText.text = enemyCount.ToString();
            waveSliderImage.fillAmount = enemyCount / totalEnemiesThisWave;
        }
        else
        {
            enemyCountText.text = "$$$";
            waveSliderImage.fillAmount = startDelayTimer / startDelay;
        }

        if(startDelayTimer <= 0f && !fighting) StartWave();
        else startDelayTimer -= Time.deltaTime;

        if(spawnDelayTimer <= 0 && fighting)
        {
            //spawn enemy
            SpawnEnemy();
            spawnDelayTimer = spawnDelay;
        }
        else
        {
            spawnDelayTimer -= Time.deltaTime;
        }

        if(enemyCount <= 0 && fighting && waveMap[currentWave - 1].y <= 0 && waveMap[currentWave - 1].x <= 0) EndWave();
    }

    void StartWave()
    {
        fighting = true;
        currentWave++;
        storeManager.RaiseStore();
        totalEnemiesThisWave = waveMap[currentWave - 1].x + waveMap[currentWave - 1].y;
    }

    void EndWave()
    {
        startDelayTimer = startDelay;
        storeManager.LowerStore();
        fighting = false;
    }

    void SpawnEnemy()
    {
        if(Random.Range(0,2) == 0 && waveMap[currentWave - 1].x > 0)
        {
            //spawn crabtopus
            enemyCount++;
            waveMap[currentWave - 1].x--;

            float xPos = Random.Range(xBounds.x, xBounds.y);
            float zPos = Random.Range(zBounds.x, zBounds.y);
            GameObject spawnedEnemy = Instantiate(crabtopusPrefab);
            spawnedEnemy.transform.position = new Vector3(xPos, spawnHeight, zPos);
            spawnedEnemy.SetActive(true);
        }
        else if(waveMap[currentWave - 1].y > 0)
        {
            //spawn sheel
            enemyCount++;
            waveMap[currentWave - 1].y--;

            float xPos = Random.Range(xBounds.x, xBounds.y);
            float zPos = Random.Range(zBounds.x, zBounds.y);
            GameObject spawnedEnemy = Instantiate(sheelPrefab);
            spawnedEnemy.transform.position = new Vector3(xPos, spawnHeight, zPos);
            spawnedEnemy.SetActive(true);
        }
    }
}
