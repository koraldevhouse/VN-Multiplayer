using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTE
{
    public class TESTE_arquivosDialogo : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartConversation();
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset("testin", false);

            DialogueSystem.instance.Say(lines);
        }
    }
}