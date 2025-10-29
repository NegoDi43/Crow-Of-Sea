using UnityEngine;
using System;
using System.IO;
using System.Text;

public static class EconomiaSave
{
    private static readonly string caminho = Application.persistentDataPath + "/eco.dat";

    public static void Salvar(int moedas)
    {
        string dados = moedas.ToString();
        string codificado = Convert.ToBase64String(Encoding.UTF8.GetBytes(dados));
        File.WriteAllText(caminho, codificado);
    }

    public static int Carregar()
    {
        if (!File.Exists(caminho)) return 0;
        string codificado = File.ReadAllText(caminho);
        string decodificado = Encoding.UTF8.GetString(Convert.FromBase64String(codificado));
        return int.Parse(decodificado);
    }
}
