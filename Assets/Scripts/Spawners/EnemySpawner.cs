using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyController> enemies;

        public float spawnRateMin;
        public float spawnRateMax;
        public float initalSpawn;

        private int spawnIndex;
        private void Start()
        {
            StartCoroutine(DelaySpawn());
        }

        //Ilk üretim için delay
        IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(initalSpawn);
            
            StartCoroutine(SpawnEnemies());
        }

        //Düşmanları belirli aralıkta üret
        IEnumerator SpawnEnemies()
        {
            spawnIndex= Random.Range(0, enemies.Count);//Random düşman seçiyoruz
            
            while (true)
            {
                //Eğer düşman sayısı istenenden fazla ise
                if (GameMaster.instance.enemyList.Count >= GameMaster.instance.enemyLimit)
                {
                    //Durmadan önce çalışır
                    yield return new WaitWhile(()=>GameMaster.instance.enemyList.Count >= GameMaster.instance.enemyLimit);//Düşman sayısı azalana kadar bekle
                    //Sonra
                }
                
                EnemyController instance = Instantiate(enemies[spawnIndex], transform.position, Quaternion.identity);//Düşman üret
                
                instance.transform.SetParent(GameMaster.instance.levelHolder);
                //Önce
                yield return new WaitForSeconds(Random.Range(spawnRateMin,spawnRateMax));//Bu kadar bekle
                //Sonra
                
                spawnIndex= Random.Range(0, enemies.Count);//Yeni düşmanı seç
            
            }
        
        }
    
    
    }
}
