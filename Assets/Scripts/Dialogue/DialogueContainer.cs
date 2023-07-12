using UnityEngine;
using TMPro;

//classe que agrega os componentes da caixa de dialogo

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueContainer
    {
        public GameObject root; //raiz pra desabilitar o dialogo inteiro
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogueText;
    }
}