using System;
using System.Numerics;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Meteor : GameObject
{
    private const float Speed = 0.5f;
    private readonly Sprite _sprite = new Sprite();
    public Meteor(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/meteor.png");
        _sprite.Position = pos;

        AssignTag("meteor");
        SetCollisionCheckEnabled(true);
    }

    public override FloatRect GetCollisionRect()
    {
        return _sprite.GetGlobalBounds();
    }
    public override void HandleCollision(GameObject otherGameObject)
    {
        if (otherGameObject.HasTag("laser"))
        {
            otherGameObject.MakeDead();
        }
        MakeDead();
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }
    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;

        if(pos.X < _sprite.GetGlobalBounds().Width * -1)
    {
        MakeDead();
    }
        else
    {
        _sprite.Position = new Vector2f(pos.X - Speed * msElapsed, pos.Y);
    }

    }

}
