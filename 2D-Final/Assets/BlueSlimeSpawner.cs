using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private float swarmerInterval = 4.5f;
    ScoreManager sm;
    void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-2.3f,1.7f),Random.Range(2.6f,4.6f),0),Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
