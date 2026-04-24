using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Enemy : GameObject
{
    private const float Speed = 0.2f;
    private const int FireDelay = 1500;
    private int _fireTimer = 1000;
    private int _health = 3;
    private bool _movingDown = true;
    private readonly Sprite _sprite = new Sprite();

    public Enemy(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/meteor.png");
        _sprite.Position = pos;
        AssignTag("enemy");
        SetCollisionCheckEnabled(true);
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }

    public FloatRect GetBounds()
    {
        return _sprite.GetGlobalBounds();
    }

    public void TakeHit()
    {
        _health--;
        if (_health <= 0)
        {
            ((GameScene)Game.CurrentScene).IncreaseScore();
            MakeDead();
        }
    }

    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;

        // Move up and down on right side
        if (_movingDown)
        {
            pos.Y += Speed * msElapsed;
            if (pos.Y >= 500) _movingDown = false;
        }
        else
        {
            pos.Y -= Speed * msElapsed;
            if (pos.Y <= 50) _movingDown = true;
        }

        _sprite.Position = pos;

        // Shoot at player
        if (_fireTimer > 0)
            _fireTimer -= msElapsed;

        if (_fireTimer <= 0)
        {
            _fireTimer = FireDelay;
            var shot = new EnemyLaser(_sprite.Position);
            Game.CurrentScene.AddGameObject(shot);
        }
    }
}

