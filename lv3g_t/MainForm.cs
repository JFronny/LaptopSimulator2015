using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lv3g_t
{
    public partial class MainForm : Form
    {
        #region FRMBD
        uint minigameTime;
        uint minigamePrevTime;
        public MainForm()
        {
            InitializeComponent();
            _initGame();
        }

        private void Button1_Click(object sender, EventArgs e) => Application.Exit();
        private void MinigameClockT_Tick(object sender, EventArgs e)
        {
            minigameTime++;
            minigamePanel.Invalidate();
        }

        private void _initGame()
        {
            minigameTime = 0;
            minigamePrevTime = 0;
            initGame();
        }
        #endregion
        public static int[,] grid = new int[23, 10];
        public static int[,] droppedtetrominoeLocationGrid = new int[23, 10];
        public static bool isDropped = false;
        static Tetrominoe tet;
        static Tetrominoe nexttet;
        public static int linesCleared = 0, score = 0, level = 1;
        public static Random rnd;
        private void initGame()
        {
            rnd = new Random();
            grid = new int[23, 10];
            droppedtetrominoeLocationGrid = new int[23, 10];
            isDropped = false;
            linesCleared = 0;
            score = 0;
            level = 1;
            nexttet = new Tetrominoe();
            tet = nexttet;
            tet.Spawn();
            nexttet = new Tetrominoe();
        }

        private void MinigamePanel_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
            Graphics g = buffer.Graphics;
            try
            {
                g.Clear(Color.Black);
                for (int y = 0; y < 23; ++y)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (grid[y, x] == 1 | droppedtetrominoeLocationGrid[y, x] == 1)
                            g.FillRectangle(Brushes.White, new Rectangle(x * 10, y * 10, 10, 10));
                    }
                    g.DrawLine(new Pen(Color.DarkGray), new Point(0, (y + 1) * 10), new Point(10 * 10, (y + 1) * 10));
                }
                for (int x = 0; x < 10; x++)
                {
                    g.DrawLine(new Pen(Color.DarkGray), new Point((x + 1) * 10, 0), new Point((x + 1) * 10, 23 * 10));
                }
                Drawing.DrawSizedString(g, "Level " + level, 10, new PointF(150, 10), Brushes.White);
                Drawing.DrawSizedString(g, "Score " + score, 10, new PointF(150, 30), Brushes.White);
                Drawing.DrawSizedString(g, "LinesCleared " + linesCleared, 10, new PointF(150, 50), Brushes.White);
                Random random = new Random();
                if (minigameTime != minigamePrevTime)
                {
                    minigamePrevTime = minigameTime;
                    tet.Drop();
                    if (isDropped == true)
                    {
                        tet = nexttet;
                        nexttet = new Tetrominoe();
                        tet.Spawn();
                        isDropped = false;
                        score += 10;
                    }
                    int j; for (j = 0; j < 10; j++)
                    {
                        if (droppedtetrominoeLocationGrid[0, j] == 1)
                            _initGame();
                    }
                    Input();
                    ClearBlock();
                }
                buffer.Render();
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#else
                Console.WriteLine(ex.ToString());
#endif
            }
        }

        private static void ClearBlock()
        {
            int combo = 0;
            for (int i = 0; i < 23; i++)
            {
                int j; for (j = 0; j < 10; j++)
                {
                    if (droppedtetrominoeLocationGrid[i, j] == 0)
                        break;
                }
                if (j == 10)
                {
                    linesCleared++;
                    combo++;
                    Console.Beep(400, 200);
                    for (j = 0; j < 10; j++)
                    {
                        droppedtetrominoeLocationGrid[i, j] = 0;
                    }
                    int[,] newdroppedtetrominoeLocationGrid = new int[23, 10];
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            newdroppedtetrominoeLocationGrid[k + 1, l] = droppedtetrominoeLocationGrid[k, l];
                        }
                    }
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            droppedtetrominoeLocationGrid[k, l] = 0;
                        }
                    }
                    for (int k = 0; k < 23; k++)
                        for (int l = 0; l < 10; l++)
                            if (newdroppedtetrominoeLocationGrid[k, l] == 1)
                                droppedtetrominoeLocationGrid[k, l] = 1;
                }
            }
            score += (int)Math.Round(Math.Sqrt(Math.Max(combo * 50 - 50, 0)) * 5);
            level = (int)Math.Round(Math.Sqrt(score * 0.01)) + 1;
        }
        private static void Input()
        {
            if (Base.Input.Left & !tet.isSomethingLeft())
            {
                for (int i = 0; i < 4; i++)
                    tet.location[i][1] -= 1;
                tet.Update();
            }
            else if (Base.Input.Right & !tet.isSomethingRight())
            {
                for (int i = 0; i < 4; i++)
                    tet.location[i][1] += 1;
                tet.Update();
            }
            if (Base.Input.Down)
                tet.Drop();
            if (Base.Input.Up)
                for (; tet.isSomethingBelow() != true;)
                    tet.Drop();
            if (Base.Input.Action)
            {
                tet.Rotate();
                tet.Update();
            }
        }

        public class Tetrominoe
        {
            public static int[,] I = new int[1, 4] { { 1, 1, 1, 1 } };

            public static int[,] O = new int[2, 2] { { 1, 1 },
                                                 { 1, 1 } };

            public static int[,] T = new int[2, 3] { { 0, 1, 0 },
                                                 { 1, 1, 1 } };

            public static int[,] S = new int[2, 3] { { 0, 1, 1 },
                                                 { 1, 1, 0 } };

            public static int[,] Z = new int[2, 3] { { 1, 1, 0 },
                                                 { 0, 1, 1 } };

            public static int[,] J = new int[2, 3] { { 1, 0, 0 },
                                                 { 1, 1, 1 } };

            public static int[,] L = new int[2, 3] { { 0, 0, 1 },
                                                 { 1, 1, 1 } };
            public static List<int[,]> tetrominoes = new List<int[,]>() { I, O, T, S, Z, J, L };
            private readonly int[,] shape;
            public List<int[]> location = new List<int[]>();
            public Tetrominoe()
            {
                shape = tetrominoes[rnd.Next(0, tetrominoes.Count)];
            }
            public void Spawn()
            {
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.GetLength(1); j++)
                    {
                        if (shape[i, j] == 1)
                        {
                            location.Add(new int[] { i, (10 - shape.GetLength(1)) / 2 + j });
                        }
                    }
                }
                Update();
            }
            public void Drop()
            {
                if (isSomethingBelow())
                {
                    for (int i = 0; i < 4; i++)
                    {
                        droppedtetrominoeLocationGrid[location[i][0], location[i][1]] = 1;
                    }
                    isDropped = true;
                    Console.Beep(800, 200);
                }
                else
                {
                    for (int numCount = 0; numCount < 4; numCount++)
                    {
                        location[numCount][0] += 1;
                    }
                    Update();
                }
            }
            public void Rotate()
            {
                List<int[]> templocation = new List<int[]>();
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.GetLength(1); j++)
                    {
                        if (shape[i, j] == 1)
                        {
                            templocation.Add(new int[] { i, (10 - shape.GetLength(1)) / 2 + j });
                        }
                    }
                }
                if (shape == tetrominoes[0])
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2]);
                    }
                }
                else if (shape == tetrominoes[3])
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[3]);
                    }
                }
                else if (shape == tetrominoes[1])
                    return;
                else
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2]);
                    }
                }
                for (int count = 0; isOverlayLeft(templocation) != false | isOverlayRight(templocation) != false | isOverlayBelow(templocation) != false; count++)
                {
                    if (isOverlayLeft(templocation) == true)
                    {
                        for (int i = 0; i < location.Count; i++)
                        {
                            templocation[i][1] += 1;
                        }
                    }
                    if (isOverlayRight(templocation) == true)
                    {
                        for (int i = 0; i < location.Count; i++)
                        {
                            templocation[i][1] -= 1;
                        }
                    }
                    if (isOverlayBelow(templocation) == true)
                    {
                        for (int i = 0; i < location.Count; i++)
                        {
                            templocation[i][0] -= 1;
                        }
                    }
                    if (count == 3)
                    {
                        return;
                    }
                }
                location = templocation;
            }
            public bool notFalse(bool? inp) => (inp ?? true);
            public int[] TransformMatrix(int[] coord, int[] axis) => new int[] { axis[0] - axis[1] + coord[1], axis[0] + axis[1] - coord[0] };
            public bool isSomethingBelow()
            {
                for (int i = 0; i < 4; i++)
                {
                    if (location[i][0] + 1 >= 23)
                        return true;
                    if (location[i][0] + 1 < 23 & droppedtetrominoeLocationGrid[location[i][0] + 1, location[i][1]] == 1)
                        return true;
                }
                return false;
            }
            public bool? isOverlayBelow(List<int[]> location)
            {
                List<int> ycoords = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    ycoords.Add(location[i][0]);
                    if (location[i][0] >= 23)
                        return true;
                    if (location[i][0] < 0 | location[i][1] < 0 | location[i][1] > 9)
                        return null;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (ycoords.Max() - ycoords.Min() == 3)
                    {
                        if ((ycoords.Max() == location[i][0] | ycoords.Max() - 1 == location[i][0]) & (droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if ((ycoords.Max() == location[i][0]) & (droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            public bool isSomethingLeft()
            {
                for (int i = 0; i < 4; i++)
                {
                    if (location[i][1] == 0)
                        return true;
                    else if (droppedtetrominoeLocationGrid[location[i][0], location[i][1] - 1] == 1)
                        return true;
                }
                return false;
            }
            public bool? isOverlayLeft(List<int[]> location)
            {
                List<int> xcoords = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    xcoords.Add(location[i][1]);
                    if (location[i][1] < 0)
                        return true;
                    if (location[i][1] > 9)
                        return false;
                    if (location[i][0] >= 23 | location[i][0] < 0)
                        return null;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (xcoords.Max() - xcoords.Min() == 3)
                    {
                        if (xcoords.Min() == location[i][1] | xcoords.Min() + 1 == location[i][1])
                        {
                            if (droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (xcoords.Min() == location[i][1])
                        {
                            if (droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            public bool isSomethingRight()
            {
                for (int i = 0; i < 4; i++)
                {
                    if (location[i][1] == 9)
                        return true;
                    else if (droppedtetrominoeLocationGrid[location[i][0], location[i][1] + 1] == 1)
                        return true;
                }
                return false;
            }
            public bool? isOverlayRight(List<int[]> location)
            {
                List<int> xcoords = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    xcoords.Add(location[i][1]);
                    if (location[i][1] > 9)
                        return true;
                    if (location[i][1] < 0)
                        return false;
                    if (location[i][0] >= 23 | location[i][0] < 0)
                        return null;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (xcoords.Max() - xcoords.Min() == 3)
                    {
                        if ((xcoords.Max() == location[i][1] | xcoords.Max() - 1 == location[i][1]) & droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (xcoords.Max() == location[i][1] & droppedtetrominoeLocationGrid[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            public void Update()
            {
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        grid[i, j] = 0;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    grid[location[i][0], location[i][1]] = 1;
                }
            }
        }
    }
}
