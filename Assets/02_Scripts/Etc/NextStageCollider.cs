using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.Instance.GoNextMap();
    }
}
