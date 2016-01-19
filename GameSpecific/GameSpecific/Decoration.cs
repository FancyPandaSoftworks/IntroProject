using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
   
class Decoration: Object3D
{
    public Decoration(string modelName, string id): base(modelName, id)
    {
    }

    public int Height(string name)
    {
        switch (name)
        {
            case "Closet": return 0; 
            case "Cupboard": return 30; 
            case "ChairTable": return 15; 
            default: return 0;
        }
    }

    public int Width(string name)
    {
        switch (name)
        {
            case "Closet": return 130; 
            case "Cupboard": return 100; 
            case "ChairTable": return 250; 
            default: return 0;
        }
    }
}
