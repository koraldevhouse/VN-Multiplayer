using System.Collections;
using UnityEngine;
using TMPro;

public class TextPrinter
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world; //permite arrastar o texto tanto da UI quanto do mundo 3D

     //public string currentText => tmpro.text; //atualmente faz altos nada
    public string targetText { get; private set; } = "";
    public string preText { get; private set; } = ""; //pega o texto atual, serve pra adicionar a possibilidade de adicionar (append) ao texto atual ao inves de comecar um novo
    public string fullTargetText => preText + targetText; //preText eh vazio exceto no caso do Append

    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

    //controla como o texto aparece
    //instant: instantaneo
    //typewriter: caractere por caractere
    //fade: nao implementado
    public enum BuildMethod { instant, typewriter, fade }
    public BuildMethod buildMethod = BuildMethod.typewriter;


    //controla a velocidade que o texto aparece
    public float txtSpeed { get { return baseTxtSpeed * txtSpeedMultiplier; } set { txtSpeedMultiplier = value; } }
    private const float baseTxtSpeed = 1f;
    private float txtSpeedMultiplier = 1f;
    //permite imprimir mais de um caractere por frame 
    public int charactersPerCycle { get { return txtSpeed <= 2f ? characterMultiplier : txtSpeed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }
    private int characterMultiplier = 1;
    //bool CELERA = false; //a ideia era acelerar o texto quando clica, e completar se clicar 2 vezes, mas acho melhor completar o dialogo direto logo


    //construtores
    public TextPrinter(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }
    public TextPrinter(TextMeshPro tmpro_world)
    {
        this.tmpro_world = tmpro_world;
    }

    //apaga o texto atual e inicia o targetText
    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }
    //mantem o texto atual e adiciona o targetText no final
    public Coroutine Append(string text)
    {
        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void Stop()
    {
        if (!isBuilding)
            return;
        tmpro.StopCoroutine(buildProcess);  //TextPrinter nao eh monobehavior entao nao pode ter co-rotinas
        buildProcess = null;                //entao a co-rotina eh associada ao objeto de text mesh pro, que é monobehavior
    }

    IEnumerator Building()
    {
        Prepare();

        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }

        OnComplete();
    }


    //prepara pro metodo de impressao q vai ser usado (instant, typewriter, fade)
    private void Prepare()
    {
        switch (buildMethod)
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    //codigo que roda na finalizacao do build process
    private void OnComplete()
    {
        buildProcess = null;
        //CELERA = false;
    }

    public void ForceComplete()
    {
        switch (buildMethod)
        {
            case BuildMethod.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                break;
        }
    }

    //definicoes do preparo do metodo de impressao
    private void Prepare_Instant()
    {
        tmpro.color = tmpro.color; //reinicia a cor original caso tenha mudado (por ex o fade mudando o alpha)
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate(); //aplica as mudanças no texto
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }
    private void Prepare_Typewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount; //length do preText
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }
    private void Prepare_Fade()
    {

    }

    //implementacao que faz o texto aparecer letra por letra
    private IEnumerator Build_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += /*CELERA ? charactersPerCycle*5 :*/ charactersPerCycle;
            yield return new WaitForSeconds(0.015f / txtSpeed);
        }
        
    }
    private IEnumerator Build_Fade()
    {
        yield return null;
    }

}

