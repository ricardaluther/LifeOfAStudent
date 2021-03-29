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
    private int _lives = 3;
    
    [SerializeField]
    private bool _usePowerIdea = false;

   
    //private float _colorChannel = 1f;
    //private MaterialPropertyBlock _mpb;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private float _powerUpTimeOut = 7f;


    void Start()
    {
        _usePowerIdea = false;
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
        //_colorChannel -= 0.5f;
        //_mpb.SetColor("_Color",new Color(r: _colorChannel,g: 0, b: _colorChannel, a:1f));
        //this.GetComponent<Renderer>().SetPropertyBlock(_mpb);
        if (_lives == 0)
        {
            
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
        }
    }
    void PlayerMovement()
    {
        //move player up and down
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        transform.GetChild(0).Rotate(new Vector3(0,horizontalInput* Time.deltaTime,0), Space.World);
        Vector3 playerTranslate = new Vector3(1f * horizontalInput * _speed * Time.deltaTime,
            1f * verticalInput * _speed * Time.deltaTime,
            0f);
        transform.Translate(playerTranslate);

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

        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x,
                y: -4.5f,
                z: 0f);
        }
        // set boundaries for x coordinate
        if (transform.position.x < -11.3495f)
        {
            transform.position = new Vector3(x: 11.04375f,
                transform.position.y,
                0f);
        }
        else if (transform.position.x > 11.44986f)
        {
            transform.position = new Vector3(x: -11.13391f,
                y: transform.position.y,
                z: 0f);
        }
    }

    void Study()
    {
        // if we press space bar, than we want to instantiate the vaccineprefab and allow spawning 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _timeToStudy)
        {
            _timeToStudy = Time.time + _studyRate;
            if (!_usePowerIdea)
            {

                Debug.Log("space bar pressed");
                Instantiate(_weaponPrefab, transform.position + new Vector3(x: 0f, y: 0.7f, z: 0), Quaternion.identity);
            }
            else if (_usePowerIdea)
            {
                Instantiate(_powerIdeaPrefab,transform.position + new Vector3(x: 0f, y: 0.7f, z: 0), Quaternion.identity); 
            }
        }   
    }

    public void ActivatePowerUp()
    {
        _usePowerIdea = true;
        StartCoroutine(DeactivatePowerUp());
    }

    IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(_powerUpTimeOut);
        _usePowerIdea = false;
    }
}
