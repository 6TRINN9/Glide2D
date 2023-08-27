using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBox : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {        
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    
    private void OnCollisionStay2D(Collision2D collision)
    {        
        if (collision != null)
        {
            collision.gameObject.GetComponent<Box>().isMayBeReplaced = false;
            StartCoroutine(activatingTimer());
            
        }
        else
        {
            
        }
        
        
    }


    IEnumerator activatingTimer()
    {        
        float time = 3f;
        spriteRenderer.color = new Color32(240, 240, 240, 60);
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(time);        
        boxCollider2D.enabled = true;
        spriteRenderer.color = new Color32(240, 240, 240, 255);
    }
}
