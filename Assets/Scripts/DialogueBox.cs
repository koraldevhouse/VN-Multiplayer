using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueBox
{
    public GameObject root; //raiz pra desabilitar o dialogo inteiro
    public TextMeshProUGUI speaker; //mostra quem tá falando, talvez vire uma imagem
    public TextMeshProUGUI dialogueText;
}
