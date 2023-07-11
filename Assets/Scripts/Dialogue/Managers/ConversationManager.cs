using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lida com toda a logica para passar o dialogo na tela, uma linha de cada vez
/// </summary>

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
            for (int i = 0; i < conversation.Count; i++) //count esta para list   assim como   lenght esta par string
            {
                if ( string.IsNullOrWhiteSpace(conversation[i]) )
                    continue; //pula essa iteracao
                
                DIALOGUE_LINE line = DialogueInterpreter.Process(conversation[i]);

                //mostra o dialogo
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);
                    //pra chamar co-rotina dentro de uma co-rotina eh so dar yield ate a outra retornar

                //roda os comandos
                if (line.hasCommands)
                    yield return Line_RunCommands(line);
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            yield return null;
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            yield return null;
        }
    }
}