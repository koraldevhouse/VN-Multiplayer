using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// responsavel por armazenar as informacoes ja processadas de uma linha do arquivo
/// </summary>

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public string speaker;
        public string dialogue;
        public string commands;

        public bool hasSpeaker => (speaker != "");
        public bool hasDialogue => (dialogue != "");
        public bool hasCommands => (commands != "");

        //construtor
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
        }
    }
}