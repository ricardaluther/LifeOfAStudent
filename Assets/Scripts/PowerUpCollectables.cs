using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PowerUpCollectables : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Student"))
            {
                other.GetComponent<Student>().ActivatePowerUp();
                Destroy(this.gameObject);
            }
        }
    }

    
