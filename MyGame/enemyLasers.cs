using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class EnemyLaser : GameObject
{
    private const float Speed = 0.5f;
    private readonly Sprite _sprite = new Sprite();

    public EnemyLaser(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/laser.png");
        _sprite.Position = pos;
        AssignTag("enemylaser");
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }

    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;

        // Moves downward toward player
        pos.Y += Speed * msElapsed;
        _sprite.Position = pos;

        // Remove if off screen
        if (pos.Y > 600)
            MakeDead();
    }
}

