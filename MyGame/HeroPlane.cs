using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyGame
{
    class HeroPlane : Plane
    {
        bool impact;
        bool power;
        
        int item;
        Image tp1;
        Image tp2;
        
        public List<HeroBullet> heroBullets;
        public HeroPlane(Image img, int height_p, int width_p,Image boom,int hp,Image HeroBullet,int bullet_damage) : base(img, height_p, width_p,boom,hp,HeroBullet, bullet_damage)
        {
            tp1 = Image.FromFile("Resources/plane/子弹4hc.png");      
            tp2 = Image.FromFile("Resources/plane/子弹5rc.png");
            item = 0;
            power = false;
            impact = false;
            heroBullets = new List<HeroBullet>();
            bullet_img = Image.FromFile("Resources/plane/子弹3c.png");
        }
        public void addHp()
        {
            current_hp += 100;
        }
        public int getPower(int i)
        {
            
            if (i == 0)//子弹强化
            {             
                power = true;
            }
            else if (i == 1)//加血
            {
                current_hp += 20;
            }
            else if (i == 2)//火焰
            {
                if (item != 1)
                {
                    item = 1;
                    power = false;
                    bullet_img = Image.FromFile("Resources/plane/子弹1hc.png");
                }                              
            }           
            else if (i == 4)//激光
            {
                if (item != 2)
                {
                    item = 2;
                    power = false;
                    bullet_img = Image.FromFile("Resources/plane/子弹5c.png");
                }                                                
            }
            return i;
        }
        public bool isImpact()
        {
            return impact;
        }
        public void changeImpact(bool b)
        {
            impact = b;
        }

        public new void init()
        {
            item = 0;
            current_hp = hp;
            impact = false;
            isDestory = false;
            power = false;
            bullet_img = Image.FromFile("Resources/plane/子弹3c.png");
        }

      
        public new void draw(Graphics g)
        {
            if (!isDestory)
            {
                g.DrawImage(img, x, y);
            }
            else
            {
                g.DrawImage(boom, x, y);
               
            }
        }
        public int getHP()
        {
            if (current_hp <= 0)
                return 0;
            return current_hp;
        }
        public new void play(Graphics g)
        {
            draw(g);
        }
        public void fire()
        {
            
            if (item == 1)//fire
            {
                
                if (power)
                {
                    heroBullets.Add(new HeroBullet(tp1, x, y, height_p,width_p, height, width, 30, 1));
                }
                else
                {
                    heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 15, 1));
                }
            }
            else if (item == 2)
            {
                heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 4, 1));
                if (power)
                {
                    heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 12, 4));
                    heroBullets.Add(new HeroBullet(tp2, x, y, height_p, width_p, height, width, 4, 5));
                    heroBullets.Add(new HeroBullet(tp2, x, y, height_p, width_p, height, width, 4, 6));
                   
                }
            }
            else if(item == 0)
            {
                heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 5, 1));
                if (power)
                {
                    heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 8, 2));
                    heroBullets.Add(new HeroBullet(this.bullet_img, x, y, height_p, width_p, height, width, 8, 3));
                }
            }



        }
        
        public new void drawBullets(Graphics g)
        {           
            foreach (HeroBullet b in heroBullets)
            {
                if (b.getOutStatus())
                {
                    heroBullets.Remove(b);
                    break;
                }
                b.draw(g);               
            }            
        }
    }
}