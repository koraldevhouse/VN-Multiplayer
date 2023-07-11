using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A caixa que segura o texto do nome na tela. Parte do dialogueBox
/// </summary>

public class NameContainer : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI nameText;

    public void Show(string nameToShow = "")
    {
        root.SetActive(true);

        if (nameToShow != "")
            nameText.text = nameToShow;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
