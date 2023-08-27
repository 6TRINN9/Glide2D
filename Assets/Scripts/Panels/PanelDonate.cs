using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDonate : Panel
{
    public void Buy()
    {
        SaveLoadManager.Instance.SaveObject.showAds = !SaveLoadManager.Instance.SaveObject.showAds;
        ScoreManager.Instance.AddECrystals(100000);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();

    }

    public override void HidePanel()
    {
        base.HidePanel();

    }
    protected override void Start()
    {
        base.Start();
    }
}
