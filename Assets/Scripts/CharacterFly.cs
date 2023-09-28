using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFly : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
 
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                direction = Vector3.up * strength;

        }

        direction.y += gravity * Time.deltaTime;
        // Move the character
        transform.position += direction * Time.deltaTime;
    }

 }