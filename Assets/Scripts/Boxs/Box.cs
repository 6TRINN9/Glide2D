using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Sound")]
    public AudioClip soundBoxSelect;
    public AudioClip soundBoxDestroy;
    public AudioClip soundBoxMove;
    public AudioClip soundBoxCancel;
  
    public bool isMayBeReplaced = true;
    public bool isMoving = false;


    private BoxCollider2D boxCollider2D;
    private Vector3 touchOrigin;
    private Vector3 positionOrigin;
    private Vector3 directionDrag;
    private bool touched = false;

    private bool IsCanMoving(GameObject _target)
    {
        if (_target == null)
            return false;
        if (_target.GetComponent<Box>())
            if (_target.GetComponent<Box>().isMoving || _target.GetComponent<Box>().isMayBeReplaced == false)
                return false;

        string _tagBlock = gameObject.tag;
        string _tagTarget = _target.tag;        

        if (_tagBlock != _tagTarget && _tagTarget != "Wall")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void MoveBox(GameObject _target)
    {
        if(IsCanMoving(_target))
        {
            isMoving = true;
            SoundManager.Instance.PlaySingle(soundBoxMove);
            StartCoroutine(SmoothMovement(_target.transform.position));
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Cancel");
            SoundManager.Instance.PlaySingle(soundBoxCancel);
        }
    }
    
    private IEnumerator SmoothMovement(Vector3 endPosition)
    {     
        float sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;
        isMayBeReplaced = true;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, endPosition, 5f * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;
            yield return null;
        }

        isMoving = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMoving)
            return;
        touched = true;
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        SoundManager.Instance.PlaySingle(soundBoxSelect);
        touchOrigin = eventData.position;
        positionOrigin = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isMoving || !touched)
            return;
        
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        transform.position = positionOrigin;
        touched = false;
    }
      
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMoving || !touched)
            return;

        Debug.Log(" Dragging ...");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMoving || !touched)
            return;

        boxCollider2D.enabled = false;
        int _horizontal = 0;
        int _vertical = 0;

        Vector3 touchEnd = eventData.position;
        float x = touchEnd.x - touchOrigin.x;
        float y = touchEnd.y - touchOrigin.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
            _horizontal = x > 0 ? 1 : -1;
        else
            _vertical = y > 0 ? 1 : -1;

        directionDrag = new Vector3(_horizontal, _vertical, 0);
        transform.position = positionOrigin + directionDrag / 10;
        Debug.DrawRay(transform.position, directionDrag * 5f, Color.red);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionDrag, 5f);
        boxCollider2D.enabled = true;
        if(hit)
            MoveBox(hit.collider.gameObject);
        else
            MoveBox(null);
    }

    private void Start()
    {
        if(gameObject.tag == "XBox")
           GetComponent<SpriteRenderer>().color = ColorManager.Instance.currentXBoxColor.Color;
        else
            GetComponent<SpriteRenderer>().color = ColorManager.Instance.currentYBoxColor.Color;

        boxCollider2D = GetComponent<BoxCollider2D>();
        BoxManager.Instance.AddBlock(gameObject);       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<Box>() && !isMoving)
        {
            BoxManager.Instance.RemoveBlock(gameObject);
            Destroy(gameObject);
        }
    }

}
