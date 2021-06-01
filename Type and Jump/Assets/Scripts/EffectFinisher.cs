using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFinisher : MonoBehaviour
{
    public void FinishAnimation()
    {
        Destroy(this.gameObject);
    }
}
