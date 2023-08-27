using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ColorObject
{
    public Color32 Color = new Color32(255, 255, 255, 255);
    public int price = 500;
    public bool isPurchased = false;

    public ColorObject(Color32 _color, int _price, bool _isPurchased)
    {
        Color = _color;
        price = _price;
        isPurchased = _isPurchased;
    }
}
public class ColorManager : Singleton<ColorManager>
{
    public enum coloringElement
    { 
        UI, Blue, Orange
    }

    public List<Panel> ColoringPanels;

    public ColorObject currentXBoxColor { get; private set; }
    public ColorObject currentYBoxColor { get; private set; }
    public ColorObject currentUIColor { get; private set; }

    private ObjectToSave SaveObject; 


    public void ChangeColor(ColorObject colorUI, ColorObject colorXBox, ColorObject colorYBox) 
    {            
        currentUIColor = colorUI;       
        currentXBoxColor = colorXBox;
        currentYBoxColor = colorYBox;
        foreach (var panel in ColoringPanels)
        {
            panel.ColoringObjects();
        }
        SaveParam();
    }

    private void InitColors()
    {
        currentUIColor =  new ColorObject(new Color32(255, 230, 0, 255), 0, true);
        currentXBoxColor = new ColorObject(new Color32(0, 100, 250, 255), 0, true);
        currentYBoxColor =  new ColorObject(new Color32(255, 140, 0, 255), 0, true);
    }
    private void Start()
    {
        SaveObject = SaveLoadManager.Instance.SaveObject;
        if (SaveObject.savedXBoxColor != null && SaveObject.savedYBoxColor != null && SaveObject.savedUIColor != null)
        {
            currentXBoxColor = SaveObject.savedXBoxColor;
            currentYBoxColor = SaveObject.savedYBoxColor;
            currentUIColor = SaveObject.savedUIColor;
        }
        else
        {
            InitColors();
        }
        
        SaveParam();
    }    

    private void SaveParam()
    {
        SaveObject.savedUIColor = currentUIColor;
        SaveObject.savedXBoxColor = currentXBoxColor;
        SaveObject.savedYBoxColor = currentYBoxColor;
    }


}
