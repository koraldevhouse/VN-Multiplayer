using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//garante que nada do teste vai aparecer em outros codigos
namespace TESTE
{
    public class TESTE_TextPrinter : MonoBehaviour
    {

        DialogueSystem ds;
        TextPrinter printer;

        string[] falas = new string[5]
        {
            "Oi, tudo bem?",
            "Me deixa em paz. Eu preciso de um tempo a sós comigo mesmo. Espero que você entenda.",
            "Este texto está saindo da minha boca de livre e espontânea vontade e eu não sou um código programado para imprimir esta sequência de caracteres.",
            "É um pássaro? É um avião? Não, é apenas o espírito que me assombra todos os dias...",
            "Estou com muito frio. Mas o que mais me incomoda, é que está frio por dentro, também..."
        };



        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            printer = new TextPrinter(ds.dialogueBox.dialogueText);
            printer.buildMethod = TextPrinter.BuildMethod.typewriter;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) //ADICIONAR PRA CLIQUE
                printer.Build(falas[Random.Range(0,falas.Length)]);
        }
    }
}