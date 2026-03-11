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
        Texture2D characterTexture;
        
        Texture2D supportTexture;

        int mapWidth = 10;
        int mapHeight = 10;

        int tileWidth = 64;
        int tileHeight = 32;
        
        Tile[,] map;
        
        Texture2D pixel;

        private Vector2 charPos = Vector2.Zero;
        
        KeyboardState previousKeyboard;
        
        
      

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
             
             supportTexture = Content.Load<Texture2D>("New Piskel (10) (2)");
             characterTexture = Content.Load<Texture2D>("New Piskel (12)");
            
         
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            
            map = new Tile[mapWidth, mapHeight];

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (x == y)
                    {
                        map[x, y] = new Tile(x, y, tileTexture2);
                        map[x, y].Texture = tileTexture;
                        map[x, y].Height = 1;
                    }
                    else if ((x ==0 && y == 1) || (x ==1 && y == 0 ))
                    {
                        map[x, y] = new Tile(x, y, tileTexture2);
                        map[x, y].Texture = tileTexture;
                        map[x, y].Height = 2;
                    }
                    
                    else if ((x ==1 && y == 1))
                    {
                        map[x, y] = new Tile(x, y, tileTexture2);
                        map[x, y].Texture = tileTexture;
                        map[x, y].Height = 1;
                    }

                    else
                    {
                        map[x, y] = new Tile(x, y, tileTexture2);
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Escape))
                Exit();

            if (kb.IsKeyDown(Keys.W) && previousKeyboard.IsKeyUp(Keys.W))
                charPos.Y -= 1;

            if (kb.IsKeyDown(Keys.S) && previousKeyboard.IsKeyUp(Keys.S))
                charPos.Y += 1;

            if (kb.IsKeyDown(Keys.A) && previousKeyboard.IsKeyUp(Keys.A))
                charPos.X -= 1;

            if (kb.IsKeyDown(Keys.D) && previousKeyboard.IsKeyUp(Keys.D))
                charPos.X += 1;

            // keep player inside map
            charPos.X = MathHelper.Clamp(charPos.X, 0, mapWidth - 1);
            charPos.Y = MathHelper.Clamp(charPos.Y, 0, mapHeight - 1);

            previousKeyboard = kb;

            base.Update(gameTime);
        }      

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            
            for (int y = 0; y < mapHeight; y++){
                for (int x = 0; x < mapWidth; x++){
                    Tile tile = map[x, y];

                    Vector2 basePos = WorldToScreen(tile.X, tile.Y);

                    // draw supports
                    for (int h = 0; h < tile.Height; h++)
                    {
                        Vector2 pos = basePos;
                        pos.Y -= (h) * (tileHeight / 2);

                        _spriteBatch.Draw(
                            supportTexture,
                            new Rectangle((int)pos.X, (int)pos.Y, tileWidth, tileHeight),
                            Color.White
                        );
                    }

                    // draw tile top
                    Vector2 topPos = basePos;
                    topPos.Y -= tile.Height * (tileHeight / 2);

                    _spriteBatch.Draw(
                        tile.Texture,
                        new Rectangle((int)topPos.X, (int)topPos.Y, tileWidth, tileHeight),
                        Color.White
                    );

                    // DRAW CHARACTER IF ON THIS TILE
                    if ((int)charPos.X == x && (int)charPos.Y == y)
                    {
                        Vector2 charDrawPos = topPos;

                        _spriteBatch.Draw(
                            characterTexture,
                            new Rectangle((int)charDrawPos.X + 12, (int)charDrawPos.Y - 20, 40, 40),
                            Color.White
                        );
                    }
                    
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