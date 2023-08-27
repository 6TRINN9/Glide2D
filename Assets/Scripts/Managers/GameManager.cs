using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameObject restartButton;
    public GameObject nextLevelButton;
    public Text textCurrentLevel;    
    public Text textCountECrystals;    
    public Text textTutor;    

    private LevelManager _levelManager;

    
    private bool showAds = true;   
    private static int _advCount = 0;



    public void RestartLevel()
    {
        _advCount += 1;
        if (showAds && _advCount % 3 == 0)
        {
            
        }

        _levelManager.RestarLevel();
        SoundManager.Instance.PlayRestartLevelSound();
        InitGame();
    }

    public void UnlockNextLevel()
    {
        nextLevelButton.SetActive(true);
        SoundManager.Instance.PlayUnlockLevelSound();
    }

    public void NextLevel()
    {
        if (showAds && _levelManager.currentLevel % 5 == 0)
        {
                _advCount = 0;
                
        }
        if (_levelManager.currentLevel % 5 == 0)
            ScoreManager.Instance.AddECrystals(75);
        else
            ScoreManager.Instance.AddECrystals(25);
        _levelManager.LoadNextLevel();
        textTutor.enabled = false;
        InitGame();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        SoundManager.Instance.PlaySingle(SoundManager.Instance.soundButton1);
    }
    
    private void InitGame()
    {
        textCurrentLevel.text = ($"{GameMultiLang.GetTraduction("LEVEL")}: {_levelManager.currentLevel}");
        textCountECrystals.text = ScoreManager.Instance.eCrystals.ToString();
        nextLevelButton.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
    }

    private void Start()
    {
        _levelManager = LevelManager.Instance;

        if( _levelManager != null)
        {
            _levelManager.Generate(_levelManager.lastCompletedLevel + 1);
            InitGame();
        }
        if (_levelManager.currentLevel != 1)
            textTutor.gameObject.SetActive(false);

        if(SaveLoadManager.Instance.SaveObject != null)
            showAds = SaveLoadManager.Instance.SaveObject.showAds;
    }
   

}
