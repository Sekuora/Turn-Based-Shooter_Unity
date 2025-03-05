using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TurnSystem : MonoBehaviour
{
    [SerializeField]
    private TurnSystem turnSystem;

    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private TextMeshProUGUI turnNumberText;

    [SerializeField]
    private GameObject enemyTurnBanner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        turnSystem.OnEndTurnButtonTriggered += OnEndTurnButtonTriggered_Event;

        turnNumberText.text = "Turn Number: " + turnSystem.TurnNumber;

        UpdateEnemyTurnBanner();

        UpdateEndTurnButtonActive();
    }

    private void OnEndTurnButtonTriggered_Event(object sender, EventArgs e)
    {
        turnNumberText.text = "Turn Number: " + turnSystem.TurnNumber;

        UpdateEnemyTurnBanner();

        UpdateEndTurnButtonActive();

    }


    private void UpdateEnemyTurnBanner()
    {
        enemyTurnBanner.SetActive(!turnSystem.IsPlayerTurn);
    }

    private void UpdateEndTurnButtonActive()
    {
        endTurnButton.gameObject.SetActive(turnSystem.IsPlayerTurn);
    }
}
