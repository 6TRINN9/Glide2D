using UnityEngine;

public class MainMenuManager : MonoBehaviour
{    
    private bool isFirstStart = true;


    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }

}
