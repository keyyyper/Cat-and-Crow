//using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Component Variables")]
    public Animator PlayerAnim;
    public Animator WeaponAnim;

    //private List<GameObject> Inventory = new List<GameObject>();
    //Inventory.Add(object)
    //17:30ish time code in video

    [Header("Movement Variables")]
    public float PlayerSpeed;
    private KeyCode LastKeyPressedInFrame;
    [HideInInspector]
    public static KeyCode LastKeyDirection;
    private Vector3 Velocity;
    private List<KeyCode> KeysPressedInFrame = new List<KeyCode>();

    public LayerMask Enemies;

    [Header("Melee Attack Variables")]
    public float MeleeAttackRadius;
    public float MeleeAttackRange;
    public float TotalMeleeTime;
    private bool isMeleeAttacking;
    private float meleeTimer;

    [Header("Ranged Attack Variables")]
    public GameObject ProjectilePrefab;
    public float RangedAttackCooldown;
    private float rangedAttackTimer;
    private bool hasRangedAttacked;

    [Header("Interact Variables")]
    public float InteractRange;
    public float InteractRadius;
    public LayerMask Interactables;


    //  public int PlayerSpeed;

    //  public Vector3 Velocity;

    //  public Animator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        LastKeyDirection = KeyCode.D;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);

        CheckMovementInput();
        CheckMeleeAttackInput();
        //   CheckRangedAttackInput();
        MeleeAttackTimer();
        //   RangedAttackTimer();
        Interact();
        WalkingMovement();
        IdleMovement();
    }

    void CheckMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("up");
            KeysPressedInFrame.Add(KeyCode.W);
        }
        else if (Input.GetKeyUp(KeyCode.W))
            KeysPressedInFrame.Remove(KeyCode.W);
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~up
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("down");
            KeysPressedInFrame.Add(KeyCode.S);
        }
        else if (Input.GetKeyUp(KeyCode.S))
            KeysPressedInFrame.Remove(KeyCode.S);
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~down
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("left");
            KeysPressedInFrame.Add(KeyCode.A);
        }
        else if (Input.GetKeyUp(KeyCode.A))
            KeysPressedInFrame.Remove(KeyCode.A);
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~left
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("right");
            KeysPressedInFrame.Add(KeyCode.D);
        }
        else if (Input.GetKeyUp(KeyCode.D))
            KeysPressedInFrame.Remove(KeyCode.D);
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~right
        if (KeysPressedInFrame.Count > 0)
        {
            Debug.Log("several keys pressed");
            LastKeyPressedInFrame = KeysPressedInFrame[^1];
            LastKeyDirection = LastKeyPressedInFrame;
        }
        else
        {
            Debug.Log("idle");
            LastKeyPressedInFrame = KeyCode.None;
        }

        if (!isMeleeAttacking)
        {
            Velocity = GetDirection(LastKeyPressedInFrame) * PlayerSpeed;

            transform.position += Velocity * Time.deltaTime;
        }

    }

    void CheckMeleeAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMeleeAttacking)
        {
            //can attack so attack
            isMeleeAttacking = true;
            //here is where we put attack anim
            //WeaponAnim.Play("MeleeAttack");

            Vector3 CastDirection;
            if (GetDirection(LastKeyPressedInFrame) == Vector3.zero)
                CastDirection = transform.position + (GetDirection(LastKeyDirection) * MeleeAttackRange);
            else
                CastDirection = transform.position + (GetDirection(LastKeyPressedInFrame) * MeleeAttackRange);

            Collider2D hitObj = Physics2D.OverlapCircle(CastDirection, MeleeAttackRadius, Enemies);

            //enemies is layermask

            // if (hitObj != null)
            //     hitObj.GetComponent<Slime>().Die();
            // red is where slime script would go
        }
    }

    //void CheckRangedAttackInput()
    //{
    //if (Input.GetKeyDown(KeyCode.F) && !isMeleeAttacking && !hasRangedAttacked)
    //{
    //can attack so attack
    //   hasRangedAttacked = true;
    //here is where we put ranged anim

    //   Vector3 CastDirection;
    //   if (GetDirection(LastKeyPressedInFrame) == Vector3.0)
    //           CastDirection = transform.position + (GetDirection(LastKeyDirection) * MeleeAttackRange);
    //  else
    //       CastDirection = transform.position + (GetDirection(LastkeyPressedInFrame) * MeleeAttackRange);
    //instantiate projectile yeye
    //   Instantiate(ProjectilePrefab).transform.position = transform.position;
    //  }
    // }

    void MeleeAttackTimer()
    {
        if (isMeleeAttacking)
        {
            if (meleeTimer > TotalMeleeTime)
            {
                meleeTimer = 0;
                isMeleeAttacking = false;
                //play silly little animation hurray
            }
            meleeTimer += Time.deltaTime;
        }
    }

    // void RangedAttackTimer()
    // {
    //     if (hasRangedAttacked)
    //     {
    //         if (rangedAttackTimer > RangedAttackCooldown)
    //         {
    //             rangedAttackTimer = 0;
    //             hasRangedAttacked = false;
    //eyooo another animation yipee
    //        }
    //         rangedAttackTimer += Time.deltaTime;
    //    }
    // }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            Vector3 CastDirection;
            if (GetDirection(LastKeyPressedInFrame) == Vector3.zero)
                CastDirection = transform.position + (GetDirection(LastKeyDirection) * InteractRange);
            else
                CastDirection = transform.position + (GetDirection(LastKeyPressedInFrame) * InteractRange);

            Collider2D hitObj = Physics2D.OverlapCircle(CastDirection, InteractRadius, Interactables);

            //   if (hitObj != null)
            //       hitObj.GetComponent<Interactable>().Pickup();
            //red is where interactable script goes
        }
    }

    public static Vector3 GetDirection(KeyCode LastPressedKey) => LastPressedKey switch
    {
        KeyCode.W => Vector3.up,
        KeyCode.S => Vector3.down,
        KeyCode.A => Vector3.left,
        KeyCode.D => Vector3.right,
        KeyCode.None => Vector3.zero,
        _ => Vector3.zero
    };

    void WalkingMovement()
    {
        if (!isMeleeAttacking)
        {
            //switch (LastKeyPressedInFrame)
            //{
            //    case

            //    LastKeyPressedInFrame => Vector3.up, PlayerAnim.Play("PlayerWalk_up");

            //if last key pressed in frame = to vector3.up , do this code/ play this anim
            //        break;

            //    case LastKeyPressedInFrame 

            //        break;


            //    default IdleMovement();

            //        break;

            //}

            if (GetDirection(LastKeyPressedInFrame) == Vector3.up)
                PlayerAnim.Play("PlayerWalk_up");

            else if (GetDirection(LastKeyPressedInFrame) == Vector3.down)
                PlayerAnim.Play("PlayerWalk_down");

            else if (GetDirection(LastKeyPressedInFrame) == Vector3.left)
                PlayerAnim.Play("PlayerWalk_left");

            else if (GetDirection(LastKeyPressedInFrame) == Vector3.right)
                PlayerAnim.Play("PlayerWalk_right");

            else
                IdleMovement();
        }

    }

    void IdleMovement()
    {
        if (GetDirection(LastKeyDirection) == Vector3.up)
            PlayerAnim.Play("PlayerIdle_up");

        if (GetDirection(LastKeyDirection) == Vector3.down)
            PlayerAnim.Play("PlayerIdle_down");

        if (GetDirection(LastKeyDirection) == Vector3.left)
            PlayerAnim.Play("PlayerIdle_left");

        if (GetDirection(LastKeyDirection) == Vector3.right)
            PlayerAnim.Play("PlayerIdle_right");
    }

    private void OnDrawGizmos()
    {
        Vector3 CastDirection;
        if (isMeleeAttacking)
        {
            //  Gizmos.color = Color.red;

            if (GetDirection(LastKeyPressedInFrame) == Vector3.zero)
                CastDirection = transform.position + (GetDirection(LastKeyDirection) * MeleeAttackRange);
            else
                CastDirection = transform.position + (GetDirection(LastKeyPressedInFrame) * MeleeAttackRange);
            Gizmos.DrawWireSphere(CastDirection, MeleeAttackRadius);
        }
        else
        {
            if (GetDirection(LastKeyPressedInFrame) == Vector3.zero)
                CastDirection = transform.position + (GetDirection(LastKeyDirection) * InteractRange);
            else
                CastDirection = transform.position + (GetDirection(LastKeyPressedInFrame) * InteractRange);
            Gizmos.DrawWireSphere(CastDirection, InteractRadius);
        }
    }

    //    CheckMovementInput();


    //if (Input.GetKey(KeyCode.S))

    //     {
    //    transform.position += new Vector3(0, PlayerSpeed * Time.deltaTime, 0); }

    //    if (Input.GetKey(KeyCode.X))
    //   {
    //      transform.position += new Vector3(0, -PlayerSpeed * Time.deltaTime, 0); }

    //  if (Input.GetKey(KeyCode.C))
    //     {
    //       transform.position += new Vector3( PlayerSpeed * Time.deltaTime, 0, 0); }

    //    if (Input.GetKey(KeyCode.Z))
    //       {
    //         transform.position += new Vector3( -PlayerSpeed * Time.deltaTime, 0, 0); }

    // transform.position += Velocity * Time.deltaTime;


    //  void CheckMovementInput()
    //  {
    //     Velocity = Vector3.zero;


    //      if (Input.GetKey(KeyCode.W))

    //       {
    //        playerAnim.Play("PlayerWalk_up");
    //         Velocity += Vector3.up;
    //    }

    //   else
    //    {
    //      playerAnim.Play("PlayerIdle_up");
    //   }


    //    if (Input.GetKey(KeyCode.S))

    //   {
    //        playerAnim.Play("PlayerWalk_down");
    //       Velocity += Vector3.down;
    //   }
    //   else
    //    {
    //       playerAnim.Play("PlayerIdle_down");
    //   }


    //   if (Input.GetKey(KeyCode.D))

    //   {
    //       playerAnim.Play("PlayerWalk_right");
    //      Velocity += Vector3.right;
    //   }


    //   if (Input.GetKey(KeyCode.A))

    //   {
    //      playerAnim.Play("PlayerWalk_left");
    //       Velocity += Vector3.left;
    //   }


    //    transform.position += Velocity * (.005f + Time.deltaTime);

    //    Velocity *= PlayerSpeed;
    // }
}


