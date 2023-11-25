using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProductStateController : ControllerBaseModel
{
    [ShowInInspector] private ProductState _currentState;

    private void ChangeState(ProductState state)
    {
        _currentState = state;
    }

    public ProductState GetState()
    {
        return _currentState;
    }

    public void SetProduction()
    {
        ChangeState(ProductState.Production);
    }

    public void SetSewed()
    {
        ChangeState(ProductState.Sewed);
    }

    public void SetPainted()
    {
        ChangeState(ProductState.Painted);
    }

    public void SetSold()
    {
        ChangeState(ProductState.Sold);
    }
}

public enum ProductState
{
    Production,
    Sewed,
    Painted,
    Sold
}