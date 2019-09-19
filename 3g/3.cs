using Base;
using LaptopSimulator2015;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaptopSimulator2015.Goals
{
    public class G3 : Goal
    {
        private Image _installer;

        public string name => "DropBlox";

        public Image icon
        {
            get {
                if (_installer == null)
                {
                    string __installer = "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";
                    Stream s = new MemoryStream(Convert.FromBase64String(__installer));
                    _installer = Image.FromStream(s);
                    s.Close();
                }
                return _installer;
            }
        }

        public int availableAfter => 0;

        public int gameClock => 300;

        public Panel desktopIcon { get; set; }

        public int playableAfter => 4;

        public string[] availableText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return new string[] { "Dieses neue Spiel sieht unglaublich aus!", "Schaun' wir mal ob mein PC gut genug dafür ist..." };
                    default:
                        return new string[] { "My gosh, this new game will be so amazing!", "Lets see if my computer can handle it..." };
                }
            }
        }

        public string[] incompleteText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return new string[] { "Scheinbar nicht.", "Mal sehen... ", "Ich brauche eine neue CPU, GPU und mehr RAM...", "Diese Programme sollten meinen PC kompatibel machen!" };
                    default:
                        return new string[] { "Doesn't seem like it.", "Lets see...", "I will need a new CPU, GPU and more RAM...", "These programs should make my PC compatible!" };
                }
            }
        }

        public string[] completeText
        {
            get {
                switch (CultureInfo.CurrentUICulture.Name.Split('-')[0])
                {
                    case "de":
                        return new string[] { "Ich sollte es jetzt nochmal versuchen..." };
                    default:
                        return new string[] { "I should try again now..." };
                }
            }
        }

        public static uint minigamePrevTime;
        
        public static int[,] grid = new int[23, 10];
        public static int[,] droppedtetrominoeLocationGrid = new int[23, 10];
        public static bool isDropped = false;
        static Tetrominoe tet;
        static Tetrominoe nexttet;
        public static int linesCleared = 0, score = 0, level = 1;
        public static Random rnd;
        public void gameTick(Graphics e, Panel minigamePanel, Timer minigameTimer, uint minigameTime)
        {
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e, new Rectangle(0, 0, minigamePanel.Width, minigamePanel.Height));
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
                            Misc.closeGameWindow.Invoke();
                    }
                    Input();
                    ClearBlock();
                }
                buffer.Render();
                buffer.Dispose();
            }
            catch (Exception ex) { if (ex.InnerException?.Message == "0717750f-3508-4bc2-841e-f3b077c676fe") Misc.closeGameWindow.Invoke(); else Console.WriteLine(ex.ToString()); }
        }

        public void initGame(Graphics g, Panel minigamePanel, Timer minigameTimer)
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
