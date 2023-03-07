using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 p;

    // Start is called before the first frame update
    void Start()
    {
        int a = 5;

        int b = 2;

        int c = a + b;

        Debug.Log(c);
    }

    // Update is called once per frame
    void Update()
    {
        if (p.y<= -4)
        {
            p = new Vector2(p.x, p.y);
            p = new Vector2(p.x, p.y + 2);
        }
        p = new Vector2(p.x, p.y + 1);

        if (p.y >= 4) 

        {
            p = new Vector2(p.x, p.y);
            p = new Vector2(p.x, p.y - 2);
        }
    

            transform.position = p;


    }
}
