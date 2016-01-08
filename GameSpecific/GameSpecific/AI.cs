using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// The class which takes care of the AI
/// </summary>
public class AI: GameObject
{
    /// <summary>
    /// Innerclass representing the monster in the game
    /// </summary>
    struct Monster
    {
        public Texture2D monsterTexture;
        public VertexBuffer vertexBuffer; //drawing it in the world
        public IndexBuffer indexBuffer; // the order of rendering
    }
    Monster monster;
    Effect effect;
    BasicEffect basicEffect;
    SpriteBatch spriteBatch;

    /// <summary>
    /// Creating the Object through
    /// </summary>
    /// <param name="assetName">The name of the texture</param>
    public AI(string assetName): base("monsterTexture")
    {
        monster.monsterTexture = GameEnvironment.AssetManager.GetSprite("monsterTexture");
        CreateMonster(new Vector3(0f, 0f, 0f));
        effect = GameEnvironment.AssetManager.GetEffect("Albedo");
        effect.CurrentTechnique = effect.Techniques["Albedo"];
        Matrix projection = Matrix.CreateOrthographicOffCenter(0, (float)GameEnvironment.Graphics.Viewport.Width, (float)GameEnvironment.Graphics.Viewport.Height, 0, 0, 1);
        Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
        basicEffect = new BasicEffect(GameEnvironment.Graphics);
        spriteBatch = new SpriteBatch(GameEnvironment.Graphics);             
    }

    /// <summary>
    /// Load the content required
    /// </summary>
    public void LoadContent()
    {
        Level parentLevel = Parent as Level;
        Player levelPlayer = parentLevel.Find("player") as Player;
        basicEffect.EnableDefaultLighting();
        basicEffect.World = Matrix.Identity;
        basicEffect.View = Matrix.CreateLookAt(levelPlayer.Position, levelPlayer.ViewVertex, Vector3.Up);
        basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                Object3D.aspectRatio, 1.0f, 10000.0f);      
    }

    /// <summary>
    /// Puts the texture2D in the 3D world
    /// </summary>
    /// <param name="pos">The starting position of the monster</param>
    private void CreateMonster(Vector3 pos)
    {
        int x = monster.monsterTexture.Width;
        int y = monster.monsterTexture.Height;
        Vector3 upperLeft = new Vector3(-x, y, 0.0f);
        Vector3 upperRight = new Vector3(x, y, 0.0f);
        Vector3 lowerLeft = new Vector3(-x, -y, 0.0f);
        Vector3 lowerRight = new Vector3(x, -y, 0.0f);
 
        //The 4 points of the texture2D in the world
        VertexPositionTexture[] PointsOfTexture = {new VertexPositionTexture(pos + upperLeft,  new Vector2(0.0f, 0.0f)), 
                                                   new VertexPositionTexture(pos + upperRight, new Vector2(1.0f, 0.0f)),  
                                                   new VertexPositionTexture(pos + lowerLeft,  new Vector2(0.0f, 1.0f)),
                                                   new VertexPositionTexture(pos + lowerRight, new Vector2(1.0f, 1.0f)), };
   
            
        monster.vertexBuffer = new VertexBuffer(GameEnvironment.Graphics, typeof(VertexPositionTexture), PointsOfTexture.Length, BufferUsage.WriteOnly);
        monster.vertexBuffer.SetData(PointsOfTexture);
            
        //The order of how it has to be drawn
        short[] indices =
        {
            0, 1, 2, 
            1, 2, 3
        };

        monster.indexBuffer = new IndexBuffer(GameEnvironment.Graphics, IndexElementSize.SixteenBits,indices.Length, BufferUsage.WriteOnly);
        monster.indexBuffer.SetData(indices);
        }
    
    /// <summary>
    /// Draw the Monster
    /// </summary>
    public void DrawMonster()
    {
        GameEnvironment.Graphics.SetVertexBuffer(monster.vertexBuffer);
        GameEnvironment.Graphics.Indices = monster.indexBuffer;

        Matrix monsterMatrix = Matrix.CreateConstrainedBillboard(Vector3.Zero, position, Vector3.UnitY, Vector3.Up, Object3D.playercamera.ViewVertex);
        effect.Parameters["colorMap"].SetValue(monster.monsterTexture); // anders wordt het een andere texture
            
        foreach(EffectPass pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GameEnvironment.Graphics.SamplerStates[0] = SamplerState.LinearClamp;
            GameEnvironment.Graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, monster.vertexBuffer.VertexCount, 0, monster.indexBuffer.IndexCount / 3);

        }
        GameEnvironment.Graphics.SetVertexBuffer(null);
        GameEnvironment.Graphics.Indices = null;
        RasterizerState prevRasterizerState = GameEnvironment.Graphics.RasterizerState;
        BlendState prevBlendState = GameEnvironment.Graphics.BlendState;

        GameEnvironment.Graphics.BlendState = BlendState.NonPremultiplied;
        GameEnvironment.Graphics.RasterizerState = RasterizerState.CullNone;
        GameEnvironment.Graphics.BlendState = prevBlendState;
        GameEnvironment.Graphics.RasterizerState = prevRasterizerState;
    }

    /// <summary>
    /// Draw the things that need to be drawn
    /// </summary>
    /// <param name="gameTime">The object used for reacting to timechanges</param>
    /// <param name="spriteBatch">The SpriteBatch</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawMonster();
    }
}

