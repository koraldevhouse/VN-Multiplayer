using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//responsavel por armazenar as informacoes ja processadas de uma linha do arquivo

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public string speaker;
        public string dialogue;
        public string commands;

        //construtor
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
        }
    }
}