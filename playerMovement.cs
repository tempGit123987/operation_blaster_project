using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    //left - movement, right - looking around
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    
    
    public int moveSpeed = 1;
    //movementVec will be the input of the left stick to actually make it move
    Vector2 movementVec;
    //lookVec is the input of the right stick to make it rotate
    Vector2 lookVec;
    public Rigidbody2D rB;

    public Transform bulletPosition;
    public GameObject shotgunHolder;
    public GameObject[] shotgunHolderPos;

    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    float currentTime;
    public int currentWeapon = 0;

    private bool isVaulnerable;
    private int playerHealth = 2;
    public List<GameObject> hearts;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        isVaulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //left stick's movement
        movementVec.x = leftJoystick.Horizontal;
        movementVec.y = leftJoystick.Vertical;

        //right stick's movement
        lookVec.x = rightJoystick.Horizontal;
        lookVec.y = rightJoystick.Vertical;

        float rotAngle = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg; //calculates the angle to look
        
        if(currentWeapon == 0)
        {
            fireRate = 1.0f;
        }
        
        if(lookVec.x != 0 || lookVec.y != 0) //checks to see if right stick is moving
        {
            transform.rotation = Quaternion.Euler(0, 0, rotAngle - 90); //points player to right stick direction
            Fire();
        }

        if(playerHealth < 0)
        {
            SceneManager.LoadScene("test_01");
            Debug.Log("GameOver");
        }
    }

    void ChangeWeapon(int weaponNum)
    {
        currentWeapon = weaponNum;
    }

    void Fire()
    {
        
        if(Time.time > currentTime)
        {
            if(currentWeapon == 0 || currentWeapon == 1)
            {

            GameObject thisBullet = Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation);
            Rigidbody2D bulletRb = thisBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bulletPosition.up * 10f, ForceMode2D.Impulse);
            }
            
            if(currentWeapon == 2)
            {
                foreach(GameObject bulletPos in shotgunHolderPos)
                {
                    GameObject thisBullet = Instantiate(bulletPrefab, bulletPos.transform.position, 
                    bulletPos.transform.rotation);
                    Rigidbody2D bulletRb = thisBullet.GetComponent<Rigidbody2D>();
                    bulletRb.AddForce(bulletPos.transform.up * 10f, ForceMode2D.Impulse);
                }
                    
            }

            currentTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        //move the player
        rB.MovePosition(rB.position + movementVec * moveSpeed * Time.fixedDeltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Heart") && playerHealth < 2)
        {
            playerHealth++;
            hearts[playerHealth].SetActive(true);
        }

        if(other.CompareTag("MG"))
        {
            currentWeapon = 1;
            fireRate = 0.25f;
            shotgunHolder.SetActive(false);
        }

        if(other.CompareTag("Shotgun"))
        {
            currentWeapon = 2;
            fireRate = 1.0f;
            shotgunHolder.SetActive(true);
        }
    }

    void TakeDamage()
    {
        if(isVaulnerable == true)
        {
            isVaulnerable = false;
            hearts[playerHealth].SetActive(false);
            playerHealth--;
            
        
            StartCoroutine(Invulnerability());
        }
    }

    IEnumerator Invulnerability()
    {
        yield return new WaitForSeconds(2f);
        isVaulnerable = true;
    }
}
