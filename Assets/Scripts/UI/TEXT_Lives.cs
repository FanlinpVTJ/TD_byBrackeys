using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: ааа капс убери и _
// глазаааа
// почему не LivesText?

// Вообще ты мог просто объеденить Lives, Money и WaveIndex в один класс, описывающий экран
// это называется (что-нибудь)View или (что-нибудь)Screen
// типа отображение статов игрока
// PlayerStatsView
// PlayerStatsScreen
// короче оно бы всё вместе обновляло просто и всё, три класса тут это арпеджио (перебор)

// опять же если б были ивенты на то, что поменялось кол-во жизней, поменялось кол-во денег и поменялся индекс волны
// можно было бы подпиаться на ивенты и выполнять это всё не в апдейте, а 1 раз, когда оно меняется
// хер с ним можешь пока сделать статические ивенты, если не забудешь от них отписываться
public class TEXT_Lives : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentPlayerLives;

    // TODO: я знаю, что делает апдейт, все знают, правда, я тебе гарантирую)
    // TODO: private забыл
    // Update is called once per frame
    void Update()
    {
        _currentPlayerLives.text = PlayerStats._liveCount.ToString();
    }
}
