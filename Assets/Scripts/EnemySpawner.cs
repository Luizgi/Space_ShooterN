using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private int points = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int levelBase = 20;

    private float waitToSpawn = 0;
    [SerializeField] private float chargeTime = 5f;

    [SerializeField] private int enemiesQtd = 0;

    [SerializeField] private GameObject bossAnimation;
    private bool bossCreated;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (level < 3)
        {
            SpawnEnemies();
        }
        else 
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (enemiesQtd <= 0 && waitToSpawn > 0)
        { 
            waitToSpawn -= Time.deltaTime/2;
        }

        if(!bossCreated && waitToSpawn <= 0)
        {
            GameObject animBoss = Instantiate(bossAnimation, Vector3.zero, transform.rotation);
            Destroy(animBoss, 4.5f);
            bossCreated = true;
        }
    
    }

    public void PointsToGive(int points)
    {
        this.points += points;

        if (this.points > levelBase * level)
        {
            level++;
        }
    }

    public void ReduceEnemiesQtd()
    {
        enemiesQtd--;
    }

    private void SpawnEnemies()
    {
        if (waitToSpawn > 0 && enemiesQtd <= 0)
        {
            waitToSpawn -= Time.deltaTime;
        }
        if (waitToSpawn <= 0)
        {
            int qtdDifficulty = level * 4;
            
            //int attempt = 0;

            while (enemiesQtd < qtdDifficulty)
            {
                //attempt++;

                //if (attempt > 200)
                //{
                //    break;
                //}

                GameObject enemyCreated;
                float chance = Random.Range(0, level);
               

                if (chance > 2f)
                {
                    enemyCreated = enemies[1];
                }
                else
                {
                    enemyCreated = enemies[0];
                }

                

                Vector3 spawnOffset = new Vector3(Random.Range(-8, 8), Random.Range(7, 12), transform.position.z);

                Instantiate(enemyCreated, spawnOffset, transform.rotation);
                enemiesQtd++;
                waitToSpawn = chargeTime;

            }           

            
        }

    }

    //private bool CheckPosition(Vector3 posToCheck, Vector3 size)
    //{ 
        
    //}
}
