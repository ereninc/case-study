using TMPro;
using UnityEngine;

public class GameScreen : ScreenElement
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;

    public override void Initialize()
    {
        base.Initialize();
        InitializeCoinText();
        UpdateLevelText();
    }

    private void InitializeCoinText()
    {
        int coinAmount = UserPrefs.GetTotalCollection();
        coinText.text = coinAmount.ToString();
    }

    private void UpdateCoinText()
    {
        coinText.transform.PunchScale();
        coinText.text = UserPrefs.GetTotalCollection().ToString();
    }

    private void UpdateLevelText()
    {
        levelText.transform.PunchScale();
        levelText.text = "Level 1";
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        EventController.OnCoinUpdated += UpdateCoinText;
    }

    private void OnDisable()
    {
        EventController.OnCoinUpdated -= UpdateCoinText;
    }

    #endregion
}