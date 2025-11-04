using System;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    // Posição
    public float posX, posY, posZ;

    // Nome da cena (fase atual)
    public string sceneName;

    // Status
    public float vidaMaxima;
    public float vidaAtual;
    public float danoMaximo;
    public float velocidade;
    public float staminaMax;
    public float staminaAtual;
    public float pontos;
    public float pontosVida;
    public float pontosDano;
    public float pontosVelocidade;
    public float pontosStamina;

    // XP
    public int xpAtual;
    public int xpNecessario;
    public int levelAtual;

    public PlayerSaveData(Vector3 pos, string currentScene, Status status, GanhodeXp xp)
    {
        posX = pos.x;
        posY = pos.y;
        posZ = pos.z;
        sceneName = currentScene;

        vidaMaxima = status.GetVidaMaxima();
        vidaAtual = status.GetVidaAtual();
        danoMaximo = status.GetDanoMaximo();
        velocidade = status.GetVelocidade();
        staminaMax = status.GetStaminaMax();
        staminaAtual = status.GetStaminaAtual();
        pontos = status.GetPontos();
        pontosVida = status.GetPontosVida();
        pontosDano = status.GetPontosDano();
        pontosVelocidade = status.GetPontosVelocidade();
        pontosStamina = status.GetPontosStamina();

        xpAtual = xp.GetXpAtual();
        xpNecessario = xp.GetXpNecessarioParaNivelUp();

        var levelField = typeof(GanhodeXp).GetField("levelAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        levelAtual = (int)levelField.GetValue(xp);
    }

    public Vector3 GetPosition() => new Vector3(posX, posY, posZ);
}
