using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

public class Collision
{
    
    public static Vector2 CalculatedIntersectionDepth(Rectangle rectA, Rectangle rectB)
    {
        Vector2 minDistance = new Vector2(rectA.Width + rectB.Width, rectA.Height + rectB.Height) / 2;
        Vector2 centerA = new Vector2(rectA.Center.X, rectA.Center.Y);
        Vector2 CenterB = new Vector2(rectB.Center.X, rectB.Center.Y);
        Vector2 distance = centerA - CenterB;
        Vector2 depth = Vector2.Zero;
        if (distance.X > 0)
            depth.X = minDistance.X - distance.X;
        else
            depth.X = -minDistance.X - distance.X;
        if (distance.Y > 0)
            depth.Y = minDistance.Y - distance.Y;
        else
            depth.Y = -minDistance.Y - distance.Y;
        return depth;
    }

    public static bool Collision3D(BoundingSphere player, Object3D object1, Vector3 pos)
    {
        
        for (int i = 0; i < object1.Model.Meshes.Count; i++)
        {
            BoundingSphere bs1 = object1.Model.Meshes[i].BoundingSphere;
            bs1.Center += object1.Position;
            if (player.Intersects(bs1))
                return true;
        }
        return false;
    }

    public static Rectangle Intersection(Rectangle rect1, Rectangle rect2)
    {
        int xmin = (int)MathHelper.Max(rect1.Left, rect2.Left);
        int xmax = (int)MathHelper.Min(rect1.Right, rect2.Right);
        int ymin = (int)MathHelper.Max(rect1.Top, rect2.Top);
        int ymax = (int)MathHelper.Min(rect1.Bottom, rect2.Bottom);
        return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
    }
}
