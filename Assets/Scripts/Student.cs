using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Student : MonoBehaviour
{
    // potential student states, initialised as false
    [SerializeField]
    private bool _usePowerIdea = false;

    [SerializeField] 
    private bool _useBafog = false;

    [SerializeField] 
    private bool _sleepy = false;

    [SerializeField] 
    private bool _drunk = false;

    [SerializeField] 
    private bool _caffeinated;

    // students abilities
    [SerializeField]
    private float _speed = 5f;
    
    [SerializeField]
    private float _studyRate = 0.4f;

    [SerializeField]
    private float _timeToStudy = 0f;
    
    [SerializeField]
    [Range(0, 20)]
    public int _lives = 5;
    
    //Prefabs the student accesses

    [SerializeField] 
    private GameObject _weaponPrefab;

    [SerializeField] 
    private GameObject _powerIdeaPrefab;
    
    // time limits of powerups and negative effects of being hit by enemy (bed&wine)
    [SerializeField]
    private float _powerUpTimeOut = 5f;

    [SerializeField] private float _sleepTimeOut = 3f;

    [SerializeField] private float _drunkTimeOut = 4f;

    //managers
    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField] 
    private UIManager _uiManager;

    void Start()
    {
        // initially set attributes to false
        _usePowerIdea = false;
        _sleepy = false;
        _drunk = false;
        _caffeinated = false;
      
    }


    void Update()
    { 
        // in each frame call these functions
       PlayerMovement();
       PlayerBoundaries();
       Study();
    }

    public void Damage()
    {
        //subtract one live and give info to UIManager
        _lives -= 1;
        RelayLives(1);
        
        if (_lives == 0)
        {
            // if student dead, show gameovertext and destroy all objects
            _uiManager.ShowGameOver();
            Destroy(this.gameObject);
            //use nullchecks
            if (_spawnManager != null)
            {
                _spawnManager.onPlayerDeath();
            }
            else if (_spawnManager == null)
            {
                Debug.LogError("SpawnManager not assigned you tired idiot!");
            }
            
            Destroy(this.gameObject);
        }
    }

    // function in student that calls addscore in UIManager
    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);       
    }
    // function in student that calls countlives in UIManager
    public void RelayLives(int lives)
    {
        _uiManager.CountLives(lives);
    }
    
    void PlayerMovement()
    {
        // in all different states of the player the input defines the movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (!_sleepy && !_drunk && !_caffeinated)
        {
            // initially the student moves ordinarily up and down
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime,
                1f * verticalInput * _speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_drunk && !_sleepy && !_caffeinated)
        {
            // when student drank the bottle of wine, he moves in the opposite direction of the keys
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * _speed * Time.deltaTime,
                -1f * verticalInput * _speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
          
        }
        else if (_caffeinated && !_sleepy && !_drunk)
        {
            // when student drank coffee, the caffeine makes him overexcited and he walks twice the speed
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput *2* _speed * Time.deltaTime,
                1f * verticalInput * 2*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_sleepy && !_caffeinated && !_drunk)
        {
            // when the student is asleep, he does not move anymore for 3 secs
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_drunk && _sleepy && !_caffeinated)
        {
            // player is drunk and sleepy and walks slowly and in the wrong direction
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * 0.5f*_speed * Time.deltaTime,
                -1f * verticalInput * 0.5f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);

        }
        else if (_drunk && _caffeinated && !_sleepy)
        {
            // player is drunk and sleepy and walks slowly and in the wrong direction
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * 2f*_speed * Time.deltaTime,
                -1f * verticalInput * 2f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_sleepy && _caffeinated && !_drunk)
        {
            // player is drunk and sleepy and walks slowly and in the wrong direction
            transform.GetChild(0).rotation =Quaternion.Euler(0, 180, 0);
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput * 0.3f*_speed * Time.deltaTime,
                1f * verticalInput * 0.3f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_drunk && _sleepy && _caffeinated)
        {
            // the player is sleep deprived, drunk and caffeinated.. total chaos!!
            transform.GetChild(0).Rotate(new Vector3(2,2 , 2), Space.Self);
            // since when this state "expires" the rotation stops, in every other condition the quaternion is resetted to (0.0.0)
        }
      
    }

    void PlayerBoundaries()
    {
        // set boundaries for y coordinate
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x,
                y: 0f,
                0f);
        }

        else if (transform.position.y < -5.9f)
        {
            transform.position = new Vector3(transform.position.x,
                y: -5.9f,
                z: 0f);
        }
        // set boundaries for x coordinate
        if (transform.position.x < -10.28f)
        {
            transform.position = new Vector3(x: 10.28f,
                transform.position.y,
                0f);
        }
        else if (transform.position.x > 10.28f)
        {
            transform.position = new Vector3(x: -10.28f,
                y: transform.position.y,
                z: 0f);
        }
    }

    void Study()
    {
        // if we press space bar, than we want to instantiate the weaponprefab and allow spawning 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToStudy)
        {
            _timeToStudy = Time.time + _studyRate;
            if (!_usePowerIdea && !_useBafog)
            {
                // if no powerup is given, just instantiate the pencil
                Debug.Log("space bar pressed");
                Instantiate(_weaponPrefab, transform.position + new Vector3(x: 0.5f, y: 0.7f, z: 0), Quaternion.identity);
            }
            else if (_usePowerIdea && !_useBafog)
            {
              // if the student drank the coke, he spawns powerideas.
              // since i used an assset with a weird rotation i reset the quaternion when instantiating
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: 0f, y: 0.9f, z: 0), Quaternion.Euler(-89, 0, 270));
            }
            else if (_useBafog && !_usePowerIdea)
            {
                // if the student collected bafog, he can afford more supplies and spawn two pencils at a time
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: 0.5f, y: 0.7f, z: 0), Quaternion.identity);
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: -0.5f, y: 0.7f, z: 0), Quaternion.identity);
            }
            else if (_useBafog && _usePowerIdea)
            {
                // if he collected both bafog and drank the coke, he spawns two powerideas at a time
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: 0.7f, y: 0.9f, z: 0), Quaternion.Euler(-89, 0, 270));
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: -0.7f, y: 0.9f, z: 0), Quaternion.Euler(-89, 0, 270));

                
            }
        }   
    }
// switch for the powerIdea PowerUp
    public void ActivatePowerUp()
    {
            _usePowerIdea = true;
            Debug.Log("GlugGluGLug");
            StartCoroutine(DeactivatePowerUp());
    }
    //Switch for the Bafog PowerUp

    public void GetBafog()
    {
        _useBafog = true;
        Debug.Log("You received Bafög!");
        StartCoroutine(DeactivatePowerUp());
    }
    // Switch for the Donut PowerUp

    public void EatDonut()
    {
        _lives += 1;
        RelayLives(-1);
        Debug.Log("Yummyyy!");
    }
    //Switch for the Coffee PowerUp 
    public void DrinkCoffee()
    {
        _caffeinated = true;
        Debug.Log("You feel the rush of the caffeine!");
        StartCoroutine(DeactivatePowerUp());
    }
    
    // coroutine that deactivates all powerups after _powerUpTimeOut seconds
    IEnumerator DeactivatePowerUp()
    {
        
        yield return new WaitForSeconds(_powerUpTimeOut);
        _usePowerIdea = false;
        _useBafog = false;
        _caffeinated = false;
       
    }
    // switch for the bed enemy
    public void StartSleeping()
    {
        _sleepy = true;
        Debug.Log("Goodnight!");
        StartCoroutine(WakeUp());
    }
    // deactivates sleeping
    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(_sleepTimeOut);
        _sleepy = false;
    }
    // switch for the wine enemy
    public void StartDrinking()
    {
        _drunk = true;
        Debug.Log(("That was a glass too much!"));
        StartCoroutine(SoberUp());
    }
    // deactivate wine enemy
    // here i used multiple deactivate functions, since i wanted to give each enemy a specific duration of effect
    IEnumerator SoberUp()
    {
        yield return new WaitForSeconds(_drunkTimeOut);
        _drunk = false;
    }
}
