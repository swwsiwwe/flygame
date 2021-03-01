using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyGame
{
    class BossPlane
    {
        int level;
        int power;
        bool boss;
        protected int hp;
        protected int current_hp;
        public List<Bullet> bullets;
        protected Image boom;
        protected Image img;
        protected Image bullet_img1;
        protected Image bullet_img2;
        protected Image bullet_img3;
        protected Image bullet_img4;
        protected int height_p;
        protected int width_p;
        protected int height;
        protected int width;
        protected int tick;
        public int x;
        public int y;
        int speed_y;
        int speed_x;
        protected bool isDestory;

        public bool hasBoss()
        {
            return boss;
        }
        public void callBoss()
        {
            boss = true;
        }
        public void init()
        {

            boss = false;
            level = 0;
            current_hp = hp;
            tick = 0;
            isDestory = false;
            setTrack();
        }
        public void setTrack()
        {
            x = 0;
            y = 0;
            speed_y = 5 + power / 2;
            speed_x = 5+ power / 2;
        }

        public Rectangle getRectangle()
        {
            return new Rectangle(this.x, this.y, this.width, this.height);
        }

        public int destory()
        {
            if (current_hp <= 0)
            {
                isDestory = true;
                return hp;
            }
            return 0;
        }

        public void damage(int d)
        {
            current_hp -= d;
        }


        public BossPlane(Image img, int height_p, int width_p, Image boom, int hp)
        {
            boss = false;
            level = 0;
            
            bullet_img1 = Image.FromFile("Resources/plane/子弹3c.png");
            bullet_img2 = Image.FromFile("Resources/plane/子弹5c.png");
            bullet_img3 = Image.FromFile("Resources/plane/子弹4c.png");
            bullet_img4 = Image.FromFile("Resources/plane/子弹5rc.png");
            this.hp = hp;
            this.current_hp = hp;
            isDestory = false;
            tick = 0;
            bullets = new List<Bullet>();
            this.boom = boom;
            this.img = img;
            this.height_p = height_p;
            this.height = img.Height;
            this.width = img.Width;
            this.width_p = width_p;
            setTrack();
        }

        //绘制飞机
        public void draw(Graphics g)
        {
            if (boss)
            {
                if (!isDestory)
                {
                    g.DrawImage(img, x, y);
                }
                else
                {
                    g.DrawImage(boom, x, y);
                    tick++;
                    if (tick >= 20)
                    {
                        
                        init();
                    }
                }
            }
        }
        public void addLevel()
        {
            power += 1;
        }
        public void initLevel()
        {
            power = 0;
        }

        public bool getStatus()
        {
            return isDestory;
        }

        //绘制子弹
        public void drawBullets(Graphics g)
        {
            foreach (Bullet b in bullets)
            {
                if (b.getOutStatus())
                {
                    bullets.Remove(b);
                    break;
                }
                b.draw(g);
            }
        }

        //飞机轨迹计算
        public void play(Graphics g)
        {
            //current_hp = 50;
            if (current_hp >= (hp * 3 / 4))
            {
                level = 0;
                x += speed_x;

            }
            else if (current_hp >= (hp / 4))
            {
                level = 1;
                x += speed_x;
                y += speed_y;
            }
            else
            {
                level = 2;
                x +=  speed_x * 3 / 4;
                y +=  speed_y * 3 / 4;
            }

            if (x >= width_p - width)
            {
                speed_x = -5;
            }
            if (x <= 0)
            {
                speed_x = 5;
            }
            if (y >= height_p)
            {
                speed_y = -5;
            }
            if (y <= 0)
            {
                speed_y = 5;
            }
            draw(g);
        }

        //开火
        public void fire()
        {

            if (!isDestory)
            {
                if (level == 0)
                {
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 5 + power, 1));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 5 + power, 2));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 5 + power, 3));
                }
                else if (level == 1)
                {
                    bullets.Add(new Bullet(bullet_img2, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 1));
                    bullets.Add(new Bullet(bullet_img2, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 6));
                    bullets.Add(new Bullet(bullet_img4, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 4));
                    bullets.Add(new Bullet(bullet_img4, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 5));
                }
                else if (level == 2)
                {
                    bullets.Add(new Bullet(bullet_img3, speed_y, x, y, height_p, width_p, height, width, false, 10+ power, 1));
                    bullets.Add(new Bullet(bullet_img2, speed_y, x, y, height_p, width_p, height, width, false, 3 + power, 6));
                    bullets.Add(new Bullet(bullet_img4, speed_y, x, y, height_p, width_p, height, width, false, 3 + power, 4));
                    bullets.Add(new Bullet(bullet_img4, speed_y, x, y, height_p, width_p, height, width, false, 3 + power, 5));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 3 + power, 2));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 3));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 7));
                    bullets.Add(new Bullet(bullet_img1, speed_y, x, y, height_p, width_p, height, width, false, 4 + power, 8));
                }
            }


        }
    }
}