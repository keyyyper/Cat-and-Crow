using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Direction
//{    Up,
 //   Down,
 //   Right,
//    Left
//}



public class Slime : MonoBehaviour
{
    public Vector3 Velocity;
    public float MoveSpeed;
    public float CheckDistance;
    public LayerMask Wall;

    public GameObject DropPrefab;
   // public Direction StartingDirection;
   // public float Speed;
   // public float CheckDistance;
   // public LayerMask Wall;
    //note wall isnt on tilemap yet for colliders

  //  private Vector3 Velcocity;



    // Start is called before the first frame update
    void Start()
    {
    //    Velocity = GetDirection(StartingDirection);
    }

   // public Vector3 GetDirection(Direction dir) => dir switch
   // {
    //    Direction.Up => Vector3.up,

    //    Direction.Down => Vector3.down,

    //    Direction.Right => Vector3.right,

   //     Direction.Left => Vector3.left,

   //     _ => Vector3.zero
   // };





    // Update is called once per frame
    void Update()
    {
         transform.position += Velocity * MoveSpeed * Time.deltaTime;

         RaycastHit2D hit = Physics2D.Raycast(transform.position, Velocity, CheckDistance, Wall);


        if (hit != false)
        {
            Velocity *= -1;

            transform.Rotate(0, 180, 0);
        }

      //  transform.position += Velocity * Speed * Time.deltaTime;

      //  RaycastHit2D hit = Physics2D.Raycast(transform.position, Velocity.normalized, CheckDistance, Wall);

      //  if (hit != false)
      //  {
      //      Velocity *= -1;

       //     transform.rotation *= Quaternion.Euler(0, 180, 0);
       // }

    }

    public void Die()
    {
        Instantiate(DropPrefab).transform.position = transform.position;
        //play die anim here
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (Velocity * CheckDistance));
    }
}
