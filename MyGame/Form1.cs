using MyGame;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    public delegate void play(Graphics g);
    public delegate void living();
    public partial class Form1 : Form
    {
        int score;
        bool status;
        int temp;
        Background background; //背景图片
        play p1; //飞行委托        
        List<Plane> p; //敌方飞机队列
        BossPlane bp;
        List<Bullet> bossBullets;
       // List<LittlePlane> littlePlanes;
        List<Bullet> bullets;
        List<HeroBullet> heroBullets;
        Graphics gg; //画笔
        Bitmap b; //画板
        HeroPlane hp; //玩家飞机
        Gem gem; //宝物
        int gem_tp;

        private void clear()
        {
            b.Dispose();
            gg.Dispose();
        }
        public Form1()
        {
            InitializeComponent();
            gem_tp=temp = 1;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            label2.Visible = false;
            hp_label.Visible = false;
            label4.Visible = false;
            score_label.Visible = false;

            init();
           // start();
        }
        private void init()
        {
            bp = new BossPlane(Image.FromFile("Resources/plane/boss.png"), this.Height, this.Width, Image.FromFile("Resources/plane/boss_boom.png"), 400);
            background = new Background(this.Height, this.Width);
            gem = new Gem(this.Height, this.Width);
            hp = new HeroPlane(Image.FromFile("Resources/plane/飞机5c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机5c_boom.png"), 100, Image.FromFile("Resources/plane/子弹1c.png"), 0);
            b = new Bitmap(this.Width, this.Height);
            gg = Graphics.FromImage(b);
            p = new List<Plane>();
             p.Add(new Plane(Image.FromFile("Resources/plane/飞机1c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机1c_boom.png"), 10, Image.FromFile("Resources/plane/子弹3c.png"), 5));
             p.Add(new Plane(Image.FromFile("Resources/plane/飞机2c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机2c_boom.png"), 5, Image.FromFile("Resources/plane/子弹2c.png"), 8));
             p.Add(new Plane(Image.FromFile("Resources/plane/飞机3c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机3c_boom.png"), 20, Image.FromFile("Resources/plane/子弹1c.png"), 10));
             p.Add(new Plane(Image.FromFile("Resources/plane/飞机4c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机4c_boom.png"), 15, Image.FromFile("Resources/plane/子弹5c.png"), 4));
             p.Add(new Plane(Image.FromFile("Resources/plane/飞机6c.png"), this.Height, this.Width, Image.FromFile("Resources/plane/飞机6c_boom.png"), 40, Image.FromFile("Resources/plane/子弹4c.png"), 20));
           
            p1 = new play(p[0].play);                      
            p1 += p[1].play;
            p1 += p[2].play;
            p1 = new play(p[0].play);
            p1 += p[1].play;
            p1 += p[2].play;
            p1 += p[3].play;
            p1 += p[4].play;
          
            setInterval();
        }
        private void end()
        {
            
            if (hp.getStatus())
            {
                status = false;
                pictureBox1.Visible = true;
                score_end.Text = "上局得分:" + score;
                label1.Visible = true;
                hp_label.Visible = false;
                label2.Visible = false;
                button1.Visible = true;
                label4.Visible = false;
                score_label.Visible = false;
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                hp.init();
                //clear();
                bp.init();
                System.Windows.Forms.Cursor.Show();
                copyright.Visible = true;
            }
        }
        private void start()
        {
            copyright.Visible=false;
            score_tp = 0;
            status = true;
            gem_tp = temp = 1;
            pictureBox1.Visible = false;

            bp.initLevel();
            foreach(Plane pp in p)
            {
                pp.initLevel();
            }
            score_end.Text = "";
            score = 0;
            System.Windows.Forms.Cursor.Hide();
            setInterval();
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
        }
        private void setInterval()
        {
            timer1.Interval = 10;
            
            timer2.Interval = 500;
            
            timer3.Interval = 600;

            timer4.Interval = 1000;
            
           
        }
        int gem_id = 0;
        int score_tp = 0;
        public bool checkCrash()
        {
            heroBullets = hp.heroBullets;
            bossBullets = bp.bullets;
            if (hp.getRectangle().IntersectsWith(gem.getRectangle()))
            {
                gem_id = hp.getPower(gem.destory());
                if (gem_id == 3)
                {
                    if (timer3.Interval > 300)
                    {
                        timer3.Interval -= 100;
                    }
                }
                else if (gem_id == 5)
                {
                    foreach (Plane pp in p)
                    {
                        pp.damage(50);
                        score += 50;
                        pp.destory();
                    }
                }
            }
            if (bp.hasBoss())
            {
                if (heroBullets.Count > 0)
                {
                    for (int j = heroBullets.Count - 1; j >= 0; j--)
                    {
                        if ((!bp.getStatus()) && bp.getRectangle().IntersectsWith(heroBullets[j].getRectangle()))
                        {
                            bp.damage(heroBullets[j].getDamage());
                            if ((score_tp = bp.destory()) != 0)
                            {                                
                                score += score_tp;
                                score_tp = 0;
                                //难度提高
                                bp.addLevel();
                                foreach (Plane p_tp in p)
                                    p_tp.addLevel();
                            }
                                
                            heroBullets.Remove(heroBullets[j]);
                            return true;
                        }

                    }
                   
                }
                for (int k = bossBullets.Count - 1; k >= 0; k--)
                {
                    if (bossBullets[k].getRectangle().IntersectsWith(hp.getRectangle()))
                    {
                        hp.damage(bossBullets[k].getDamage());
                        bossBullets.Remove(bossBullets[k]);
                        hp.destory();
                        return true;
                    }
                }
                if ((!bp.getStatus()) && bp.getRectangle().IntersectsWith(hp.getRectangle()))
                {
                    if (!hp.isImpact())
                    {
                        bp.damage(10);
                        hp.damage(10);
                        hp.changeImpact(true);
                        if ((score_tp = bp.destory()) != 0)
                        {
                            score += score_tp;
                            score_tp = 0;

                            bp.addLevel();
                            foreach (Plane p_tp in p)
                                p_tp.addLevel();
                        }
                        
                        hp.destory();
                    }
                    return true;
                }
                else
                {
                    hp.changeImpact(false);
                }
            }
            else
            {
                for (int i = p.Count - 1; i >= 0; i--)
                {
                    if (heroBullets.Count > 0)
                        for (int j = heroBullets.Count - 1; j >= 0; j--)
                        {
                            if ((!p[i].getStatus()) && p[i].getRectangle().IntersectsWith(heroBullets[j].getRectangle()))
                            {
                                p[i].damage(heroBullets[j].getDamage());
                                score += p[i].destory();
                                heroBullets.Remove(heroBullets[j]);
                                return true;
                            }
                        }
                    bullets = p[i].bullets;
                    for (int k = bullets.Count - 1; k >= 0; k--)
                    {
                        if (bullets[k].getRectangle().IntersectsWith(hp.getRectangle()))
                        {
                            hp.damage(bullets[k].getDamage());
                            bullets.Remove(bullets[k]);
                            hp.destory();
                            return true;
                        }
                    }
                    if ((!p[i].getStatus()) && p[i].getRectangle().IntersectsWith(hp.getRectangle()))
                    {
                        if (!hp.isImpact())
                        {
                            p[i].damage(10);
                            hp.damage(10);
                            hp.changeImpact(true);
                            score += p[i].destory()/2;
                            hp.destory();
                        }

                        //p[i].destory();               
                        return true;
                    }
                    else
                    {
                        hp.changeImpact(false);
                    }
                }
            }                                 
            return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            checkCrash();
            hp_label.Text = hp.getHP() + "";
            score_label.Text = score + "";
            if (score / 100 >= gem_tp)
            {
                gem.sendSupply();
                gem_tp = score / 100 + 1;
            }
            this.Invalidate();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {

            if (bp.hasBoss())
            {
                bp.fire();
            }
            else
            {
                foreach (Plane plane in p)
                {
                    plane.fire(4);
                }
            }

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            hp.fire();
            end();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (gg != null)
            {
                gg.Clear(Color.Transparent);//避免重复创建对象
                background.play(gg);

                if (status)
                {
                    hp.play(gg);
                    gem.play(gg);
                    hp.drawBullets(gg);
                    if (score / 100 >= temp)
                    {
                        hp.addHp();
                        temp = score / 100 + 5;
                        bp.callBoss();

                    }
                    if (bp.hasBoss())
                    {
                        bp.play(gg);
                        bp.drawBullets(gg);
                    }
                    else
                    {
                        p1(gg);
                        foreach (Plane p in p)
                        {
                            p.drawBullets(gg);
                        }
                    }
                }
                e.Graphics.DrawImage(b, 0, 0);

            }

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
         
            if (hp != null)
            {
                hp.x = e.X;
                hp.y = e.Y;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            button1.Visible = false;
            hp_label.Visible = true;
            label2.Visible = true;
            label4.Visible = true;
            score_label.Visible = true;
            start();
            
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            gem.supply();            
        }
    }
}