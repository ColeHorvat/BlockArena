using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Camera cam;

    float angle;
    Rigidbody2D rb2d;
    Vector2 mousePos;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(angle < -90 || angle > 90) spriteRenderer.flipY = true;
        else spriteRenderer.flipY = false;
    }

    void FixedUpdate() 
    {
        Vector2 lookDir = mousePos - rb2d.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //float finalAngle = Mathf.Clamp(angle, -65, 65);
        
        //transform.Rotate(Vector3.back * Time.deltaTime * angle);
        
    }

    private void LateUpdate()
    {
        rb2d.rotation = angle;
    }
}
