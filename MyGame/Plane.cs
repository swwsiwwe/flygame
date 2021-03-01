using System;
using System.Collections.Generic;
using System.Drawing;


namespace MyGame
{
    class Plane
    {
        int level;
        protected int bullet_damage;//子弹伤害
        protected int hp;
        protected int current_hp;
        public List<Bullet> bullets;
        protected Image boom;
        protected Image img;
        protected Image bullet_img;
        protected int height_p;
        protected int width_p;
        protected int height;
        protected int width;
        protected int tick;
        public int x;
        public int y;
        int fg;
        int speed_x;
        int speed_y;
        protected bool isDestory;
        Random rd;

        public void init()
        {            
            current_hp = hp;
            tick = 0;
            isDestory = false;
            setTrack();
        }
        public void setTrack()
        {
            int k = rd.Next(0, 100);
            y = rd.Next(0, height_p / 3);
            x = rd.Next(0, width_p);
            if (k <= 33)
            {
                x = 0;
                fg = 1;
            }
            else if (k > 33 && k < 66)
            {
                x = width_p;
                fg = -1;
            }
            else
            {
                fg = 0;
                y = 0;
            }
            speed_x = rd.Next(1, 6) + level;
            speed_y = rd.Next(2, 10) + level/2;
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

        public Plane(Image img, int height_p, int width_p,Image boom,int hp,Image bullet,int bullet_damage)
        {
            level = 0;
            this.bullet_damage = bullet_damage;
            this.hp = hp;
            this.current_hp = hp;
            isDestory = false;
            tick = 0;
            bullets = new List<Bullet>();
            bullet_img = bullet;
            this.boom = boom;
            this.img = img;
            bullet_img = bullet;
            this.height_p = height_p;
            this.height = img.Height;
            this.width = img.Width;
            this.width_p = width_p;
            rd = new Random();

            setTrack();
        }
        public void addLevel()
        {
            level += 1;
        }
        public void initLevel()
        {
            level = 0;
        }
        //绘制飞机
        public void draw(Graphics g)
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
        public bool getStatus()
        {
            return isDestory;
        }

        //绘制子弹
        public void drawBullets(Graphics g)
        {           
            foreach (Bullet b in bullets)
            {
                if (b.getOutStatus() && b.isLast())
                {
                    bullets.Clear();
                    break;
                }
                
                b.draw(g);               
            }            
        }

        //飞机轨迹计算
        public void play(Graphics g)
        {
            draw(g);
            if (!isDestory)
            {
                x += (speed_x * fg);
                y += speed_y;
            }
          
            
            if (fg == -1)
            {
                if (x <= 0 - width || y >= height_p)
                {
                    init();
                }
            }
            else
            {
                if (x >= width_p || y >= height_p)
                {
                    init();
                }
            }
        }

        //开火
        public void fire(int count)
        {
            if (!isDestory)
            {
                if (bullets.Count < count)
                    bullets.Add(new Bullet(bullet_img, speed_y, x, y, height_p, width_p, height, width, false,bullet_damage + level,1));
                else if (bullets.Count == count)
                {
                    bullets.Add(new Bullet(bullet_img, speed_y, x, y, height_p, width_p, height, width, true, bullet_damage + level, 1));
                }
                else
                {
                    return;
                }
            }            
        }
    }
}