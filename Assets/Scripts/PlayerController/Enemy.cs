using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int life = 100;

    PlayerAttack weapon;
    ItemHeld itemHeld;
    // Start is called before the first frame update
    void Start()
    {
        itemHeld = FindObjectOfType<ItemHeld>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (life <= 0)
        {
            Destroy(gameObject);
        }
        Timer();
    }

    private void OnTriggerStay(Collider other)
    {
        weapon = itemHeld.activeWeapon;
        if (weapon.GetDamageStatus() && !cooldown)
       {
            if (other.tag == "Weapon")
            {
                life -= 100;
                Debug.Log("attacked");
                cooldown = true;
            }
        }
    }
    float cooldownLength = 1;
    float timer = 1;
    bool cooldown = false;
    void Timer()
    {
        if (timer > 0 && cooldown)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            cooldown = false;
            timer = cooldownLength;
        }
    }
}
