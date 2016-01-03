using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DrawingHelper
{
    protected static Texture2D pixel;

    //Make a White Pixel
    public static void Initialize(GraphicsDevice graphics)
    {
        pixel = new Texture2D(graphics, 1, 1);
        pixel.SetData(new[] { Color.White });
    }
}