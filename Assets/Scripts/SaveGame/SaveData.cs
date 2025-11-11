using System.Collections.Generic;

// [System.Serializable] é crucial para o Unity converter esta classe para JSON.
[System.Serializable]
public class SaveData
{
    // --- Dados do Status.cs ---
    public float vidaMaxima;
    public float vidaAtual;
    public float danoMaximo;
    public float velocidade;
    public float staminaAtual;
    public float staminaMax;
    public float pontos;
    public float pontosVida;
    public float pontosDano;
    public float pontosVelocidade;
    public float pontosStamina;

    // --- Dados do GanhodeXp.cs ---
    public int xpAtual;
    public int xpNecessarioParaNivelUp;
    public int levelAtual;

    // --- Dados do Player (Posição) ---
    public float playerPosX;
    public float playerPosY;

    // --- Dados do Inventario.cs ---
    public List<SerializableSlot> inventorySlots;

    // Construtor padrão para criar novos dados (usado se não houver save)
    public SaveData()
    {
        this.vidaMaxima = 15;
        this.vidaAtual = 15;
        this.danoMaximo = 1;
        this.velocidade = 5;
        this.staminaAtual = 10; // Valor inicial padrão
        this.staminaMax = 10;   // Valor inicial padrão
        this.pontos = 0;
        this.pontosVida = 0;
        this.pontosDano = 0;
        this.pontosVelocidade = 0;
        this.pontosStamina = 0;
        this.xpAtual = 0;
        this.xpNecessarioParaNivelUp = 10;
        this.levelAtual = 0;
        this.playerPosX = 0; // Posição inicial
        this.playerPosY = 0; // Posição inicial
        this.inventorySlots = new List<SerializableSlot>();
    }
}

// Classe auxiliar para salvar o inventário
[System.Serializable]
public class SerializableSlot
{
    public string itemID; // Usaremos o "nomeItem" do seu PrefabsItens
    public int quantidade;

    public SerializableSlot(string id, int qtd)
    {
        this.itemID = id;
        this.quantidade = qtd;
    }
}