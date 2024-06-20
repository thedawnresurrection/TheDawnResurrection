using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
   
    public float speed = 10f;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // KARAKTER SA�A SOLA HAREKET� OLU�TURMA G�RSEL 1

    private void Update()
    {
        rb.velocity = (Vector2) (transform.right * speed);
    }



}



