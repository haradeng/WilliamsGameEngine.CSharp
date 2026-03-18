using System;
using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class MeteorSpawner : GameObject
{   
    private const int SpawnDelay = 1000;

    private int _timer;

    public override void Update(Time elapsed)
    {
        // Determine how much time has passed and adjust our timer
        int msElapsed = elapsed.AsMilliseconds();
        _timer -= msElapsed;
        //If our timer has elapsed, reset it and spawn a meteor
        if(_timer<=0)
        {
            _timer = SpawnDelay;
            Vector2u size = Game.RenderWindow.Size;
            // Spawn the meteor off the right side of the screen
            //were assuming the meteor isnt more than 100 pixels wide 
            float meteorx = size.X + 100;
            //spawn the meteor somwhere along teh height of the window
            float meteorY = Game.Random.Next() % size.Y;

            //create a meteor and add it to the scene
            Meteor meteor = new Meteor(new Vector2f(meteorx, meteorY));
            Game.CurrentScene.AddGameObject(meteor);
        }
    }
}
