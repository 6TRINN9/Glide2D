using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [Header("Object prefabs")]
    public  GameObject prefBlueBlock;
    public  GameObject prefOrangeBlock;
    public  GameObject prefEmptyBlock;
    public  GameObject prefWall;
    public  GameObject prefSwitcher;

    


    //Size playing grid
    private static int columns = 5, rows = 5;

    private ArrayOfLevels _arrayLevels = new ArrayOfLevels();
    private Level _cacheLevel;
    private List<GameObject> _cacheObjects = new List<GameObject>();
    
    public int levelsCount { get; private set; }    
    public int currentLevel { get; private set; } 
    public int lastCompletedLevel { get; private set; }



    /*public Dictionary<string, GameObject> prefabsChar = new Dictionary<string, GameObject>
    {
        ["-"] = null,
        ["b"] = prefBlueBlock,
        ["o"] = prefOrangeBlock,
        ["e"] = prefEmptyBlock,
        ["w"] = prefWall,
        ["s"] = prefSwitcher
    };*/




    private GameObject GetObjectByString(string value)
    {
        switch (value)
        {
            case "-":
                return null;
            case "b":
                return prefBlueBlock;
            case "o":
                return prefOrangeBlock;
            case "w":
                return prefWall;
            case "e":
                return prefEmptyBlock;
            case "s":
                return prefSwitcher;
            default:
                return null;
        }

    }

    //Caching level for not to reload the level
    private void CachingLevel(int numLevel )
    {
        _cacheLevel = _arrayLevels.levels[numLevel - 1];
        currentLevel = _cacheLevel.id;
    }

    private void ClearLevel()
    {
        foreach ( var _object in _cacheObjects)
        {
            //_cacheObjects.Remove(_object);
            Destroy(_object);
        }
        BoxManager.Instance.ClearBlockList();
    }

    public void Generate(int numLevel)
    {  
        ClearLevel();

        if (numLevel > levelsCount)
            return;

        
        if (numLevel != currentLevel)           
            CachingLevel(numLevel);        

        

        for (int i = 0; i < rows; i += 1)
        {
            float y = 4;            
            y -= i;                       
            
            string _row = _cacheLevel.rows[i]; 

            for(int x = 0; x < columns; x += 1)
            {                
                string _sym = _row[x].ToString();   
                 
                GameObject _object = GetObjectByString(_sym); 
                
                if (_object == null)
                    continue;
                _cacheObjects.Add( Instantiate( _object, new Vector3(x, y, 0f), Quaternion.identity ) );
            }
        }
    }

    public void RestarLevel()
    {
        Generate(currentLevel);
    }

    public void LoadNextLevel()
    {
        if (currentLevel > lastCompletedLevel)
        {
            lastCompletedLevel = currentLevel;
            SaveLoadManager.Instance.SaveObject.lastCompletedLevel = lastCompletedLevel;
        }
        Generate(currentLevel + 1);        
    }

    //Load array with levels from file
    public void InitArrayLevels()
    {     
        TextAsset targetFile = Resources.Load<TextAsset>("Levels");
        string _loadedData = targetFile.text;

        if (targetFile != null)
        {            
            _arrayLevels = JsonUtility.FromJson<ArrayOfLevels>(_loadedData);
        }
        else
        {
            Debug.LogError("File Levels not found");
        }
    }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        InitArrayLevels();
        //Count amount levels
        levelsCount = _arrayLevels.levels.Count;
        lastCompletedLevel = SaveLoadManager.Instance.SaveObject.lastCompletedLevel;

    }


}
