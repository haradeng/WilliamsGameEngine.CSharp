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
    private int _health = 10;
    private bool _movingDown = true;
    private readonly Sprite _sprite = new Sprite();

    
private int _explosionTimer = 0;
private readonly string[] _damageTextures = {
    "Resources/ENEMYSHIP.PNG",
    "Resources/ENEMYSHIP_HIT 1.png",
    "Resources/ENEMYSHIP_HIT 2.png",
    "Resources/ENEMYSHIP_HIT 3.png",
};


    public Enemy(Vector2f pos)
    {
        _sprite.Texture = Game.GetTexture("Resources/ENEMYSHIP.PNG");
        _sprite.Position = pos;
        _sprite.Scale = new Vector2f(6.5f, 6.5f);
        AssignTag("enemy");
        SetCollisionCheckEnabled(true);
    }

    public override FloatRect GetCollisionRect()
    {
        return _sprite.GetGlobalBounds();
    }

  public override void HandleCollision(GameObject otherGameObject)
{
      //  Console.WriteLine("my enemy ship = " + otherGameObject.Tag);
    if (otherGameObject.HasTag("laser"))
    {
        GameScene scene = (GameScene)Game.CurrentScene;
        scene.DecreaseLives();
    }
}

    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }

    public FloatRect GetBounds()
    {
        return _sprite.GetGlobalBounds();
    }

/*     public void TakeHit()
    {
        _health--;
        if (_health <= 0)
        {
            ((GameScene)Game.CurrentScene).IncreaseScore();
            MakeDead();
        }
    } */
     

public void TakeHit()
{
    _health--;
    
    int textureIndex = _damageTextures.Length - (_health * _damageTextures.Length / 10);
    textureIndex = Math.Clamp(textureIndex, 0, _damageTextures.Length - 1);
    _sprite.Texture = Game.GetTexture(_damageTextures[textureIndex]);

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


           if (_health <= 7)
    {
        _explosionTimer -= msElapsed;
        if (_explosionTimer <= 0)
        {
            _explosionTimer = 150;
            Vector2f epos = _sprite.Position;
            pos.X += _sprite.GetGlobalBounds().Width / 2;
            pos.Y += _sprite.GetGlobalBounds().Height / 2;
            var explosion = new Explosion(pos);
            Game.CurrentScene.AddGameObject(explosion);
        }
    }
}




        
    }

    


