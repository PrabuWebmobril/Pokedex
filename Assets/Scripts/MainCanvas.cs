using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private RawImage _pokemonImage;

    [SerializeField] private TextMeshProUGUI _pokemonName;

    [SerializeField] private PokemonStats _pokemonStats;

    [SerializeField] private Button _heartButton;
    [SerializeField] private Button _camerButton;
    [SerializeField] private Button _camerModeQuitButton;

    [SerializeField] private GameObject _lowerCanvas;
    [SerializeField] private bool _clearPlayerPref;
    private PokemonCard selectedCard;

    private void Awake()
    {
        if(_clearPlayerPref) PlayerPrefs.DeleteAll();
    }

    private void OnEnable()
    {
        _heartButton.onClick.AddListener(OnClickHeartButton);
        _camerButton.onClick.AddListener(OnClickCameraButton);
        _camerModeQuitButton.onClick.AddListener(OnClickCameraModeQuitButton);
    }

    

    private void OnDisable()
    {
        _heartButton.onClick.RemoveAllListeners();
        _camerButton.onClick.RemoveAllListeners();
        _camerModeQuitButton.onClick.RemoveAllListeners();
    }
    private void OnClickCameraModeQuitButton()
    {
        _camerModeQuitButton.gameObject.SetActive(false);
        HandleUiInteractions(true);
    }

    private void OnClickHeartButton()
    {
        selectedCard.SetFavourite();
    }
    private void OnClickCameraButton()
    {
        HandleUiInteractions(false);
        _camerModeQuitButton.gameObject.SetActive(true);
    }

    void HandleUiInteractions(bool state)
    {
        _lowerCanvas.SetActive(state);
        _pokemonStats.gameObject.SetActive(state);
        _heartButton.gameObject.SetActive(state);
        _camerButton.gameObject.SetActive(state);
        _pokemonName.gameObject.SetActive(state);
    }
    public void OnCardSelected(pokemonDatas pokemonData, Texture pokemonImage, PokemonCard pokemonCard)
    {
        if(selectedCard != null)selectedCard.OnOtherButtonSelected();
        _pokemonImage.texture = pokemonImage;
        _pokemonName.text = pokemonData.name;
        _pokemonStats.SetStats(pokemonData.height ,pokemonData.weight , pokemonData.abilities , pokemonData.type );
        selectedCard = pokemonCard;
    }
}
