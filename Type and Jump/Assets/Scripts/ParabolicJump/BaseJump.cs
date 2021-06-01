using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseJump : MonoBehaviour
{
    public float sayi;

    private void Update()
    {
        sayi += Time.deltaTime;
        sayi %= 5f;

        transform.position = MathParabola.Parabola(Vector2.zero, Vector2.right * 10f, 5f, sayi / 5f);
    }
}
