using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine : MonoBehaviour
{
    
     
    [SerializeField]
    private float _drinkSpeed = 3f;

    [SerializeField] private bool _drunk = false;
    
    void Update()
    {
        transform.Translate(Vector3.down * _drinkSpeed * Time.deltaTime);
        transform.Rotate(0f,3f,0f, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student
        //damage student or destroy it
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().StartDrinking();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(1);
        }
    }
}

