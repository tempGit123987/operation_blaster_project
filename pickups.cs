using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{

    private int weaponNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if(this.CompareTag("MG"))
        {
            weaponNum = 1;
        }
        if(this.CompareTag("Shotgun"))
        {
            weaponNum = 2;
        }
        */
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //other.SendMessage("ChangeWeapon", weaponNum);
            Destroy(gameObject);
        }
    }
}
