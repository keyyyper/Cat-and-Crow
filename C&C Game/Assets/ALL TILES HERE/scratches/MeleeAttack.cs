using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Animator weaponAnim;

    public float AttackRadius;

    public Vector3 AttackOffset;

    private Vector3 attackDir;


   // private float attackTimer;

   // public float attackCooldown;

   // public float attackTotalTime;


   // bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

       
            weaponAnim.Play("MeleeAttack");

      //  }
     //   else
       // {
            //attackTimer += attackTimer.deltaTime;
        }
    }

  //  void PreformAttack()
   // {
        
       // if (Input.GetKeyDown(KeyCode.Space))
      //  {


            //Attack!
        //    weaponAnim.SetBool("isAttacking", true);
      //  }
     //   else
      //  {
          //  weaponAnim.SetBool("isAttacking", false);
       // }
  //  }
  //  bool CanAttack()
  //  {
      
       // if (attackTimer > attackTotalTime)
     //   {
            //we are still attacking, so dont attack again
       //     attackTimer += Time.deltaTime;
         //   return true;
      //  }

      //  attackTimer = 0;
      //  return false;
    //}

}
