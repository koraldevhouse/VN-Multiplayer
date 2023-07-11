using UnityEngine;

public class FilePaths
{
    public static readonly string root = $"{Application.dataPath}/gameData/";
}

//os resources da unity nao podem mais ser acessados diretamente depois que o jogo builda pq sao compilados junto
//o {Application.dataPath} garante que dê pra pegar os textos tanto no editor da unity quanto na build