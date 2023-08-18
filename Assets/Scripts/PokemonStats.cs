using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PokemonStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statsText;
    public void SetStats(int pokemonDataHeight, int pokemonDataWeight, List<string> pokemonDataAbilities, string pokemonDataType)
    {
        var abilityText = new StringBuilder();
        foreach (var abilities in pokemonDataAbilities)
        {
            abilityText.Append(abilities);
            abilityText.Append("<br>"+"<space=4.3em>");

        }
        _statsText.text = "Height : " +"<space=0.3em>"+ pokemonDataHeight +
                          "<br>"+  "Weight : " +"<space=0.1em>" + pokemonDataWeight +
                          "<br>"+"Type : " +"<space=1.2em>"+ pokemonDataType +
                          "<br>"+"Abilities : " + abilityText;
    }
}
