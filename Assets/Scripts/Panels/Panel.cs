using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public bool RL = false;
    public List<GameObject> objectToColoring;
    private Animator animPanel;

    public void ColoringObjects()
    {
        foreach(var @object in objectToColoring)
        {
            Image image = @object.GetComponent<Image>();
            image.color = ColorManager.Instance.currentUIColor.Color;
        }
    }
    public virtual void ShowPanel()
    {        
        SoundManager.Instance.PlaySingle(SoundManager.Instance.soundButton1);
        animPanel = GetComponent<Animator>();
        animPanel.SetFloat("Speed", 1f);
        if (RL)
        {
            animPanel.SetTrigger("ShowRL");
        }
        else
        {
            animPanel.SetTrigger("ShowLR");
        }        
    }
    
    public virtual void HidePanel()
    {
        SoundManager.Instance.PlaySingle(SoundManager.Instance.soundUIBack);
        animPanel.SetFloat("Speed", -1f);
        if (RL)
        {
            animPanel.SetTrigger("ShowRL");
        }
        else
        {
            animPanel.SetTrigger("ShowLR");
        }
    }

    protected virtual void Start()
    {
        ColorManager.Instance.ColoringPanels.Add(this);
        ColoringObjects();
    }
    private void OnDisable()
    {
        if(ColorManager.Instance)
            ColorManager.Instance.ColoringPanels.Remove(this);
    }
}
