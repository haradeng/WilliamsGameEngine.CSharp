using GameEngine;
using SFML.System;

namespace MyGame
{
    public class GameScene : Scene
    {
        private int _score = 0;
        private int _lives = 3;
        public GameScene()
        {
             Background background = new Background();
            AddGameObject(background);

            Ship ship = new Ship();
            AddGameObject(ship);
            
            MeteorSpawner meteorSpawner = new MeteorSpawner();
            AddGameObject(meteorSpawner);

            Score score = new Score(new Vector2f(10.0f, 10.0f));
            AddGameObject(score);
            
        }
        //get current code
        public int GetScore()
        {
            return _score;
        }
        //increase the score
        public void IncreaseScore()
        {
            ++_score;
        }
        //get the number of lives
        public int GetLives()
        {
            return _lives;
        }
        //decrease number of lives
        public void DecreaseLives()
        {
            --_lives;
            if(_lives == 0)
            {
                GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);
            }
        }

    }
}