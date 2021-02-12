using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading;

namespace Examples
{
    static class Program
    {
        public static string levelName;
        static void Main(string[] args)
        {
            Console.Title = "Geometry Dash Level Preset Generator";


            Console.WriteLine("Input your level name. (Case Sensitive) (WARNING THIS WILL OVERRIDE THE LEVEL'S CONTENTS)");
            levelName = Console.ReadLine();

            Console.WriteLine("Generating level...");
            F();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Level generated! You can open Geometry Dash now.");
            //System.Threading.Thread.Sleep(20000);
            //TrollFace();
            Console.ReadKey();
        }

        private static void F()
        {
            //important shit
            Random rand = new Random();
            var local = new LocalLevels();
            GameManager gameManager = new GameManager();

            //random shit
            var gameModes = new List<GameMode> { GameMode.Cube, GameMode.Ship, GameMode.Ball, GameMode.Dird, GameMode.Robot, GameMode.Spider };
            var speedTypes = new List<SpeedType> { SpeedType.Yellow, SpeedType.Default, SpeedType.Green, SpeedType.Purple, SpeedType.Red };

            Level level = new Level(local.GetLevel(levelName));
            byte BGColorR = (byte)rand.Next(255); Console.WriteLine("R: " + BGColorR);
            byte BGColorB = (byte)rand.Next(255); Console.WriteLine("B: " + BGColorB);
            byte BGColorG = (byte)rand.Next(255); Console.WriteLine("G: " + BGColorG);

            level.AddColor(new Color(ColorType.Background, BGColorR, BGColorB, BGColorG));
            //Code doesnt work for rn, doesnt match bg color
            level.AddColor(new Color(ColorType.Ground, (byte)(BGColorR - (byte)rand.Next(40)), (byte)(BGColorB - (byte)rand.Next(40)), (byte)(BGColorG - (byte)rand.Next(40))));
            level.AddColor(new Color(ColorType.Ground2, (byte)(BGColorR - (byte)rand.Next(70)), (byte)(BGColorB - (byte)rand.Next(70)), (byte)(BGColorG - (byte)rand.Next(70))));

            byte[] randomInts = { (byte)rand.Next(20), (byte)rand.Next(17), (byte)rand.Next(speedTypes.Count), (byte)rand.Next(gameModes.Count), (byte)rand.Next(12) };
            bool[] randomBools = { rand.Next(2) == 1, rand.Next(2) == 1, rand.Next(2) == 1 };

            level.Background = randomInts[0];                Console.WriteLine("Background ID: " + randomInts[0]);
            level.Ground = randomInts[1];                    Console.WriteLine("Ground ID: " + randomInts[1]);
            level.PlayerSpeed = (SpeedType)randomInts[2];    Console.WriteLine("Player Speed: " + (SpeedType)randomInts[2]);
            level.GameMode = (GameMode)randomInts[3];        Console.WriteLine("GameMode: " + (GameMode)randomInts[3]);
            level.Fonts = randomInts[4];                     Console.WriteLine("Font ID: " + (randomInts[4]));
            level.kA17 = 0;
            level.TwoPlayerMode = (bool)randomBools[0];      Console.WriteLine("Two Player Mode: " + (randomBools[0]));
            level.Mini = (bool)randomBools[1];               Console.WriteLine("Mini Player: " + (randomBools[1]));
            level.Dual = (bool)randomBools[2];               Console.WriteLine("Dual Mode: " + (randomBools[2]));

            //Save the level.
            local.GetLevel(levelName).LevelString = level.ToString(); 
            local.GetLevel(levelName).Description = "Random Description"; 
            local.GetLevel(levelName).TotalJumps = rand.Next(99999);
            local.GetLevel(levelName).TotalAttempts = rand.Next(99999);
            local.Save();

        }

        static private void TrollFace()
        {
            Console.WriteLine("     ▄▄▄▄▀▀▀▀▀▀▀▀▄▄▄▄▄▄");
            Console.WriteLine("░░░░█░░░░▒▒▒▒▒▒▒▒▒▒▒▒░░▀▀▄");
            Console.WriteLine("░░░█░░░▒▒▒▒▒▒░░░░░░░░▒▒▒░░█");
            Console.WriteLine("░░█░░░░░░▄██▀▄▄░░░░░▄▄▄░░░█");
            Console.WriteLine("░▀▒▄▄▄▒░█▀▀▀▀▄▄█░░░██▄▄█░░░█");
            Console.WriteLine("█▒█▒▄░▀▄▄▄▀░░░░░░░░█░░░▒▒▒▒▒█");
            Console.WriteLine("█▒█░█▀▄▄░░░░░█▀░░░░▀▄░░▄▀▀▀▄▒█");
            Console.WriteLine("░█▀▄░█▄░█▀▄▄░▀░▀▀░▄▄▀░░░░█░░█");
            Console.WriteLine("░░█░░▀▄▀█▄▄░█▀▀▀▄▄▄▄▀▀█▀██░█");
            Console.WriteLine("░░░█░░██░░▀█▄▄▄█▄▄█▄████░█");
            Console.WriteLine("░░░░█░░░▀▀▄░█░░░█░███████░█");
            Console.WriteLine("░░░░░▀▄░░░▀▀▄▄▄█▄█▄█▄█▄▀░░█");
            Console.WriteLine("░░░░░░░▀▄▄░▒▒▒▒░░░░░░░░░░█");
            Console.WriteLine("░░░░░░░░░░▀▀▄▄░▒▒▒▒▒▒▒▒▒▒░█");
            Console.WriteLine("░░░░░░░░░░░░░░▀▄▄▄▄▄░░░░░█");
        }
    }
}
