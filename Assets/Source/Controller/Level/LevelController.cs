using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : Singleton<LevelController>
{
    [Header("Folders/Prefixes")] 
    private readonly string _levelsFolder = "Levels";
    [ShowInInspector] private int _levelCount = 0;

    [SerializeField] private List<LevelModel> levels;

    [ShowInInspector]public LevelModel ActiveLevel { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        LoadLevel();
    }

    private void LoadLevel()
    {
        var index = 0;
        LevelModel spawnedLevel = Instantiate(levels[0]);
        ActiveLevel = spawnedLevel;
        ActiveLevel.SetActiveGameObject(true);
        // ActiveLevel = levels[PlayerDataModel.Data.LevelIndex];
    }

    public void NextLevel()
    {
        PlayerDataModel.Data.Level++;
        PlayerDataModel.Data.LevelIndex = PlayerDataModel.Data.LevelIndex + 1 < levels.Count
            ? PlayerDataModel.Data.LevelIndex + 1
            : 0;
        PlayerDataModel.Data.Save();
    }

    [Button]
    public void Editor_GetAllLevels()
    {
        levels.Clear();
        Object[] levelPrefabs = Resources.LoadAll<GameObject>(_levelsFolder);
        foreach (var obj in levelPrefabs)
        {
            GameObject levelPrefab = (GameObject)obj;
            LevelModel levelModel = levelPrefab.GetComponent<LevelModel>();

            if (levelModel != null)
            {
                levels.Add(levelModel);
            }
        }
        _levelCount = levels.Count;
    }
}