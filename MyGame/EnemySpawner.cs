using System;
using GameEngine;
using SFML.System;

namespace MyGame;

public class EnemySpawner : GameObject
{
    private bool _spawned = false;

    public override void Update(Time elapsed)
    {
        if (!_spawned)
        {
            _spawned = true;
            Enemy enemy = new Enemy(new Vector2f(700, 100));
            Game.CurrentScene.AddGameObject(enemy);
        }
    }
}

