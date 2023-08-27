using System.Collections.Generic;

[System.Serializable]
public class Level
{
    public int id;
    public string[] rows = new string[5];
}

[System.Serializable]
public class ArrayOfLevels
{
    public List<Level> levels = new List<Level>();
}