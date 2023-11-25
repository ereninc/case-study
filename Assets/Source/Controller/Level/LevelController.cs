using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : Singleton<LevelController>
{
    [Header("Folders/Prefixes")] private readonly string _levelsFolder = "Levels";
    [ShowInInspector] private int _levelCount = 0;

    [SerializeField] private int loopLevelStartIndex;
    [SerializeField] private List<LevelModel> levels;

    [ShowInInspector] public LevelModel ActiveLevel { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        LoadLevel();
    }

    private void LoadLevel()
    {
        var index = UserPrefs.GetCurrentLevel();
        LevelModel spawnedLevel = Instantiate(levels[index]);
        ActiveLevel = spawnedLevel;
        ActiveLevel.SetActiveGameObject(true);
        // ActiveLevel = levels[PlayerDataModel.Data.LevelIndex];
    }

    public void NextLevel()
    {
        int level = UserPrefs.GetCurrentLevel();
        if (level < _levelCount)
        {
            level++;
            UserPrefs.SetLevel(level);
        }
        else
        {
            level = loopLevelStartIndex;
            UserPrefs.SetLevel(level);
        }
        Timing.CallDelayed(0.5f, () => SceneManager.LoadScene(0));
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