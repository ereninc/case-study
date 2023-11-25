using UnityEngine;
using UnityEngine.UI;

public class AreaButtonController : Singleton<AreaButtonController>, IAreaStateObserver
{
    [SerializeField] private Button paintAreaButton;
    [SerializeField] private Button sewingAreaButton;

    [SerializeField] private RectTransform paintAreaRectTransform;


    public override void Initialize()
    {
        base.Initialize();
        AddToAreaObserverList();
        InitializeButtons();
    }

    public void AddToAreaObserverList()
    {
        AreaController.AddListener(this);
        Debug.Log("AreaButtonController is now AreaStateListener");
    }

    public void OnAreaStateChanged()
    {
        switch (AreaController.currentAreaState)
        {
            case AreaStates.Sewing:
                paintAreaButton.SetActiveGameObject(true);
                sewingAreaButton.SetActiveGameObject(false);
                break;
            case AreaStates.Painting:
                paintAreaButton.SetActiveGameObject(false);
                sewingAreaButton.SetActiveGameObject(true);
                break;
        }
    }

    public RectTransform GetRect()
    {
        return paintAreaRectTransform;
    }

    private void OnProductReached(IDraggable draggable)
    {
        paintAreaButton.transform.PunchScale();
    }

    private void InitializeButtons()
    {
        sewingAreaButton.SetActiveGameObject(false);
        paintAreaButton.SetActiveGameObject(true);
    }

    private void OnClickPaintButton()
    {
        AreaController.SetAreaState(AreaStates.Painting);
        sewingAreaButton.transform.PunchScale();
    }

    private void OnClickSewingButton()
    {
        AreaController.SetAreaState(AreaStates.Sewing);
        paintAreaButton.transform.PunchScale();
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        paintAreaButton.onClick.AddListener(OnClickPaintButton);
        sewingAreaButton.onClick.AddListener(OnClickSewingButton);
        SewingActions.OnProductReached += OnProductReached;
    }

    private void OnDisable()
    {
        paintAreaButton.onClick.RemoveAllListeners();
        sewingAreaButton.onClick.RemoveAllListeners();
        SewingActions.OnProductReached -= OnProductReached;
    }

    #endregion
}