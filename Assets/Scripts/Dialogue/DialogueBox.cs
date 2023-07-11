using UnityEngine;
using TMPro;

//classe que agrega os componentes da caixa de dialogo

namespace DIALOGUE
{
    [System.Serializable]
    public class DialogueBox
    {
        public GameObject root; //raiz pra desabilitar o dialogo inteiro
        public TextMeshProUGUI speaker;
        public TextMeshProUGUI dialogueText;
        //precisa implementar um sprite se pa
    }
}