using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTE
{
    public class TESTE_Interpreter : MonoBehaviour
    {
        //teste arrastando o arquivo na unity
        //[SerializeField] private TextAsset file;
        
        // Start is called before the first frame update
        void Start()
        {
            //teste com a string pura
            //string line = "Speaker \"Dialogo vem \\\"aqui!\\\" \" codigo(vem aqui)";
            //DialogueInterpreter.Process(line);

            SendFileToInterpreter();
        }

        void SendFileToInterpreter()
        {
            List<string> lines = FileManager.ReadTextAsset("testin", false);
        
            foreach(string line in lines)
            {
                DIALOGUE_LINE dl = DialogueInterpreter.Process(line);
            }

        }
    }
}