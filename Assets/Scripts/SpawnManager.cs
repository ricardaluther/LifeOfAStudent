using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _assignmentPrefab;

    [SerializeField]
    private GameObject _colaPrefab;

    [SerializeField]
    private float _delay = 2f;

    [SerializeField] 
    private float _powerUpSpawnRate = 5f;
    
    private bool _spawningOn = true;

    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerUp());
    }

    // instantiate an assignment prefab every 2 secs
    IEnumerator SpawnSystem()
    {
        // as long as game is running, after a delay (2 sec) spawn a new assignment
        while (_spawningOn)
        {
            // Instantiate assignment at random location and setParent to SpawnManager
            Instantiate(_assignmentPrefab, new Vector3(Random.Range(-8f,8f),7f,0f),Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_delay);    
        }
        Destroy(this.gameObject);
        
    }

    public void onPlayerDeath()
    {
        _spawningOn = false;
    }

    IEnumerator SpawnPowerUp()
    {
        while (_spawningOn)
        {
            Instantiate(_colaPrefab, new Vector3(Random.Range(-8f,8f),7f,0f),Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpSpawnRate);

        }
    }
}
