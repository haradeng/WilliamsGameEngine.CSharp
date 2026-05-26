using System;
using GameEngine;
using SFML.System;

namespace MyGame;

public class EnemySpawner : GameObject
{
    private bool _firstSpawned = false;
    private bool _secondSpawned = false;

    public override void Update(Time elapsed)
    {
        // spawn first enemy
        if (!_firstSpawned)
        {
            _firstSpawned = true;
            Enemy enemy = new Enemy(new Vector2f(650, 100));
            Game.CurrentScene.AddGameObject(enemy);
        }

        // once first is dead spawn second
        if (_firstSpawned && !_secondSpawned && Enemy.EnemyCount == 0)
        {
            _secondSpawned = true;
            Enemy2 enemy2 = new Enemy2(new Vector2f(650, 100));
            Game.CurrentScene.AddGameObject(enemy2);
        }
    }
}
