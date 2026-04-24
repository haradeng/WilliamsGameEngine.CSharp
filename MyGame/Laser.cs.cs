using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Laser : GameObject
{
    private const float Speed = 1.2f;
    private readonly Sprite _sprite= new Sprite();
   
    public Laser(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/laser.png");
        _sprite.Position = pos;

        AssignTag("laser");

        SetCollisionCheckEnabled(true);
    }

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }
        public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        Vector2f pos = _sprite.Position;
        if(pos.X > Game.RenderWindow.Size.X)
        {
            MakeDead();
        }
        else
        {
            _sprite.Position = new Vector2f(pos.X + Speed * msElapsed, pos.Y);
        }
    
        
    }
    public override FloatRect GetCollisionRect()
    {
        return _sprite.GetGlobalBounds();
    }
    public override void HandleCollision(GameObject otherGameObject)
{
    if (otherGameObject.HasTag("enemy"))
    {
        ((Enemy)otherGameObject).TakeHit();
        MakeDead();
    }
}

    
}
