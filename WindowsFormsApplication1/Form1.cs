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
        public TileMap Map { get; private set; }

        public Form1()
        {
            InitializeComponent();
            this.Map = new TileMap(Properties.Resources.grass_dirt, 32, 32);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(Map.Tiles[4], 0, 0);
            g.DrawImage(Map.Tiles[4], 32, 0);
        }
    }

    public class TileMap
    {
        public Bitmap SourceBitmap { get; private set; }
        public List<Bitmap> Tiles { get; private set; }

        private int tileWidth, tileHeight;

        public TileMap(Bitmap bitmap, int tileWidth, int tileHeight)
        {
            this.SourceBitmap = bitmap;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

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
