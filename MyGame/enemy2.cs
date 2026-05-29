using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Enemy2 : GameObject
{
    private const float Speed = 0.3f;
    private const int FireDelay = 800;
    private int _fireTimer = 0;
    private int _health = 50;
    private int _explosionTimer = 0;
    private bool _movingDown = true;
    private readonly Sprite _sprite = new Sprite();
    private readonly string[] _damageTextures = {
        "Resources/enemy 2.png",
        "Resources/enemy 2 HITT1.png",
        "Resources/enemy 2 HITT2.png",
        "Resources/enemy 2 HITT3.png",
    };

    public Enemy2(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/enemy 2.png");
        _sprite.Position = pos;
        _sprite.Scale = new Vector2f(7f, 7f);
        AssignTag("enemy");
        SetCollisionCheckEnabled(true);
        Enemy.EnemyCount++;
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }

    public override FloatRect GetCollisionRect()
    {
        return _sprite.GetGlobalBounds();
    }

    public override void HandleCollision(GameObject otherGameObject)
    {
        if (otherGameObject.HasTag("laser"))
        {
            TakeHit();
        }
    }

    public void TakeHit()
    {
        _health--;

        Console.WriteLine("Enemy2 health: " + _health);

      //  int textureIndex = _damageTextures.Length - (_health * _damageTextures.Length / 4);

      int textureIndex = _health switch
    {
        >= 37 => 0,
        >= 25 => 1,
        >= 12 => 2,
    _   => 3
    };


        textureIndex = Math.Clamp(textureIndex, 0, _damageTextures.Length - 1);
        _sprite.Texture = Game.GetTexture(_damageTextures[textureIndex]);

        if (_health <= 0)
        {
            Enemy.EnemyCount--;
            ((GameScene)Game.CurrentScene).IncreaseScore();
            MakeDead();
        }
    }

    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;

        // movement
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

        // shooting
        if (_fireTimer > 0)
            _fireTimer -= msElapsed;

        if (_fireTimer <= 0)
        {
            _fireTimer = FireDelay;
            var shot = new EnemyLaser(_sprite.Position);
            Game.CurrentScene.AddGameObject(shot);
        }

        // explosion effect
        if (_health <= 25)
        {
            _explosionTimer -= msElapsed;
            if (_explosionTimer <= 15)
            {
                _explosionTimer = 100;
                Vector2f ePos = _sprite.Position;
                ePos.X += _sprite.GetGlobalBounds().Width / 2;
                ePos.Y += _sprite.GetGlobalBounds().Height / 2;
                var explosion = new Explosion(ePos);
                Game.CurrentScene.AddGameObject(explosion);
            }
        }
    }
}

