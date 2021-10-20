using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public string enemyPrefabPath;
    public float maxEnemies;
    public float spawnRadius;
    public float spawnCheckTime;

    private float lastSpawnCheckTime;
    private List<GameObject> curEnemies = new List<GameObject>();
    
    public int enemiesDefeated = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        for(int x = 0; x < curEnemies.Count; ++x)
        {
            if (!curEnemies[x])
            {
                curEnemies.RemoveAt(x);
            }
        }
        if(curEnemies.Count >= maxEnemies)
        {
            return;
        }
        Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;

        if(enemiesDefeated < 8)
        {
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInCircle, Quaternion.identity);
            curEnemies.Add(enemy);
        }
        else
        {
            enemyPrefabPath = "Enemy 1";
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInCircle, Quaternion.identity);
            curEnemies.Add(enemy);
        }
        
       



        
    }
}
