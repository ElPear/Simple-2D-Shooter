namespace DvD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
        int bullets = 9;
        public partial class NewPicBox : PictureBox
        {
            private int Xvalue = 0;
            private int Yvalue = 0;

            public int Xmove
            {
                get { return Xvalue; }
                set { Xvalue = value; }
            }
            public int Ymove
            {
                get { return Yvalue; }
                set { Yvalue = value; }
            }
            public void MoveBox()
            {
                Left += Xmove;
                Top += Ymove;
            }

        }
        bool Colliding(NewPicBox item1, NewPicBox item2)
        {
            if (item1.Location.X + item1.Width < item2.Location.X)
                return false;
            if (item2.Location.X + item2.Width < item1.Location.X)
                return false;
            if (item1.Location.Y + item1.Height < item2.Location.Y)
                return false;
            if (item2.Location.Y + item2.Height < item1.Location.Y)
                return false;
            return true;
        }
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            player.Location = new Point(player.Location.X, Cursor.Position.Y - 30 - Top-player.Height/2);
            label1.Location = player.Location;
            Random rng = new();
            if (rng.Next(0, 51) == 10)
            {
                if (rng.Next(0, 6) == 5)
                {
                    Controls.Add
                    (
                        new NewPicBox()
                        {
                            Xmove = -3,
                            Ymove = 0,
                            BackColor = Color.Black,
                            Size = new Size(50, 50),
                            Location = new Point(this.Width, rng.Next(0, this.Height - 39 - 50))
                        }
                    );
                }
                else { 
                    Controls.Add
                    (
                        new NewPicBox()
                        {
                            Xmove = -3,
                            Ymove = 0,
                            BackColor = Color.Green,
                            Size = new Size(50, 50),
                            Location = new Point(this.Width, rng.Next(0, this.Height-39-50))
                        }
                    );
                }
            }
            foreach (NewPicBox projectile in Controls.OfType<NewPicBox>())
            {
                projectile.MoveBox();
                if (projectile.Left > Width)
                {
                    Controls.Remove(projectile);
                }
                foreach (NewPicBox enemy in Controls.OfType<NewPicBox>())
                {
                    RegisterDamage(projectile, enemy);
                }
            }
        }
        void RegisterDamage(NewPicBox projectile, NewPicBox enemy)
        {
            if (Colliding(projectile, enemy))
            {
                if (projectile.BackColor != Color.Yellow)
                {
                    return;
                }
                switch (enemy.BackColor.ToString())
                {
                    case "Color [Black]":
                        enemy.BackColor = Color.Green;
                        Controls.Remove(projectile);
                        break;
                    case "Color [Green]":
                        enemy.BackColor = Color.Orange;
                        Controls.Remove(projectile);
                        break;
                    case "Color [Orange]":
                        enemy.BackColor = Color.Red;
                        Controls.Remove(projectile);
                        break;
                    case "Color [Red]":
                        Controls.Remove(enemy);
                        Controls.Remove(projectile);
                        bullets += 4;
                        label1.Text = $"Bullets: {bullets}";
                        break;
                    default:
                        break;
                }
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() != " ")
            {
                return;
            }
            if (bullets > 0)
            {
                bullets--;
                label1.Text = $"Bullets: {bullets}";
                Controls.Add
                (
                    new NewPicBox()
                    {
                        Xmove = 8,
                        Ymove = 0,
                        BackColor = Color.Yellow,
                        Size = new Size(20, 20),
                        Location = new Point(player.Right, player.Top + player.Height / 2 - 10)
                    }
                );
            }
        }
    }
}