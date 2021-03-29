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

    [SerializeField] private GameObject _bafogPrefab;
    

    [Range(0f, 1f)] 
    [SerializeField] 
    private float _normalAssignmentSpawnChance = 0f;

    [Range(0f, 1f)]
    [SerializeField] 
    private float _chanceModifier = 0.1f;
    [SerializeField] private List<GameObject> _assignmentPrefabs;

    [SerializeField] private List<GameObject> _powerUpPrefabs;
    

    [SerializeField] private float _delay = 2f;

    [SerializeField] private float _powerUpSpawnRate = 5f;

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
            Instantiate(_assignmentPrefabs[SelectAssignmentIndex()], new Vector3(Random.Range(-8f, 8f), 7f, 0f), Quaternion.identity,
                this.transform);
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
            Instantiate(_powerUpPrefabs[SelectAssignmentIndex()], new Vector3(Random.Range(-8f, 8f), 7f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpSpawnRate);

        }
    }

    private int SelectAssignmentIndex()
    {
        int assignmentIndex = 0;
        if (_normalAssignmentSpawnChance < Random.Range(0f, 1f))
        {
            _normalAssignmentSpawnChance += _chanceModifier;
            assignmentIndex =0;
        }
        else
        {
            assignmentIndex = Random.Range(1, _assignmentPrefabs.Count);
        }

        return assignmentIndex;
    }
    
}


