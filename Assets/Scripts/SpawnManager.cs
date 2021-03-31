using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // variable managing the spawnchances
    [Range(0f, 1f)] 
    [SerializeField] 
    private float _normalAssignmentSpawnChance = 0f;

    [Range(0f, 1f)]
    [SerializeField] 
    private float _chanceModifier = 0.1f;
    
    //The list of prefabs for enemies and powerups
    [SerializeField] 
    private List<GameObject> _assignmentPrefabs;

    [SerializeField] 
    private List<GameObject> _powerUpPrefabs;
    
    //variables defining how long to wait for the next enemy/powerup spawn
    [SerializeField] 
    private float _delay = 3f;

    [SerializeField]
    private float _powerUpSpawnRate = 7f;

    // spawning switch
    private bool _spawningOn = true;

    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerUp());
    }

    // instantiate an assignment prefab every 5 secs
    IEnumerator SpawnSystem()
    {
        // as long as game is running, after a delay (5 sec) spawn a new assignment
        while (_spawningOn)
        {
            // Instantiate the kind of assignment/enemy depending on the changing spawnchances per enemy according to SelectAssignmnentIndex
            // at random location and setParent to SpawnManager
            Instantiate(_assignmentPrefabs[SelectAssignmentIndex()], new Vector3(Random.Range(-8f, 8f), 7f, 0f), Quaternion.identity,
                this.transform);
            yield return new WaitForSeconds(_delay);
        }
        // whne spawning is off, destroy the assignments
        Destroy(this.gameObject);
    }

    public void onPlayerDeath()
    {
        //when player dies stop spawning
        _spawningOn = false;
    }

    IEnumerator SpawnPowerUp()
    {
        // the same principle of spawning powerUPs than that described for the assignment
        // spawnchances are changing for the powerups just like for assignments
        while (_spawningOn)
        {
            Instantiate(_powerUpPrefabs[SelectAssignmentIndex()], new Vector3(Random.Range(-8f, 8f), 7f, 0f), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpSpawnRate);
          
        }
    }
    // sets spawning chances
    // higher indexes are spawned more often later in the game
    // the position in the prefablist/assignmentlist are adjusted accordingly = strong powerups late in the game, hard enemies, late in the game
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


