using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace DIALOGUE
{
    [System.Serializable]
    /// <summary>
    /// A caixa que segura o texto do nome na tela. Parte do dialogueContainer
    /// </summary>
    public class NameContainer
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI nameText;
        //[SerializeField] private Image speakerImage;

        public void Show(string nameToShow = "")
        {
            root.SetActive(true);

            if (nameToShow != string.Empty)
                nameText.text = nameToShow;
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}