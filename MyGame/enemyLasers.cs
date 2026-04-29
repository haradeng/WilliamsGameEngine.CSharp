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
        _sprite.Texture = Game.GetTexture("Resources/enemylasers.png");
        _sprite.Position = pos;
        // _sprite.Scale = new Vector2f(6.5f, 6.5f);
        AssignTag("enemylaser");
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

        // Moves downward toward player
        pos.X -= Speed * msElapsed;
        _sprite.Position = pos;

        // Remove if off screen
        if (pos.X < 0)
            MakeDead();
    }

    public override FloatRect GetCollisionRect()
{
    return _sprite.GetGlobalBounds();
}

public override void HandleCollision(GameObject otherGameObject)
{
  //  Console.WriteLine("EnemyLaser hit: " + otherGameObject.Tag);
    if (otherGameObject.HasTag("ship"))
    {
        GameScene scene = (GameScene)Game.CurrentScene;
        scene.DecreaseLives();
        Explosion explosion = new Explosion(new Vector2f(
            otherGameObject.GetCollisionRect().Left,
            otherGameObject.GetCollisionRect().Top));
        Game.CurrentScene.AddGameObject(explosion);
        MakeDead();
    }
    // Ignore collisions with player lasers
    if (otherGameObject.HasTag("laser"))
    {
        return;
    }
}


}

