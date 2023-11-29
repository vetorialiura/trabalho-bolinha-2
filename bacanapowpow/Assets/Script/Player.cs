using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 10;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direcao = new Vector2(x, y);

        rb.velocity = direcao * velocidade;
    }
}
