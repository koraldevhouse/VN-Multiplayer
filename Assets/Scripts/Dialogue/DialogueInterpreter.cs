using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



// a estrutura da linha eh:      nome {outros parametros}      "fala"      funcoes()
// 
// pode ter so nome e fala, ou so funcoes, mas de toda forma o importante eh que as aspas servem como separacao entre as partes
//
// nome: pode ter (opcional)         as apelido             at x           at x:y               apelido eh o nome que aparece, x eh a posicao horizontal e y a vertical
//
// fala:            fala normal fodase
//
// funcoes: pode ter uma ou mais funcoes, separadas por virgula:           func1(), func2(), func3()

namespace DIALOGUE
{
    /// <summary>
    /// essa classe é responsavel por processar as linhas de texto do arquivo e passar para DIALOGUE_LINE armazenar
    /// </summary>
    public class DialogueInterpreter
    {
        private const string commandRegexPattern = "\\w*[^\\s]\\("; // = \w*    (uma word de qualquer tamanho)
                                                                    // [^\s]    (desde que nao tenha um espaco em branco na frente)
                                                                    // \(       (comeco de parenteses)

        //responsavel por receber e entregar (receber o bruto e entregar o limpinho)
        public static DIALOGUE_LINE Process(string rawLine)
        {
            Debug.Log($"Processando linha: '{rawLine}'");//DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG

            (string speaker, string dialogue, string commands) = SplitContent(rawLine);

            Debug.Log($"Speaker = '{speaker}'\nDialogue = '{dialogue}'\nCommands = '{commands}'");//DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG

            return new DIALOGUE_LINE(speaker, dialogue, commands);
        }

        //pega o bruto e separa em partes
        private static (string, string, string) SplitContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";

            //ints que localizam a posicao da primeira e da ultima aspa "" da fala
            int dialogueStart = -1;
            int dialogueEnd = -1;
            // bool pra ver se teve uma barra \ no ultimo caractere (pra ignorar as aspas "" que fazem parte do dialogo, por exemplo)
            bool isBarrado = false;
            
            //for para identificar dialogueStart e End
            for(int i = 0; i < rawLine.Length; i++)
            {
                char atual = rawLine[i];
                if (atual == '\\') //if current = \
                    isBarrado = !isBarrado; //usar !isBarrado ao inves de true garante que "\\" vira \
                else
                {
                    if (atual == '"' && !isBarrado)
                    {
                        if (dialogueStart == -1)
                            dialogueStart = i;
                        else if (dialogueEnd == -1)
                        {
                            dialogueEnd = i;
                            break; //nao tem pq verificar depois de encontrado dialogueEnd
                        }
                    }
                    else isBarrado = false;
                } 
            }

            // identifica o padrao dos comandos
            Regex commandRegex = new Regex(commandRegexPattern); //regex pega a pattern (palavra+abreParenteses) e procura no texto usando .match
            Match match = commandRegex.Match(rawLine); 
            int commandStart = -1; 
            if (match.Success)
            {
                commandStart = match.Index; //a posicao na string que comeca o comando

                if(dialogueStart == -1)
                    return ("", "", rawLine.Trim());
            }

            //identifica se o texto identificado dentro das aspas eh um dialogo ou um parametro de funcao que tem varias palavras
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {   // eh dialogo
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");
                if(commandStart!=-1)
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
                //eh parametro do comando
                commands = rawLine;
            else //????
                speaker = rawLine;

            return (speaker, dialogue, commands);
        }
    }
}