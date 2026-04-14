using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Background : GameObject
{
    private Sprite _sprite1;
    private Sprite _sprite2;
    private float _scrollingSpeed = 1.0f;
    private float _screenHeight;

public Background()
    {
        _screenHeight = 600;
        _sprite1 = new Sprite();
        _sprite2 = new Sprite();
        _sprite1.Texture = Game.GetTexture("Resources/background.png");
        _sprite2.Texture = Game.GetTexture("Resources/background.png");

        _sprite1.Position = new Vector2f(0, 0);
        _sprite2.Position = new Vector2f(0,-_screenHeight);
    }
    public override void Draw()
    {
        Game.RenderWindow.Draw(_sprite1);
        Game.RenderWindow.Draw(_sprite2);
    }
    public override void Update(Time elapsed)
    {
        int msElapsed = elapsed.AsMilliseconds();
        _sprite1.Position = new Vector2f(_sprite1.Position.X + _scrollingSpeed * msElapsed, _sprite1.Position.Y );
        _sprite2.Position = new Vector2f(_sprite2.Position.X + _scrollingSpeed * msElapsed, _sprite2.Position.Y);

        float screenWidth = 800f;

        if (_sprite1.Position.X >= screenWidth)
            _sprite1.Position = new Vector2f(_sprite2.Position.X - screenWidth, 0);

        if (_sprite2.Position.X >= screenWidth)
            _sprite2.Position = new Vector2f(_sprite1.Position.X - screenWidth, 0);

    }
}