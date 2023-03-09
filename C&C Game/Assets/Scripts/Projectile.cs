using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float ProjectileSpeed;
    public Vector2 CenterOffset;
    public Vector2 Bounds;
    private LayerMask Collidables;

    private Vector3 Velocity;

    //public static Quaternion Euler;

    // Start is called before the first frame update
    void Start()
    {
        Velocity = PlayerMovement.GetDirection(PlayerMovement.LastKeyDirection);
        Velocity *= ProjectileSpeed;

        switch (PlayerMovement.LastKeyDirection)
        {
            case KeyCode.W:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case KeyCode.A:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case KeyCode.S:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case KeyCode.D:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        Collidables = LayerMask.GetMask("Enemies", "Wall");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime;
        
        

        Collider2D hitObj = Physics2D.OverlapBox(transform.position + (Vector3)CenterOffset, Bounds, 0, Collidables);

        if (hitObj != null)
        {
            hitObj.GetComponent<Slime>().Die();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3)CenterOffset, Bounds);
    }
}
