﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using LogicLayer;

namespace EscapeFromTheWoods
{
   public  class Forest
    {
        public int Id;
        public int maxWidth { get; set; } 
        public int maxHeight { get; set; } 
        public int TreesInforest = 0; 
        public int monkeys = 0;
        public int monkeyId { get; set; } = DataLayer.dbFunctions.GetMonkey();
        private Random _random = new Random();
        public List<Tree> treelist = new List<Tree>();
        public List<Monkey> Monkeylist = new List<Monkey>();
        LoggingLogic logging = new LoggingLogic();
        public Bitmap image = null;
        private Forest(int id, int maxWidth, int maxHeight, int trees, int monkeys)
        {
            this.Id = id;
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.TreesInforest = trees;
            this.monkeys = monkeys;
        }

        public static Forest BuildForest(int id , int maxWidth, int maxHeight, int trees, int monkeys)
        {
            Forest forest = new Forest(id, maxWidth, maxHeight, trees, monkeys);
            return forest;
        }

        public void GenerateTrees()
        {
            for (int i = 0; i < TreesInforest; i++)
            {
                Tree t = new Tree(i, _random.Next(0, maxWidth), _random.Next(0, maxHeight));
                if (treelist.Contains(t))
                {
                    i--;
                }
                else
                {
                    treelist.Add(t);
                    logging.MakeForestLog(this, t);

                }
             this.image =  logging.CreateImage(this);
            }
        }

        public void PlaceMonkey()
        {
            for (int i = 0; i < monkeys; i++)
            {
                    int RandomTree = _random.Next(treelist.Count);
                    string name = Config.monkeyNames.ElementAt(_random.Next(Config.monkeyNames.Count));
                    Monkey m = new Monkey(monkeyId++, name, treelist[RandomTree]); 
                    var emptyTree = treelist.Where(x => x.occupied == false);
                    if (emptyTree.Contains(treelist[RandomTree]))
                    {
                        m.setTree(treelist[RandomTree]);
                        treelist[RandomTree].occupied = true;
                        Monkeylist.Add(m);
                         m.VisitedTrees.Add(m.tree);
                        for (int i2 = 0; i2 < Monkeylist.Count; i2++)
                        {
                        logging.DrawMonkey(image,m);
                        logging.MakeMonkeyLog(this, m.tree, m); 
                        } 
                    }
                    else
                    {
                        i--;
                    }
         
            }

        }

    }
}
 