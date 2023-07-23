using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DIALOGUE
{
    /// <summary>
    /// lida com toda a logica para passar o dialogo na tela, uma linha de cada vez
    /// </summary>
    public class ConversationManager
    {
        //referencia ao dialoguesystem pq ele vai rodar as co-rotinas
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
         
        private Coroutine processo = null;
        public bool isRunning => (processo != null);

        private TextPrinter printer = null;
        private bool userPrompt = false;

        //construtor
        public ConversationManager(TextPrinter printer)
        {
            this.printer = printer;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }
        
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
            // mostra ou esconde o conteiner do speaker dependendo se tiver um ou nao
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerContainer(line.speaker);
            else dialogueSystem.HideSpeakerContainer();

            //constroi o dialogo
            yield return BuildDialogue(line.dialogue);

            //espera um input do player
            yield return WaitForPlayerInput();
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);

            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            printer.Build(dialogue);

            while (printer.isBuilding)
            {
                if (userPrompt)
                {
                    printer.ForceComplete();
                    userPrompt = false;
                }
                yield return null;
            }
            
        }

        IEnumerator WaitForPlayerInput()
        {
            while (!userPrompt)
                yield return null;

            userPrompt = false;
        }
    }
}