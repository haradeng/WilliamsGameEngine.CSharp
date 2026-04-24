using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame;

public class Ship : GameObject
{
    private const float Speed = 0.3f;
    private const int FireDelay = 200;
    private int _fireTimer = 0;
    //private float _rotation = 0f;
    //private const float RotationSpeed = 0.1f;
     private bool _rapidFire = false;
     private int _rapidFireTimer = 0;
     private const int RapidFireDuration = 5000; //5 seconds

    private readonly Sprite _sprite = new Sprite();
    //creates ship
    public Ship()
    {
        _sprite.Texture = Game.GetTexture("Resources/ship.png");
        _sprite.Position = new Vector2f(100, 100);
        AssignTag("ship");
        SetCollisionCheckEnabled(true);
    }
    // draws ship 
    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite);
    }
    //updates ship every fram
    public override void Update(Time elapsed)
    {
        Vector2f pos = _sprite.Position;
        float x = pos.X;
        float y = pos.Y;
        int msElapsed = elapsed.AsMilliseconds();

        if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) { y -= Speed * msElapsed; }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) { y += Speed * msElapsed; }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) {x -= Speed * msElapsed; }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) {x += Speed * msElapsed; } 
        if (Keyboard.IsKeyPressed(Keyboard.Key.R))ActivateRapidFire();
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _fireTimer <= 0)
{
    _fireTimer = _rapidFire ? 50 : FireDelay;
    Console.WriteLine("RapidFire: " + _rapidFire + " Timer: " + _fireTimer);
    
}

        

      FloatRect bounds = _sprite.GetGlobalBounds(); 

        // screen size
        float screenWidth = 800;
        float screenHeight = 600;

        // Clamp X
        if (x < 0)
          x = 0;
        if (x + bounds.Width > screenWidth)
            x = screenWidth - bounds.Width;

        // Clamp Y
        if (y < 0)
            y = 0;
        if (y + bounds.Height > screenHeight)
             y = screenHeight - bounds.Height;


        _sprite.Position= new Vector2f(x, y);
        
       // Left arrow spins left, right arrow spins right
       /*  if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            _rotation -= RotationSpeed * msElapsed;

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            _rotation += RotationSpeed * msElapsed;
            _sprite.Rotation = _rotation;
            _sprite.Origin = new Vector2f(
            _sprite.TextureRect.Width / 2f,
            _sprite.TextureRect.Height / 2f
                                        ); */






        if (_fireTimer > 0)
    {
        _fireTimer -= msElapsed;
    }
        if(Keyboard.IsKeyPressed(Keyboard.Key.Space) && _fireTimer <=0)
    {
       // _fireTimer = FireDelay;
       _fireTimer = _rapidFire ? 50 : FireDelay;

        bounds = _sprite.GetGlobalBounds();
        float laserX = x + bounds.Width;
        float laserY = y + bounds.Height / 2.0f;

        Laser laser = new Laser(new Vector2f(laserX, laserY));
        Game.CurrentScene.AddGameObject(laser);
    }
       if (_rapidFire)
        _rapidFireTimer -= msElapsed;
        if (_rapidFireTimer <= 0)
            _rapidFire = false; 
    }
    public void ActivateRapidFire()
{
    _rapidFire = true;
    _rapidFireTimer = RapidFireDuration;
}  
}

