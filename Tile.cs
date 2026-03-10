using Microsoft.Xna.Framework.Graphics;

namespace SpaceRace;


    public class Tile
    {
        public int X;
        public int Y;
        public int Height;
        public Texture2D Texture;

        public Tile(int x, int y, Texture2D texture, int height = 0)
        {
            X = x;
            Y = y;
            Texture = texture;
            Height = height;
        }
    }
