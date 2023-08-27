using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMainMenu : Panel
{
    public Text textCompletedLevels;

    public void StartGame()
    {
        SoundManager.Instance.PlaySingle(SoundManager.Instance.soundButton1);
        SceneManager.LoadScene("PlayGround", LoadSceneMode.Single);
    }

    protected override void Start()
    {
        base.Start();

        textCompletedLevels.text = ($"{GameMultiLang.GetTraduction("COMLP")} {LevelManager.Instance.lastCompletedLevel}/{LevelManager.Instance.levelsCount}");
    }
}
