using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceRace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D tileTexture;
        Texture2D tileTexture2;

        int mapWidth = 10;
        int mapHeight = 10;

        int tileWidth = 64;
        int tileHeight = 32;
        
        Tile[,] map;
        
        Texture2D pixel;
        
        
      

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tileTexture = Content.Load<Texture2D>("New Piskel (10)");
             tileTexture2 = Content.Load<Texture2D>("New Piskel (11)");
            
         
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            
            map = new Tile[mapWidth, mapHeight];

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                   // if (x == y)
                    //{
                     //   map[x,y] = new Tile(x, y, tileTexture2);
                       // map[x,y].Texture = tileTexture;
                        //map[x, y].Height = 1;
                    //}
                    //else
                    //{
                        map[x, y] = new Tile(x, y, tileTexture2);
                    //}
                }
            }
        }
     

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Tile tile = map[x, y];

                    Vector2 pos = WorldToScreen(tile.X, tile.Y);

                    pos.Y -= tile.Height * 16;

                    _spriteBatch.Draw(
                        tile.Texture,
                        new Rectangle((int)pos.X, (int)pos.Y, tileWidth, tileHeight),
                        Color.White
                    );
                }
            }
           
                    

         
                
             
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        Vector2 WorldToScreen(int x, int y)
        {
            float screenX = (x - y) * (tileWidth / 2f);
            float screenY = (x + y) * (tileHeight / 2f);

            return new Vector2(screenX + 400, screenY + 100);
        }
     
    }
}