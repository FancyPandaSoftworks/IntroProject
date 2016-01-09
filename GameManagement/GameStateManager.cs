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

    /// <summary>
    /// Manages the gameStates and which one is active
    /// </summary>
    public GameStateManager()
    {
        gameState = new Dictionary<string, Root>();
        currentGameState = null;
    }
    
    /// <summary>
    /// Add a gamestate to the list of game states
    /// </summary>
    /// <param name="name">Specify the name to call the state</param>
    /// <param name="state">Give a reference to the state itself</param>
    public void AddGameState(string name, Root state)
    {
        gameState[name] = state;
    }

    /// <summary>
    /// Return a gamestate
    /// </summary>
    /// <param name="name">The name of the game state</param>
    /// <returns>Returns the reference to the game state object that coresponds with the name</returns>
    public Root GetGameState(string name)
    {
        return gameState[name];
    }

    /// <summary>
    /// Switch to a gamestate
    /// </summary>
    /// <param name="name">The name of the game state</param>
    public void SwitchTo(string name)
    {
        if (gameState.ContainsKey(name))
            currentGameState = gameState[name];
        else
            throw new KeyNotFoundException("Can't find gamestate" + name);
    }

    /// <summary>
    /// Handle the input for the game state
    /// </summary>
    /// <param name="inputhelper">The inputhelper to react to input</param>
    public void HandleInput(InputHelper inputHelper)
    {
        if (currentGameState != null)
            currentGameState.HandleInput(inputHelper);
    }

    /// <summary>
    /// Updating the current gamestate
    /// </summary>
    /// <param name="gametime">The object used for reacting to timechanges</param>
    public void Update(GameTime gametime)
    {
        if (currentGameState != null)
            currentGameState.Update(gametime);

    }

    /// <summary>
    /// Drawing the current gamestate
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
            currentGameState.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Reset the current gamestate
    /// </summary>
    public void Reset()
    {
        if (currentGameState != null)
            currentGameState.Reset();
    }
}
