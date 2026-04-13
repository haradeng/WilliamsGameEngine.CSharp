using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GameEngine;
using SFML.Graphics;

namespace MyGame;

public class Background : GameObject
{
    private Sprite _sprite1;
    private Sprite _sprite2;
    private float _scrollingSpeed = 100f;
    private float _screenHeight;

public Background()
    {
        _screenHeight = _screenHeight = 600;
        _sprite1 = new Sprite();
        _sprite2 = new Sprite();
        _sprite1.Texture = 
        _sprite2.Position = ;
        AddSprite(_sprite1);
        AddSprite(_sprite2);
    }
}