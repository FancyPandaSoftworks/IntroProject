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
            case "Closet": return 10; 
            case "Cupboard": return 40;
            case "Cupboard2": return 71;
            case "Table": return 70;
            case "Chair": return 70;
            case "Confused Cat": return -20;
            case "Surprise Cat": return -20;
            case "Shocked Cat": return -20;
            case "Baker Cat": return -20;
            case "Sir Quokkalot": return -20;
            default: return 0;
        }
    }

    public int Width(string name)
    {
        switch (name)
        {
            case "Closet": return 130; 
            case "Cupboard": return 100; 
            case "Cupboard2": return 130;
            case "Table": return 150;
            case "Chair": return 150;
            case "Confused Cat": return 105;
            case "Surprise Cat": return 105;
            case "Shocked Cat": return 105;
            case "Baker Cat": return 105;
            case "Sir Quokkalot": return 105;
            default: return 0;
        }
    }
}
