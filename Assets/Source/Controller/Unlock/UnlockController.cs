using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockController : ControllerBaseModel
{
    public override void Initialize()
    {
        base.Initialize();
        SetUnlocks();
    }

    private void SetUnlocks()
    {
        var currentLevel = UserPrefs.GetCurrentLevel();
        var sewingMachines = LevelController.ActiveLevel.sewingArea.sewingMachines;
        for (int i = 0; i < sewingMachines.Count; i++)
        {
            sewingMachines[i].CheckUnlockable(currentLevel);
        }

        var paintCauldrons = LevelController.ActiveLevel.paintingArea.paintCauldrons;
        for (int i = 0; i < paintCauldrons.Count; i++)
        {
            paintCauldrons[i].CheckUnlockable(currentLevel);
        }
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        EventController.OnLevelCompleted += SetUnlocks;
    }

    private void OnDisable()
    {
        EventController.OnLevelCompleted -= SetUnlocks;
    }

    #endregion
}

public enum LockState
{
    Locked,
    Unlockable,
    Unlocked
}