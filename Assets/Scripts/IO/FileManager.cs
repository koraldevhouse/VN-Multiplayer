using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//classe que lida com a leitura das linhas de texto de arquivos .txt ou arquivos textAsset
public class FileManager
{
    //sao estaticas para que so possamos acessar pela propria classe
    public static List<string> ReadTextFile(string filePath, bool includeBlankLines = true) //referenciado como um caminho absoluto
    {
        //checa se eh um caminho absoluto ou relativo
        //absoluto: pode referenciar qualquer pasta em qualquer lugar mesmo fora dos arquivos do jogo
        //relativo: esta contido no "root directory" 
        //se for relativo, transforma em absoluto
        if (!filePath.StartsWith('/'))
            filePath = FilePaths.root + filePath;

        List<string> lines = new List<string>();
        //try...catch serve pra pegar excecoes e fazer algo a respeito (no caso, so informar pra corrigir o codigo)
        //a parte que vai dentro do try{} eh um trecho de codigo normal, mas que a gente sabe que pode ter excecao e sabe que pode fazer algo sobre
        try
        {   
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    //le as linhas do arquivo
                    string line = sr.ReadLine();
                    if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                        lines.Add(line);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"File not found: '{ex.FileName}'");
        }

        return lines;

    }
    //a unity tem suporte a "textAssets" que sao basicamente arquivos de texto .txt interpretados como textAssets pela unity
    //essa versao da ReadTextAsset serve so pra transformar string em asset e passar pra funcao "real"
    public static List<string> ReadTextAsset(string filePath, bool includeBlankLines = true) //referenciado dentro do resources
    {
        TextAsset asset = Resources.Load<TextAsset>(filePath);
        if (asset == null)
        {
            Debug.LogError($"Asset not found: '{filePath}'");
            return null;
        }

        return ReadTextAsset(asset, includeBlankLines); 
    }
    //essa eh a funcao "real" do ReadTextAsset, que le as linhas do arquivo, similar a ReadTextFile
    public static List<string> ReadTextAsset(TextAsset asset, bool includeBlankLines = true)
    {

        List<string> lines = new List<string>();
        using(StringReader sr = new StringReader(asset.text))
        {
            while (sr.Peek() > -1) //basicamente o !sr.EndOfStream, se retornar -1 eh pq nao tem nada na proxima linha (ou seja, eh o fim do arquivo)
            {
                string line = sr.ReadLine();
                if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }
        }
        return lines;
    }
}
