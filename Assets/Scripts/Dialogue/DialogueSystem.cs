using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace DIALOGUE
{
    /// <summary>
    ///controla o texto que vai ser lido pra tela e, futuramente, como ele vai ser lido dos arquivos
    /// </summary>

    public class DialogueSystem : MonoBehaviour
    {

        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextPrinter printer;

        //garante que tem apenas uma instancia desse objeto (singleton)
        public static DialogueSystem instance;

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);
            

        }

        bool initialized = false;
        private void Initialize()
        {
            if (initialized)
                return;

            printer = new TextPrinter(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(printer);
            //initialized = true;
            return;
        }

        public void OnUserPrompt_Next()
        {
            // a interrogacao eh pra caso seja null nao faca nada e nao gere erros
            onUserPrompt_Next?.Invoke();
        }

        //faz o nameContainer aparecer (e possivelmente updeitar) ou esconder
        public void ShowSpeakerContainer(string speakerName = "")
        {

            //if(speakerName.ToLower() == "player") (futura implementacao caso for atribuir uma imagem ao player)
            if (speakerName != "")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerContainer();
        }
            public void HideSpeakerContainer() => dialogueContainer.nameContainer.Hide();

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