using MyGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyGame
{
    class Gem
    {
        int id;
        int tick;
        int height;
        int width;
        Image img;

        Image exp;
        Image hp;
        Image fire;
        Image bullet_speed;
        Image tunder;        
        Image bigbang;

        bool hasSupply;
        int x; int y;
        int speed;
        Random rd;
        public Gem(int height,int width)
        {
            hasSupply = false;
            tick = 0;
            this.height = height;
            this.width = width;
            speed = 10;
            x = 0;
            y = width / 3;
            exp = Image.FromFile("Resources/plane/exp.png");
            hp = Image.FromFile("Resources/plane/hp.png");          
            bullet_speed = Image.FromFile("Resources/plane/speed.png");
            tunder = Image.FromFile("Resources/plane/tunder.png");
            fire = Image.FromFile("Resources/plane/water.png");
            bigbang = Image.FromFile("Resources/plane/bigbang.png");
            rd = new Random();
            int tp = rd.Next(0, 125);            
            if (tp < 25) //加强
            {
                id = 0;
                img = exp;
            }
            else if (tp < 49) //加血
            {
                id = 1;
                img = hp;
            }
            else if (tp < 73) //火焰
            {
                id = 2;
                img = fire;
            }
            else if (tp < 97) //射速
            {
                id = 3;
                img = bullet_speed;
            }
            else if (tp < 121) //激光
            {
                id = 4;
                img = tunder;
            }           
            else
            {
                id = 5;
                img = bigbang; //必杀
            }        
        }
        public void supply()
        {
            tick++;
            if (tick >= 35)
            {
                
                hasSupply = true;
                tick = 0;
            }
        }
        public void sendSupply()
        {
            hasSupply = true;
            tick = 0;
        }
        public void init()
        {
            
            x = 0;
            tick = 0;
            int tp = rd.Next(0, 125);
            //tp = 124;
            if (tp < 25) //加强
            {
                id = 0;
                img = exp;
            }
            else if (tp < 49) //加血
            {
                id = 1;
                img = hp;
            }
            else if (tp < 73) //火焰
            {
                id = 2;
                img = fire;
            }
            else if (tp < 97) //射速
            {
                id = 3;
                img = bullet_speed;
            }
            else if (tp < 121) //激光
            {
                id = 4;
                img = tunder;
            }
            else
            {
                id = 5;
                img = bigbang; //必杀
            }
        }
        public void play(Graphics g)
        {
            if (hasSupply)
            {
                x += speed;
                if (x >= width)
                {
                    init();
                    hasSupply = false;
                }
                else
                {
                    draw(g);
                }
            }                        
        }
        public int destory()
        {
            int i = id;
            hasSupply = false;            
            init();
            return i;
        }
        public void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }
        public Rectangle getRectangle()
        {
            return new Rectangle(this.x, this.y, img.Width, img.Height);
        }
    }
}