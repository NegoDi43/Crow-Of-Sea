using System;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    // Posição do jogador
    public float posX, posY, posZ;

    // Status básicos
    [SerializeField] public float vidaMaxima;
    [SerializeField] public float vidaAtual;
    [SerializeField] public float danoMaximo;
    [SerializeField] public float velocidade;
    [SerializeField] public float staminaMax;
    [SerializeField] public float staminaAtual;
    [SerializeField] public float pontos;

    // Pontos de atributo
    [SerializeField] public float pontosVida;
    [SerializeField] public float pontosDano;
    [SerializeField] public float pontosVelocidade;
    [SerializeField] public float pontosStamina;

    // XP e nível
    [SerializeField] public int xpAtual;
    [SerializeField] public int xpNecessario;
    [SerializeField] public int levelAtual;

    public PlayerSaveData(Vector3 pos, Status status, GanhodeXp xp)
    {
        posX = pos.x;
        posY = pos.y;
        posZ = pos.z;

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

        // Aqui você pode acessar levelAtual direto do script
        var levelField = typeof(GanhodeXp).GetField("levelAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        levelAtual = (int)levelField.GetValue(xp);
    }

    public Vector3 GetPosition() => new Vector3(posX, posY, posZ);

}
