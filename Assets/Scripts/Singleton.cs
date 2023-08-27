using UnityEngine;

public class Singleton<T> : MonoBehaviour
where T : MonoBehaviour
{
     
    private static T _instance;
    
    public static T Instance
    {
        get
        {              
            if (_instance == null)
            {                
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError("Can't find " + typeof(T) + "!");
                }
            }
            return _instance;
        }
    }


}