using System;
using System.Drawing;

namespace MyGame
{
    class Background
    {
        int height;
        int width;
        Image[] img;
        int[] x;
        int[] y;
        public Background(int height, int width)
        {
            this.height = height;
            this.width = width;
            img = new Image[7];
            x = new int[7];
            y = new int[7];
            Random rd = new Random();
            for (int i = 0; i < 7; i++)
            {
                x[i] = width / 7 * i + width / 14;
                y[i] = rd.Next(0, height);
            }
            img[0] = Image.FromFile("Resources/plane/1.png");
            img[1] = Image.FromFile("Resources/plane/2.png");
            img[2] = Image.FromFile("Resources/plane/3.png");
            img[3] = Image.FromFile("Resources/plane/4.png");
            img[4] = Image.FromFile("Resources/plane/5.png");
            img[5] = Image.FromFile("Resources/plane/6.png");
            img[6] = Image.FromFile("Resources/plane/7.png");
        }
        public void play(Graphics g)
        {
            for (int i = 0; i < 7; i++)
            {
                g.DrawImage(img[i], x[i], y[i]++);
                if (y[i] >= height)
                {
                    y[i] = 0 - img[i].Height;
                }
            }
        }
    }
}