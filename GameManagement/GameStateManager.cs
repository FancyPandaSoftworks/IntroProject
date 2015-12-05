using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


    public class GameStateManager : Root
    {
        Dictionary<string, Root> gameState;
        Root currentGameState;

        public GameStateManager()
        {
            gameState = new Dictionary<string, Root>();
            currentGameState = null;
        }

        public void AddGameState(string name, Root state)
        {
            gameState[name] = state;
        }

        public Root GetGameState(string name)
        {
            return gameState[name];
        }

        public void SwitchTo(string name)
        {
            if (gameState.ContainsKey(name))
                currentGameState = gameState[name];
            else
                throw new KeyNotFoundException("Can't find gamestate" + name);
        }

        public void HandleInput(InputHelper inputhelper)
        {
            if (currentGameState != null)
                currentGameState.HandleInput(inputhelper);
        }

        public void Update(GameTime gametime)
        {
            if (currentGameState != null)
                currentGameState.Update(gametime);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (currentGameState != null)
                currentGameState.Draw(gameTime, spriteBatch);
        }

        public void Reset()
        {
            if (currentGameState != null)
                currentGameState.Reset();
        }

    }
