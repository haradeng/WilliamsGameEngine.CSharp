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

    private readonly Sprite _sprite = new Sprite();
    //creates ship
    public Ship()
    {
        _sprite.Texture = Game.GetTexture("Resources/ship.png");
        _sprite.Position = new Vector2f(100, 100);
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

        if (_fireTimer > 0)
    {
        _fireTimer -= msElapsed;
    }
        if(Keyboard.IsKeyPressed(Keyboard.Key.Space) && _fireTimer <=0)
    {
        _fireTimer = FireDelay;

        bounds = _sprite.GetGlobalBounds();
        float laserX = x + bounds.Width;
        float laserY = y + bounds.Height / 2.0f;

        Laser laser = new Laser(new Vector2f(laserX, laserY));
        Game.CurrentScene.AddGameObject(laser);
    }

    }
}
