using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [SerializeField] 
    private GameObject _weaponPrefab;

    [SerializeField] 
    private GameObject _powerIdeaPrefab;

    [SerializeField]
    private float _studyRate = 0.4f;

    [SerializeField]
    private float _timeToStudy = 0f;

    [SerializeField]
    private int _lives = 5;
    
    [SerializeField]
    private bool _usePowerIdea = false;

    [SerializeField] private bool _useBafog = false;

    [SerializeField] private bool _sleepy = false;

    [SerializeField] private bool _drunk = false;

    [SerializeField] private bool _caffeinated;

   
    //private float _colorChannel = 1f;
    //private MaterialPropertyBlock _mpb;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private float _powerUpTimeOut = 7f;

    [SerializeField] private float _sleepTimeOut = 0.1f;

    [SerializeField] private float _drunkTimeOut = 2f;

    [SerializeField] private UIManager _uiManager;

    void Start()
    {
        _usePowerIdea = false;
        _sleepy = false;
        _drunk = false;
        _caffeinated = false;
        // if (_mpb == null)
        //{
        //   _mpb = new MaterialPropertyBlock();
        // _mpb.Clear();
        //this.GetComponent<Renderer>().GetPropertyBlock(_mpb);
        //}
        //transform.position = new Vector3(0f, 0f, 0f);
    }


    void Update()
    { 
       PlayerMovement();
       PlayerBoundaries();
       Study();
    }

    public void Damage()
    {
        //remove one from our lives
        _lives -= 1;
        
        
        if (_lives == 0)
        {
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

    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);       
    }

    public void RelayLives(int lives)
    {
        _uiManager.CountLives(lives);
    }

 
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //move player up and down
        if (!_sleepy && !_drunk && !_caffeinated)
        {
         
        
        
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime,
                1f * verticalInput * _speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_drunk && !_sleepy && !_caffeinated)
        {
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * _speed * Time.deltaTime,
                -1f * verticalInput * _speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
          
        }
        else if (_caffeinated && !_sleepy && !_drunk)
        {
            
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput *2* _speed * Time.deltaTime,
                1f * verticalInput * 2*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_sleepy && !_caffeinated && !_drunk)
        {
            
        }
        else if (_drunk && _sleepy && !_caffeinated)
        {
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            // player is drunk and sleepy and walks slowly and in the wrong direction
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * 0.5f*_speed * Time.deltaTime,
                -1f * verticalInput * 0.5f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);

        }
        else if (_drunk && _caffeinated && !_sleepy)
        {
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            // player is drunk and caffeinated, thus he walks fast and in the wrong direction
            Vector3 playerTranslate = new Vector3(-1f * horizontalInput * 2f*_speed * Time.deltaTime,
                -1f * verticalInput * 2f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_sleepy && _caffeinated && !_drunk)
        {
            // the player is confused... she drank a cup of coffee but is still sleepy: she walks only a third the speed
            
            transform.GetChild(0).Rotate(new Vector3(0, horizontalInput * Time.deltaTime, 0), Space.World);
            Vector3 playerTranslate = new Vector3(1f * horizontalInput * 0.3f*_speed * Time.deltaTime,
                1f * verticalInput * 0.3f*_speed * Time.deltaTime,
                0f);
            transform.Translate(playerTranslate);
        }
        else if (_drunk && _sleepy && _caffeinated)
        {
            // the player is sleep deprived, drunk and caffeinated.. total chaos!!
            transform.GetChild(0).Rotate(new Vector3(0,0 , 2), Space.Self);
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
        // if we press space bar, than we want to instantiate the pencilprefab and allow spawning 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToStudy)
        {
            _timeToStudy = Time.time + _studyRate;
            if (!_usePowerIdea && !_useBafog)
            {

                Debug.Log("space bar pressed");
                Instantiate(_weaponPrefab, transform.position + new Vector3(x: 0.5f, y: 0.7f, z: 0), Quaternion.identity);
            }
            else if (_usePowerIdea && !_useBafog)
            {
              
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: 0f, y: 0.9f, z: 0), Quaternion.Euler(-89, 0, 270));

            }
            else if (_useBafog && !_usePowerIdea)
            {
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: 0.5f, y: 0.7f, z: 0), Quaternion.identity);
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: -0.5f, y: 0.7f, z: 0), Quaternion.identity);
            }
            else if (_useBafog && _usePowerIdea)
            {
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: 0f, y: 0.9f, z: 0), Quaternion.Euler(-89, 0, 270));
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: 0.5f, y: 0.7f, z: 0), Quaternion.identity);
                Instantiate(_weaponPrefab,transform.position + new Vector3(x: -0.5f, y: 0.7f, z: 0), Quaternion.identity);
                
            }
        }   
    }

    public void ActivatePowerUp()
    {
            _usePowerIdea = true;
            Debug.Log("GlugGluGLug");
            StartCoroutine(DeactivatePowerUp());
    }

    public void GetBafog()
    {
        _useBafog = true;
        Debug.Log("You received Bafög!");
        StartCoroutine(DeactivatePowerUp());
    }

    public void EatDonut()
    {
        _lives += 1;
        RelayLives(-1);
        Debug.Log("Yummyyy!");
    }

    public void DrinkCoffee()
    {
        _caffeinated = true;
        Debug.Log("You feel the rush of the caffeine!");
        StartCoroutine(DeactivatePowerUp());
    }
    IEnumerator DeactivatePowerUp()
    {
        
        yield return new WaitForSeconds(_powerUpTimeOut);
        _usePowerIdea = false;
        _useBafog = false;
        _caffeinated = false;
       
    }

    public void StartSleeping()
    {
        _sleepy = true;
        Debug.Log("Goodnight!");
        StartCoroutine(WakeUp());
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(_sleepTimeOut);
        _sleepy = false;
    }

    public void StartDrinking()
    {
        _drunk = true;
        Debug.Log(("That was a glass too much!"));
        StartCoroutine(SoberUp());
    }

    IEnumerator SoberUp()
    {
        yield return new WaitForSeconds(_drunkTimeOut);
        _drunk = false;
    }
}
