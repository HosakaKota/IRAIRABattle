using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderNon : MonoBehaviour
{
    private Collider[] CapCollArray;



    void OnEnable()
    {
        // CapselColliderの取得
        CapCollArray = this.GetComponentsInChildren<CapsuleCollider>();
    }

    public void Enabled_Collider()
    {
        // Colliderを有効化する
        foreach (CapsuleCollider col in CapCollArray)
        {
            col.gameObject.SetActive(true);
        }
    }

    public void Disabled_Collider()
    {
        // Colliderを無効化する
        foreach (CapsuleCollider col in CapCollArray)
        {
            col.gameObject.SetActive(false);
        }
    }
}