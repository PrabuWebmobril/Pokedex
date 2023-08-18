using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private PokemonCard _pokemonCard;
    [SerializeField] private Transform _cardSpawnParent;

    private int _cardCount;

    private CardsData _cardsData;

    void Start()
    {
        var jsonTextAsset = Resources.Load<TextAsset>("Metadata/PokemonData");
        _cardsData = JsonUtility.FromJson<CardsData>(jsonTextAsset.text);
        if (_cardsData != null) SpawnCards();
    }

    private void SpawnCards()
    {
        foreach (var cardData in _cardsData.pokemonDatas)
        {
            var card = Instantiate(_pokemonCard, _cardSpawnParent);
            card.SetData(cardData);
            if (card.IsFavourite())
            {
                card.transform.SetSiblingIndex(0);
            }
        }

        var firstCard = _cardSpawnParent.GetChild(0).GetComponent<PokemonCard>();
        firstCard.OnCardSelected();
    }
}

[System.Serializable]
public class CardsData
{
    public pokemonDatas[] pokemonDatas;
}

[System.Serializable]
public class pokemonDatas
{
    public int id;
    public string name;
    public int height;
    public int weight;
    public string spriteURL;
    public List<string> abilities;
    public string type;
}