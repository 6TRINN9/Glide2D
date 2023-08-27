using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PanelShop : Panel
{
    public GameObject objectColorUI;
    public GameObject objectXBox;
    public GameObject objectYBox;
    [Space]
    public Toggle toggleMusic;
    public Toggle toggleSFX;
    public AudioClip soundBuying;
    public AudioClip soundCancel;
    [Space]
    public Text textCountECrystals;
    public Text textMessage;

    public List<ColorObject> colorsUI = new List<ColorObject>();
    public List<ColorObject> colorsXBox = new List<ColorObject>();
    public List<ColorObject> colorsYBox = new List<ColorObject>();

    private ColorObject selectedUIColor;
    private ColorObject selectedXBoxColor;
    private ColorObject selectedYBoxColor;

    private ColorManager colorManager;
    private ObjectToSave SaveObject;
   

    public void ChoosingUIColor(int value)
    {  
        int index = colorsUI.IndexOf(selectedUIColor) + value;
        index = math.clamp(index, 0, colorsUI.Count - 1);           
        selectedUIColor = colorsUI[index];
        UpdateShop();
    }
   
    public void ChoosingXBoxColor(int value)
    {
        int index = colorsXBox.IndexOf(selectedXBoxColor) + value;
        index = math.clamp(index, 0, colorsXBox.Count - 1);
        selectedXBoxColor = colorsXBox[index];
        UpdateShop();
    }
    public void ChoosingYBoxColor(int value)
    {
        int index = colorsYBox.IndexOf(selectedYBoxColor) + value;
        index = math.clamp(index, 0, colorsYBox.Count - 1);
        selectedYBoxColor = colorsYBox[index];
        UpdateShop();
    }
    public void BuyColor(string value)
    {
        ColorObject @object;
        switch (value)
        {
            case "ui":
                @object = selectedUIColor;
                break;
            case "xbox":
                @object = selectedXBoxColor;
                break;
            case "ybox":
                @object = selectedYBoxColor;
                break;
            default:
                @object = null;
                break;
        }
        if (ScoreManager.Instance.RemoveECrystal(@object.price))
        {
            SoundManager.Instance.PlaySingle(soundBuying);
            @object.isPurchased = true;
            UpdateShop();
        }
        else
        {
            SoundManager.Instance.PlaySingle(soundCancel);
            textMessage.text = (GameMultiLang.GetTraduction("SNC"));
            textMessage.GetComponent<Animator>().SetTrigger("NoECrystals");
        }
    }
    public void ApplyColor()
    {
        if(selectedUIColor.isPurchased && selectedXBoxColor.isPurchased && selectedYBoxColor.isPurchased)
        {
            SoundManager.Instance.PlaySingle(soundBuying);
            colorManager.ChangeColor(selectedUIColor, selectedXBoxColor, selectedYBoxColor);
            SaveParam();
            textMessage.text = (GameMultiLang.GetTraduction("SCA"));
            textMessage.GetComponent<Animator>().SetTrigger("NoECrystals");            
        }
        else
        {
            SoundManager.Instance.PlaySingle(soundCancel);
            textMessage.text = (GameMultiLang.GetTraduction("SNB"));
            textMessage.GetComponent<Animator>().SetTrigger("NoECrystals");
        }
    }
    private void UpdateShop()
    {
        textCountECrystals.text = ($"{ScoreManager.Instance.eCrystals}");       

        //Change color child image
        objectColorUI.transform.GetChild(1).GetComponent<Image>().color = selectedUIColor.Color;

        if (!selectedUIColor.isPurchased)
        {
            objectColorUI.transform.GetChild(2).gameObject.SetActive(true);
            objectColorUI.transform.GetChild(2).GetComponent<Text>().text = ($"{selectedUIColor.price}");
        }
        else
        {
            objectColorUI.transform.GetChild(2).gameObject.SetActive(false);
        }

        objectXBox.transform.GetChild(1).GetComponent<Image>().color = selectedXBoxColor.Color;

        if (!selectedXBoxColor.isPurchased)
        {
            objectXBox.transform.GetChild(2).gameObject.SetActive(true);
            objectXBox.transform.GetChild(2).GetComponent<Text>().text = ($"{selectedXBoxColor.price}");
        }
        else
        {
            objectXBox.transform.GetChild(2).gameObject.SetActive(false);
        }

        objectYBox.transform.GetChild(1).GetComponent<Image>().color = selectedYBoxColor.Color;

        if (!selectedYBoxColor.isPurchased)
        {
            objectYBox.transform.GetChild(2).gameObject.SetActive(true);
            objectYBox.transform.GetChild(2).GetComponent<Text>().text = ($"{selectedYBoxColor.price}");
        }
        else
        {
            objectYBox.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    
    
    public void ChangeMuteMusic()
    {
        SoundManager.Instance.TurnMuteMusic(toggleMusic.isOn);
    }
    public void ChangeMuteSound()
    {
        SoundManager.Instance.TurnMuteSFX(toggleSFX.isOn);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();


        toggleSFX.isOn = SoundManager.Instance.muteSFX;
        toggleMusic.isOn = SoundManager.Instance.muteMusic;
        

        LoadParam();
        UpdateShop();
        SaveParam();

    }

    public override void HidePanel()
    {
        base.HidePanel();
        SaveParam();
    }

    private void LoadParam()
    {
        selectedUIColor = colorManager.currentUIColor;
        selectedXBoxColor = colorManager.currentXBoxColor;
        selectedYBoxColor = colorManager.currentYBoxColor;

        if (SaveObject.savedColorsUI != null && SaveObject.savedColorsXBox != null && SaveObject.savedColorsYBox != null)
        {
            colorsUI = SaveObject.savedColorsUI;
            colorsXBox = SaveObject.savedColorsXBox;
            colorsYBox = SaveObject.savedColorsYBox;
        }
    }
    private void SaveParam()
    {
        SaveObject.savedColorsUI = colorsUI;
        SaveObject.savedColorsXBox = colorsXBox;
        SaveObject.savedColorsYBox = colorsYBox;        
    }

    protected override void Start()
    {
        base.Start();
        SaveObject = SaveLoadManager.Instance.SaveObject;
        colorManager = ColorManager.Instance;
        SaveParam();
    }

}
