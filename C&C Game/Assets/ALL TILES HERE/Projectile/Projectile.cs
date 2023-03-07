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


    // Start is called before the first frame update
    void Start()
    {
        Velocity = PlayerMovement.GetDirection(Player.LastKeyDirection);
        Velocity *= ProjectileSpeed;

        Collidables = LayerMask.NameToLayer("Enemies") | LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime;

        Collider2D hitObj = Physics2D.OverlapBox(transform.position + (Vector3)CenterOffset, Bounds, 0, Collidables);

        if (hitObj != null)
            hitObj.GetComponent<Slime>().Die();
           Destroy(gameObject);
    }
}
