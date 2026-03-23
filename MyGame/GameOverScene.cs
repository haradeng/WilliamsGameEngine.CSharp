using System;
using GameEngine;

namespace MyGame;

public class GameOverScene :Scene
{
    public GameOverScene(int score)
    {
        GameOverMessage gameOverMessage= new GameOverMessage(score);
        AddGameObject(gameOverMessage);
    }
}
