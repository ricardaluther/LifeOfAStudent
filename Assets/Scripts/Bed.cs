using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{

     
    [SerializeField]
    private float _deadlineSpeed = 5f;

    [SerializeField] private bool _sleepy = false;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _deadlineSpeed * Time.deltaTime);
        transform.Rotate(0f,3f,0f, Space.World);
    }
    
      private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student
        //damage student or destroy it
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().StartSleeping();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
