using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//classe pra testar a leituras de arquivos .txt e assets de texto

namespace TESTE
{
    public class TESTE_arquivoTexto : MonoBehaviour
    {
        //private string fileName = "textin";
        [SerializeField] private TextAsset fileName; //otimo pra testar, permite arrastar o arquivo direto no unity

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Run());
        }

        IEnumerator Run()
        {
            List<string> lines = FileManager.ReadTextAsset(fileName, false);

            foreach (string line in lines)
                Debug.Log(line);
            yield return null;
        }
    }
}
