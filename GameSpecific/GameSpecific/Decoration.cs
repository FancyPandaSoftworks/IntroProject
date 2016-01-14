using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
   
enum DecorationType
{
    cupboard, cuboard2, chairTable, deadBody, empty
}

class Decoration: Object3D
{
    protected DecorationType decorationType;
    public Decoration(string modelName, string id, DecorationType decorationType = DecorationType.empty): base(modelName, id)
    {
        this.decorationType = decorationType;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (decorationType == DecorationType.empty)
            return;
        base.Draw(gameTime, spriteBatch);
    }
}

class Cupboard: Decoration
{
    public Cupboard(string cupboard, string id = "Cupboard")
        : base(cupboard, "Misc Level Objects\\Cupboard\\Cupboard", DecorationType.cupboard)
    {
       
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(GameEnvironment.Random.Next(0,10)==0)
        base.Draw(gameTime, spriteBatch);
        position = new Vector3(Position.X, Position.Y, Position.Z);
        Console.WriteLine(position);
    }
}

/*class Cupboard2: Decoration
{
    public Cupboard2(string cupboard2, string id = "cupboard2"): base(cupboard2, "cupboard", DecorationType.cuboard2)
    {

    }
}

class ChairTable: Decoration
{
    public ChairTable(string chairTable, string id = "chairTable"): base(chairTable, "chairTable", DecorationType.chairTable)
    {

    }
}

class DeadBody: Decoration
{
    public DeadBody(string deadBody, string id ="deadBody"): base(deadBody, "deadBody", DecorationType.deadBody)
    {

    }
}
*/