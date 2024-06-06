using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CompositeToggle : MonoBehaviour
{
    private TilemapCollider2D tilemapCollider;

    private void Start()
    {
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    public void EnableComposite()
    {
        if (tilemapCollider != null)
        {
            tilemapCollider.usedByComposite = true;
        }
    }

    public void DisableComposite()
    {
        if (tilemapCollider != null)
        {
            tilemapCollider.usedByComposite = false;
        }
    }
}