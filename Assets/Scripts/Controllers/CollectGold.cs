using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            this.gameObject.SetActive(false);
            UIManager.instance.UpdateGold(1);
            Debug.Log("Gold toplandi");
        }
    }

    public void ResetGold()
    {
        this.gameObject.SetActive(true);
    }
}
