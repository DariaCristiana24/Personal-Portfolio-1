using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    bool canDamage = false;
    bool weaponActivated = true;
    [SerializeField]
    GameObject weapon;

    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
    }

    void inputs()
    {
        if (Input.GetMouseButton(0) && weaponActivated && !uiManager.Paused)
        {
            Attack();
        }
    }

    public void EnableDamage()
    {
        Debug.Log("can attck");
        canDamage = true;
    }

    public void DisableDamage()
    {
        Debug.Log("cant attack");
        canDamage = false;
    }

    public bool GetDamageStatus()
    {
        if (canDamage)
        {
            return true;
        }

        return false;
    }

    public void Attack()
    {
        if (anim != null)
        {
            anim.SetTrigger("AnimationTrigger");
        }
    }
}
