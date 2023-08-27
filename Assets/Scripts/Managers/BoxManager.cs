using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager Instance = null;
    
    public AudioClip soundBoxDestroy;
    public AudioClip soundBoxMove;
    public AudioClip soundBoxCancel;

    public List<GameObject> blockList;
    public bool IsMovingBlock = false;

    public void AddBlock(GameObject Box)
    {
        blockList.Add(Box);
    }

    public void RemoveBlock(GameObject Box)
    {       
        blockList.Remove(Box);
        CheckCoutBlocks();
    }

    public void ClearBlockList()
    {
        blockList.Clear();
    }
    private void CheckCoutBlocks()
    {
        if (blockList.Count == 1)
        {
            Debug.Log("Game WIN");
            GameManager.Instance.UnlockNextLevel();
        }
    }
    /*
    public void MoveBlock(GameObject _block, GameObject _target)
    {
        if (_target.GetComponent<Box>())       
            if (_target.GetComponent<Box>().isMoving)
                return;

        string _tagBlock = _block.gameObject.tag;
        string _tagTarget = _target.tag;

        if( _tagBlock != _tagTarget && _tagTarget != "Wall" )
        {           
            SoundManager.Instance.PlaySingle(soundBoxMove);
            StartCoroutine(SmoothMovement(_block, _target));           
        }
        else
        {
            _block.GetComponent<Animator>().SetTrigger("Cancel");
            SoundManager.Instance.PlaySingle(soundBoxCancel);
        }
    }
    private IEnumerator SmoothMovement(GameObject _movingObject, GameObject _target)
    {
        Vector3 endPosition = _target.transform.position;
        _movingObject.GetComponent<Box>().isMayBeReplaced = true;
        _movingObject.GetComponent<Box>().isMoving = true;
        IsMovingBlock = true;

        float sqrRemainingDistance = (_movingObject.transform.position - endPosition).sqrMagnitude;
       
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(_movingObject.transform.position, endPosition, 5f * Time.deltaTime);
            _movingObject.transform.position = newPosition;
            sqrRemainingDistance = (_movingObject.transform.position - endPosition).sqrMagnitude;
            yield return null;
        }
        IsMovingBlock = false;        
        _movingObject.GetComponent<Box>().isMoving = false;        
    }*/

    public void InverseOneBlock(GameObject _block)
    {
        if (_block.tag == "XBox")
        {
            _block.tag = "YBox";
            _block.GetComponent<SpriteRenderer>().color = ColorManager.Instance.currentYBoxColor.Color;
        }
        else if (_block.tag == "YBox")
        {
            _block.tag = "XBox";
            _block.GetComponent<SpriteRenderer>().color = ColorManager.Instance.currentXBoxColor.Color;
        }
    }



    private void Awake()
    {
        Instance = this;
    }

}
