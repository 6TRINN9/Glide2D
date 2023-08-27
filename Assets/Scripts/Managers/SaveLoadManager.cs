using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectToSave
{
    //Class for save progerss 
    public int lastCompletedLevel = 0;
    public int eCrystals = 0;
    public bool showAds = true;

    public ColorObject savedXBoxColor = null;
    public ColorObject savedYBoxColor = null;
    public ColorObject savedUIColor = null;

    public List<ColorObject> savedColorsUI;
    public List<ColorObject> savedColorsXBox;
    public List<ColorObject> savedColorsYBox;

    public bool muteMusic;
    public bool muteSFX;
    public bool isFirstStart = true;


}
public class SaveLoadManager : Singleton<SaveLoadManager>
{   
    private ObjectToSave _saveObject = new ObjectToSave();
    public ObjectToSave SaveObject { get { return _saveObject; } }

    private string _path;


    private void Save()
    {
        byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(_saveObject));
        string hex = BitConverter.ToString(bytes);
        File.WriteAllText(_path, hex.Replace("-", ""));
        //File.WriteAllText(path, JsonUtility.ToJson(obj));
    }

    private void Load()
    {
        string _loadedData = null;

#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        _path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(_path))
        {
            _loadedData = File.ReadAllText(_path);
            int charsCount = _loadedData.Length;
            byte[] bytes = new byte[charsCount / 2];

            for (int i = 0; i < charsCount; i += 2) bytes[i / 2] = Convert.ToByte(_loadedData.Substring(i, 2), 16);
            _loadedData = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            _saveObject = JsonUtility.FromJson<ObjectToSave>(_loadedData);


        }
    }



    private void OnEnable()
    {
        if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Load();
    }


#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            //Save code 
            Save();
        }
    }
#else
    private void OnApplicationQuit()
    {
        Save();
    }

#endif

}
