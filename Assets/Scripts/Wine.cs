using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine : MonoBehaviour
{
    
     
    [SerializeField]
    private float _drinkSpeed = 3f;

    void Update()
    {
        // the wine rotates down around the y-axis
        transform.Translate(Vector3.down * _drinkSpeed * Time.deltaTime);
        transform.Rotate(0f,3f,0f, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student, start drinking the wine bottle
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().StartDrinking();
            Destroy(this.gameObject);
        }
        // if we collide with a weapon, the wine bottle gets destroyed since it is fairly small, your score gets +2
        else if (other.CompareTag("Weapon"))
        {
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(2);
        }
    }
}

