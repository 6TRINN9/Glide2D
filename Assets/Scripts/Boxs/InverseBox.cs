
using UnityEngine;

public class InverseBox : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject @object = collision.gameObject;
        BoxManager.Instance.InverseOneBlock(@object);
        Destroy(gameObject);
    }
}
