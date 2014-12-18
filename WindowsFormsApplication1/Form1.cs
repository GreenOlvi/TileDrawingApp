using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Map GameMap { get; set; }

        public Form1()
        {
            InitializeComponent();

            this.GameMap = new Map(new TiledBitmap(TileDrawing.Properties.Resources.grass_dirt, 32, 32));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            GameMap.Draw(g);
        }

    }

    public class Map
    {
        public TiledBitmap tileMap { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        private int[,] tiles;

        public Map(TiledBitmap tileMap)
        {
            this.tileMap = tileMap;

            this.Width = 5;
            this.Height = 5;
            this.tiles = new int[,] {
                { 0,  0,  0,  0,  0 },
                { 3,  4,  4,  4,  5 },
                { 6,  7,  7,  7,  8 },
                { 9, 10, 10, 10, 11 },
                { 0,  0,  0,  0,  0 },
            };
        }

        public void Draw(Graphics g)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var t = tiles[y,x];
                    g.DrawImage(tileMap.Tiles[t], x * tileMap.TileWidth, y * tileMap.TileHeight);
                }
            }
        }
    }

    public class TiledBitmap
    {
        public Bitmap SourceBitmap { get; private set; }
        public List<Bitmap> Tiles { get; private set; }

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public TiledBitmap(Bitmap bitmap, int tileWidth, int tileHeight)
        {
            this.SourceBitmap = bitmap;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.Tiles = SplitIntoTiles(bitmap, tileWidth, tileHeight);
        }

        private static List<Bitmap> SplitIntoTiles(Bitmap bitmap, int tileWidth, int tileHeight)
        {
            var tilesWide = bitmap.Width / tileWidth;
            var tilesHigh = bitmap.Height / tileHeight;

            var tiles = new List<Bitmap>(tilesWide * tilesHigh);

            for (int j = 0; j < tilesHigh; j++)
            {
                for (int i = 0; i < tilesWide; i++)
                {
                    var tile = new Bitmap(tileWidth, tileHeight);
                    var g = Graphics.FromImage(tile);
                    g.DrawImage(bitmap, 0, 0, new Rectangle(i * tileWidth, j * tileHeight, (i + 1) * tileWidth, (j + 1) * tileHeight), GraphicsUnit.Pixel);
                    tiles.Add(tile);
                }
            }

            return tiles;
        }
    }
}
