using System;

using System.Drawing;

namespace MyGame
{
    class Bullet
    {
        bool Last;
        int id;
        protected int damage;
        protected int x;
        protected int y;
        protected Image img;
        protected int speed;
        protected int height;
        int width;
        protected int height_p;
        protected int width_p;
        protected bool isOut;

        public int getDamage()
        {
            return damage;
        }
        public bool isLast()
        {
            return Last;
        }
        public bool getOutStatus()
        {
            return isOut;
        }
        public Rectangle getRectangle()
        {
            return new Rectangle(this.x, this.y, img.Width, img.Height);
        }

        public Bullet(Image img, int speed, int x, int y, int height,int width, int height_p, int width_p, bool isLast, int damage, int id)
        {
            this.width = width;
            this.id = id;
            this.damage = damage;
            this.Last = isLast;
            this.height_p = height_p;
            this.width_p = width_p;
            this.x = x + width_p / 2 - img.Height / 2 ;
            this.y = y + height_p;
            this.img = img;
            this.speed = Math.Abs(speed) + 2;
            this.height = height;
            isOut = false;
        }

        public void track_left()
        {
            x += speed;
            y += speed;
        }
        public void track_right()
        {
            x -= speed;
            y += speed;
        }


        public void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
            if (id == 1)
            {
                y += speed;
            }
            else if (id == 2)
            {
                track_left();
            }
            else if (id == 3)
            {
                track_right();
            }
            else if (id == 4)
            {
                track_l();
            }
            else if (id == 5)
            {
                track_r();
            }else if(id == 6)
            {
                track_b();            
            }
            else if (id == 7)
            {
                track_l();
                track_b();
            }
            else if (id == 8)
            {
                track_b();
                track_r();
            }

            if (y>=height||y<=0||x>=width||x<=0)
            {
                isOut = true;
            }
        }
        public void track_l()
        {
            x -= speed;
        }
        public void track_r()
        {
            x += speed;
        }
        public void track_b()
        {
            y -= speed;
        }
    }
}
