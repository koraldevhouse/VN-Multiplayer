using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// lida com toda a logica para passar o dialogo na tela, uma linha de cada vez
namespace DIALOGUE
{
    public class ConversationManager
    {
        //referencia ao dialoguesystem pq ele vai rodar as co-rotinas
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
         
        private Coroutine processo = null;
        public bool isRunning => (processo != null);
        
        public void StartConversation(List<string> conversation)
        {
            StopConversation(); //opcional

            processo = dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(processo);
            processo = null;
            return;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {

        }
    }
}