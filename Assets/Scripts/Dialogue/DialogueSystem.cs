using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//controla o texto que vai ser lido pra tela e, futuramente, como ele vai ser lido dos arquivos
namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {

        public DialogueBox dialogueBox = new DialogueBox();
        private ConversationManager conversationManager = new ConversationManager();

        //garante que tem apenas uma instancia desse objeto (singleton)
        public static DialogueSystem instance;

        public bool isRunningConversation => conversationManager.isRunning;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}