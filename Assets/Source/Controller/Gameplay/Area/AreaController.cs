using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AreaController : Singleton<AreaController>
{
    private readonly ObservedValue<AreaStates> AreaStatus = new ObservedValue<AreaStates>(AreaStates.Sewing);
    private IList<IAreaStateObserver> _areaStateObservers;
    
    public AreaStates currentAreaState;

    public override void Initialize()
    {
        base.Initialize();
        SetAreaState(AreaStates.Sewing);
    }

    [Button]
    public void SetAreaState(AreaStates targetState)
    {
        if (AreaStatus.Value == targetState) return;
        AreaStatus.Value = targetState;
    }

    public void AddListener(IAreaStateObserver areaStateObserver)
    {
        _areaStateObservers ??= new List<IAreaStateObserver>();
        if (!_areaStateObservers.Contains(areaStateObserver))
        {
            _areaStateObservers.Add(areaStateObserver);
        }
    }

    private void OnAreaStateChanged()
    {
        currentAreaState = AreaStatus.Value;
        if (_areaStateObservers == null) return;

        for (int i = 0; i < _areaStateObservers.Count; i++)
        {
            if (_areaStateObservers[i] != null) _areaStateObservers[i].OnAreaStateChanged();
        }
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        AreaStatus.OnValueChange += OnAreaStateChanged;
    }

    private void OnDisable()
    {
        AreaStatus.OnValueChange -= OnAreaStateChanged;
    }

    #endregion
}