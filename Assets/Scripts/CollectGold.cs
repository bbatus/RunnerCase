using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : MonoBehaviour
{
private void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == 6)
    {
        Destroy(this.gameObject);
        UIManager.instance.UpdateGold(1);
    }
}
}
