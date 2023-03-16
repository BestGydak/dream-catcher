using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] public GameManager GameManager;
    [SerializeField] public PlayerStateManager Player;

    [Header("HUD")]
    [SerializeField] public TextMeshProUGUI HealthDisplay;
    [SerializeField] public TextMeshProUGUI AmmoDisplay;
    [SerializeField] public TextMeshProUGUI RoundDisplay;
    
    [Header("Shop")]
    [SerializeField] public GameObject ShopCanvas;
    [SerializeField] public TextMeshProUGUI MaxAmmoShopText;
    [SerializeField] public TextMeshProUGUI AmmoShopText;
    [SerializeField] public TextMeshProUGUI MaxHealthShopText;
    [SerializeField] public TextMeshProUGUI HealthShopText;
    [SerializeField] public TextMeshProUGUI CoinDisplay;
    private void Awake() 
    {
        Player.GetComponent<Health>().OnHealthChanged += HandleHealthChange;
        Player.OnAmmoCountChanged += HandleAmmoCountChange;
        Player.OnStateChanged += HandleOpenShop;
        Player.OnCoinsCountChanged += HandleCoinsCountChange;
        GameManager.OnRoundChanged += HandleRoundChange;
        GameManager.OnMaxAmmoPriceChanged += UpdateShopText;
        GameManager.OnMaxHealthPriceChanged += UpdateShopText;
    }
    private void Start() 
    {
        HandleAmmoCountChange(Player);
        HandleHealthChange(Player.GetComponent<Health>());
        HandleRoundChange(GameManager);
        HandleCoinsCountChange(Player);
    }

    private void HandleHealthChange(Health player)
    {
        HealthDisplay.text = String.Format("{0} | {1}", player.CurrentHealth, player.MaxHealth);
    }

    private void HandleAmmoCountChange(PlayerStateManager player)
    {
        AmmoDisplay.text = String.Format("{0} | {1}", player.CurrentAmmo, player.MaxAmmo);
    }

    private void HandleRoundChange(GameManager gameManager)
    {
        RoundDisplay.text = String.Format("{0}", gameManager.CurrentRound);
    }

    private void HandleCoinsCountChange(PlayerStateManager playerStateManager)
    {
        CoinDisplay.text = String.Format("{0}", playerStateManager.Coins);
    }

    private void HandleOpenShop(PlayerStateManager player)
    {
        ShopCanvas.SetActive(player.CurrentState == player.InShopState);
        UpdateShopText(GameManager);
    }

    private void UpdateShopText(GameManager gameManager)
    {
        MaxAmmoShopText.text 
        = String.Format("+{0} Max Ammo Reserve \n Cost: {1} Coins", gameManager.MaxAmmoBonus, gameManager.MaxAmmoPrice);
        MaxHealthShopText.text 
        = String.Format("+{0} Max Health Reserve \n Cost {1} Coins", gameManager.MaxHealthBonus, gameManager.MaxHealthPrice);
        AmmoShopText.text
        = String.Format("+{0} Ammo \n Cost: {1} Coins", gameManager.AmmoBonus, gameManager.AmmoPrice);
        HealthShopText.text
        = String.Format("+{0} Health \n Cost: {1} Coins", gameManager.HealthBonus, gameManager.HealthPrice);
    }
}
