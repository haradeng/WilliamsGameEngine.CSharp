using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Enemy : GameObject
{
    private const float Speed = 0.1f;
    private const int FireDelay = 2000;
    private int _fireTimer = 1000;
    private readonly Sprite _sprite = new Sprite();

    public Enemy(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/meteor.png");
        _sprite.Position = pos;
        AssignTag("enemy");
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }

    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;

        // Move downward
        pos.Y += Speed * msElapsed;
        _sprite.Position = pos;

        // Shoot timer
        if (_fireTimer > 0)
            _fireTimer -= msElapsed;

        if (_fireTimer <= 0)
        {
            _fireTimer = FireDelay;
            var shot = new EnemyLaser(_sprite.Position);
            Game.CurrentScene.AddGameObject(shot);
        }

        // Remove if off screen
        if (pos.Y > 600)
            MakeDead();
    }
}

