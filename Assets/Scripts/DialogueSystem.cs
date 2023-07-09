using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//controla o texto que vai ser lido pra tela e futuramente como ele vai ser lido dos arquivos

public class DialogueSystem : MonoBehaviour
{ 

    public DialogueBox dialogueBox = new DialogueBox();

    //garante que tem apenas uma instancia desse objeto (singleton)
    public static DialogueSystem instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
