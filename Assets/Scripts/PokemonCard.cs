using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokemonCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private RawImage _pokemonImage;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedOverlay;
    [SerializeField] private GameObject _favouriteImage;
    private MainCanvas _mainCanvas;
    private pokemonDatas myData;
    private Texture _myPokemonImage;
    private bool isFavourite;
    private const string _favouriteString = "IsFavourite";
    private string _currentID;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCardSelected);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        _mainCanvas = transform.root.GetComponent<MainCanvas>();
    }

    public async void OnCardSelected()
    {
        _selectedOverlay.gameObject.SetActive(true);
        while (_myPokemonImage == null)
        {
            await Task.Delay(100);
        }
        _mainCanvas.OnCardSelected(myData , _myPokemonImage , this);
    }

    public void SetFavourite()
    {
        if (isFavourite) return;
        isFavourite = true;
        PlayerPrefs.SetInt(_currentID , Convert.ToInt16(isFavourite));
        _favouriteImage.SetActive(true);
    }

    public bool IsFavourite()
    {
        return isFavourite;
    }

    public void OnOtherButtonSelected()
    {
        _selectedOverlay.gameObject.SetActive(false);
    }
    public void SetData(pokemonDatas pokemonData)
    {
        myData = pokemonData;
        _name.text = myData.name;
        StartCoroutine(DownloadImage(myData.spriteURL));
        _currentID = myData.id.ToString();
        
        isFavourite = Convert.ToBoolean(PlayerPrefs.GetInt(_currentID));
        if(isFavourite) _favouriteImage.SetActive(true);
        // PlayerPrefs.SetInt(_currentID , Convert.ToInt16(isFavourite));

    }
    
    IEnumerator DownloadImage(string MediaUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError) 
            Debug.Log(request.error);
        else
        {
            _pokemonImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            _myPokemonImage = _pokemonImage.texture;
        }
    } 
}
