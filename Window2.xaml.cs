﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TiltGame
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private StringBuilder outputBuilder = new StringBuilder();
        public Window2()
        {
            InitializeComponent();
            Console.SetOut(new StringWriter(outputBuilder));

        }

        private void StartAlgorithmButton_Click(object sender, RoutedEventArgs e)
        {
            outputBuilder.Clear();
            //this.Background = System.Windows.Media.Brushes.Black;
            Stopwatch stopwatch = Stopwatch.StartNew();


            bool ex = bfs();
            OutputTextBlock.Text = outputBuilder.ToString();
        }
        private bool bfs()
        {
            const int mod = 1000000007;

            static int[] GeneratePrimes(int count)
            {
                int limit = count * 20; // An estimation of the upper limit
                bool[] sieve = new bool[limit + 1]; // Initialize sieve array
                int[] primes = new int[count];
                int primeCount = 0;

                // Initialize all elements of the sieve array to true, indicating prime
                for (int i = 2; i <= limit; i++)
                {
                    sieve[i] = true;
                }

                // Sieve of Eratosthenes algorithm
                for (int p = 2; p * p <= limit; p++)
                {
                    // If sieve[p] is true, then it is a prime number
                    if (sieve[p] == true)
                    {
                        // Mark all multiples of p as composite numbers
                        for (int i = p * p; i <= limit; i += p)
                        {
                            sieve[i] = false;
                        }
                    }
                }

                // Collect prime numbers
                for (int i = 2; i <= limit && primeCount < count; i++)
                {
                    if (sieve[i] == true)
                    {
                        primes[primeCount] = i;
                        primeCount++;
                    }
                }

                return primes;
            }

            string filePath = FilePathTextBox.Text;
            string[] str = new string[1000];
            int n = 0, col = 0, row = 0, targetCellHash;



            if (File.Exists(filePath))
            {
                // Open the file for reading
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read the contents of the file line by line
                    string line;
                    int j = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Process each line (e.g., print it)
                        if (j == 1)
                        {

                            n = int.Parse(line);
                            str = new string[n];
                        }
                        else if (j <= n + 1 && j > 1)
                        {
                            str[j - 2] = line;
                        }
                        else
                        {
                            string s = "", s2 = "";
                            int ii = 0;
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == ',')
                                {
                                    ii++;
                                }
                                else if (ii == 0)
                                {
                                    s += line[i];
                                }
                                else
                                {
                                    s2 += line[i];
                                }

                            }


                            col = int.Parse(s);
                            row = int.Parse(s2);
                        }

                        j++;
                    }
                }
            }

            //Console.WriteLine(row + " " + col);

            int[] primes = GeneratePrimes(2 * n);
            List<int> balls = new List<int>();
            List<int> mainballs = new List<int>();
            string[] firststr = new string[n];
            int[,] posToHash = new int[n, n];
            long maxSize = primes[2 * n - 1] * primes[n] + 1;
            Tuple<int, int>[] hashToPos = new Tuple<int, int>[maxSize];
            for (int i = 0; i < n; i++)
            {

                str[i] = str[i].Replace(",", "");
                str[i] = str[i].Replace(" ", "");
                firststr[i] = str[i];
                firststr[i] = firststr[i].Replace("o", ".");
                //Console.WriteLine(firststr[i]);
                for (int j = 0; j < n; j++)
                {
                    posToHash[i, j] = primes[i + n] * primes[j];
                    hashToPos[primes[i + n] * primes[j]] = Tuple.Create(i, j);
                    if (str[i][j] == 'o')
                    {
                        balls.Add(posToHash[i, j]);
                        mainballs.Add(posToHash[i, j]);

                    }

                }

            }
            targetCellHash = posToHash[row, col];
            int[,] r = new int[n, n];
            int[,] l = new int[n, n];
            int[,] u = new int[n, n];
            int[,] d = new int[n, n];
            void right()
            {
                int pts;
                for (int i = 0; i < n; i++)
                {
                    pts = posToHash[i, n - 1];
                    for (int j = n - 1; j >= 0; j--)
                    {
                        if (str[i][j] == '#' && j != 0)
                        {
                            pts = posToHash[i, j - 1];
                        }
                        r[i, j] = pts;
                    }


                }
            }
            void left()
            {
                int pts;
                for (int i = 0; i < n; i++)
                {

                    pts = posToHash[i, 0];
                    for (int j = 0; j < n; j++)
                    {

                        if (str[i][j] == '#' && j != (n - 1))
                        {
                            pts = posToHash[i, j + 1];
                        }
                        l[i, j] = pts;
                    }
                }
            }
            void up()
            {
                int pts;
                for (int i = 0; i < n; i++)
                {

                    pts = posToHash[0, i];
                    for (int j = 0; j < n; j++)
                    {
                        if (str[j][i] == '#' && j != (n - 1))
                        {
                            pts = posToHash[j + 1, i];
                        }
                        u[j, i] = pts;

                    }


                }

            }
            void down()
            {
                int pts;
                for (int i = 0; i < n; i++)
                {

                    pts = posToHash[n - 1, i];
                    for (int j = n - 1; j >= 0; j--)
                    {
                        if (str[j][i] == '#' && j != 0)
                        {
                            pts = posToHash[j - 1, i];
                        }
                        d[j, i] = pts;
                    }

                }
            }
            left();
            up();
            right();
            down();

            int[,] dirOfTiltArr = new int[n, n];
            long stateOfBoard = getStateOfBoard(balls);// sum of all hash value of cells with sliders
            int destHashVal;
            Dictionary<long, bool> visited = new Dictionary<long, bool>(n * n);
            //bool[] visited = new bool[maxSize];
            //state of board, last direction the board was tilted towards
            Queue<Tuple<char, string>> qt = new Queue<Tuple<char, string>>();
            //list of balls hashed values
            Queue<List<int>> ql = new Queue<List<int>>();

            bool bfs()
            {



                visited[stateOfBoard] = true;

                qt.Enqueue(Tuple.Create('a', ""));
                ql.Enqueue(balls);

                List<char> dir = new List<char> { 'l', 'r', 'u', 'd' };
                List<int> ballsPos;




                char pastDir;
                string history;
                //Tuple<int, int> destPos;
                //long cntt = 0;
                while (qt.Count != 0)
                {
                    pastDir = qt.First().Item1;
                    history = qt.Dequeue().Item2;
                    balls = ql.Dequeue();


                    for (int i = 0; i < 4; i++)
                    {
                        if (pastDir == dir[i])
                        {
                            continue;
                        }
                        //Console.WriteLine(cntt++);
                        ballsPos = new List<int>(balls);
                        dirOfTiltArr = getArray(dir[i]);
                        //bool[,] reserved = new bool[n, n];
                        //int[,] newPos = new int[n, n];
                        Dictionary<int, bool> reserved = new Dictionary<int, bool>(balls.Count);
                        Dictionary<int, int> newPos = new Dictionary<int, int>(balls.Count);
                        stateOfBoard = 1;
                        for (int j = 0; j < ballsPos.Count; j++)
                        {

                            destHashVal = dirOfTiltArr[hashToPos[ballsPos[j]].Item1, hashToPos[ballsPos[j]].Item2];
                            //destPos = Tuple.Create(hashToPos[destHashVal].Item1, hashToPos[destHashVal].Item2);
                            if (!reserved.ContainsKey(destHashVal))
                            {
                                ballsPos[j] = destHashVal;
                                reserved[destHashVal] = true;
                                newPos[destHashVal] = destHashVal;
                                //Console.WriteLine(destHashVal);
                            }
                            else
                            {
                                newPos[destHashVal] = getPrevPosHashVal(dir[i], newPos[destHashVal]);

                                ballsPos[j] = newPos[destHashVal];

                            }

                            if (ballsPos[j] == targetCellHash)
                            {
                                history += dir[i];
                                Console.WriteLine("Solvable");
                                Console.WriteLine("Min number of moves: " + history.Length);
                                Console.Write("Sequence of moves: ");
                                for (int k = 0; k < history.Length; k++)
                                {
                                    Console.Write(getchar(history[k]) + ",");
                                }
                                Console.WriteLine();
                                if (n < 10)
                                {
                                    draw(history);
                                }


                                return true;
                            }
                            stateOfBoard = ((stateOfBoard % mod) * (ballsPos[j] % mod)) % mod;
                        }
                        //Console.WriteLine(dir[i]);
                        if (!visited.ContainsKey(stateOfBoard))
                        {

                            visited[stateOfBoard] = true;
                            qt.Enqueue(Tuple.Create(dir[i], history + dir[i]));
                            ql.Enqueue(new List<int>(ballsPos));
                        }
                    }
                }
                return false;
            }

            long getStateOfBoard(List<int> balls)
            {
                long SOB = 1;
                for (int i = 0; i < balls.Count; ++i)
                {
                    SOB = ((SOB % mod) * (balls[i] % mod)) % mod;
                }
                return SOB;
            }

            int[,] getArray(char c)
            {
                if (c == 'l')
                {
                    return l;
                }
                else if (c == 'r')
                {
                    return r;
                }
                else if (c == 'u')
                {
                    return u;
                }
                else
                {
                    return d;
                }
            }

            string getchar(char c)
            {
                if (c == 'l')
                {
                    return "left";
                }
                else if (c == 'r')
                {
                    return "right";
                }
                else if (c == 'd')
                {
                    return "down";
                }
                else
                {
                    return "up";
                }
            }

            int getPrevPosHashVal(char c, int currHash)
            {
                Tuple<int, int> pos = hashToPos[currHash];

                if (c == 'l')
                {
                    //Console.WriteLine(pos.Item1 + ", " + pos.Item2 + 1);
                    if (pos.Item2 + 1 < n)
                    {

                        return posToHash[pos.Item1, pos.Item2 + 1];
                    }
                }
                else if (c == 'r')
                {
                    if (pos.Item2 - 1 >= 0)
                    {
                        return posToHash[pos.Item1, pos.Item2 - 1];
                    }
                }
                else if (c == 'u')
                {
                    if (pos.Item1 + 1 < n)
                    {
                        return posToHash[pos.Item1 + 1, pos.Item2];
                    }
                }
                else
                {
                    if (pos.Item1 - 1 >= 0)
                    {
                        return posToHash[pos.Item1 - 1, pos.Item2];
                    }
                }
                return currHash;
            }


            void draw(string history)
            {

                Console.WriteLine("Initial");
                for (int qw = 0; qw < n; qw++)
                {
                    for (int wq = 0; wq < n; wq++)
                    {
                        Console.Write(str[qw][wq]);
                        if (wq != n - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                for (int k = 0; k < history.Length; k++)
                {
                    Console.WriteLine(getchar(history[k]));

                    //ballsPos = new List<int>(mainballs);
                    dirOfTiltArr = getArray(history[k]);
                    //bool[,] reserved = new bool[n, n];
                    //int[,] newPos = new int[n, n];
                    Dictionary<int, bool> reserv = new Dictionary<int, bool>(balls.Count);
                    Dictionary<int, int> newPo = new Dictionary<int, int>(balls.Count);
                    stateOfBoard = 1;
                    char[,] outt = new char[n, n];
                    for (int t = 0; t < n; t++)
                    {
                        for (int u = 0; u < n; u++)
                            outt[t, u] = firststr[t][u];
                    }
                    for (int b = 0; b < mainballs.Count; b++)
                    {

                        destHashVal = dirOfTiltArr[hashToPos[mainballs[b]].Item1, hashToPos[mainballs[b]].Item2];
                        //destPos = Tuple.Create(hashToPos[destHashVal].Item1, hashToPos[destHashVal].Item2);
                        if (!reserv.ContainsKey(destHashVal))
                        {
                            mainballs[b] = destHashVal;
                            reserv[destHashVal] = true;
                            newPo[destHashVal] = destHashVal;
                            //Console.WriteLine(destHashVal);
                            outt[hashToPos[mainballs[b]].Item1, hashToPos[mainballs[b]].Item2] = 'o';
                        }
                        else
                        {
                            newPo[destHashVal] = getPrevPosHashVal(history[k], newPo[destHashVal]);

                            mainballs[b] = newPo[destHashVal];

                            outt[hashToPos[mainballs[b]].Item1, hashToPos[mainballs[b]].Item2] = 'o';

                        }

                    }
                    for (int we = 0; we < n; we++)
                    {
                        for (int ew = 0; ew < n; ew++)
                        {
                            Console.Write(outt[we, ew]);
                            if (ew != n - 1)
                                Console.Write(", ");
                        }

                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }



            Stopwatch stopwatch = Stopwatch.StartNew();



            ;
            bool ex = bfs();
            if (!ex)
            {
                Console.WriteLine("Unsolvable");
            }
            // stop the stopwatch
            stopwatch.Stop();

            // output the elapsed time
            Console.WriteLine($"execution time: {stopwatch.ElapsedMilliseconds / 1000} s");

            return false;
        }


    }
}
