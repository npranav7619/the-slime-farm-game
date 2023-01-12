using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeSpawnerFR : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private float swarmerInterval = 2f;
    ScoreManager sm;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-1.5f,-0.1f),Random.Range(-1.2f,-2.09f),0),Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}