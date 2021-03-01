using System;

using System.Drawing;

namespace MyGame
{
    class HeroBullet : Bullet
    {
        int id;
        int width;
        public HeroBullet(Image img, int x, int y, int height,int width, int height_p, int width_p,int damage,int id) : base(img, 0, x, y, height, height_p, width_p, width_p, false,damage,id)
        {
            this.width = width;
            this.id = id;
            this.speed = 10;
            if(id == 1)
            {
                this.y = y;
                this.x = x + width_p / 2 ;
            }
            else if (id==4)
            {
                this.y = y + height_p;
                this.x = x + width_p / 2 ;
            }
            else if (id == 5)
            {
                this.x = x;
                this.y = y + height_p / 2;
            }
            else if (id == 6)
            {
                this.x = x + width_p;
                this.y = y + height_p / 2;
            }
            
        }

        public new void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
            if (id == 1)
            {
                y -= speed;
            }
            else if (id == 2)
            {
                track_left();
            }
            else if (id == 3)
            {
                track_right();
            }else if (id == 4)
            {
                track_b();
            }
            else if (id == 5)
            {
                track_l();
            }
            else if (id == 6)
            {
                track_r();
            }

            if (y <= 0  ||  y >= height ||x <= 0|| x >= width)
            {
                isOut = true;
            }
        }
        public new void track_left()
        {
            x -= speed;
            y -= speed;
        }
        public new void track_right()
        {
            x += speed;
            y -= speed;
        }
        public new void track_l()
        {
            x -= speed;           
        }
        public new void track_r()
        {
            x += speed;            
        }
        public new void track_b()
        {          
            y += speed;
        }
    }
}