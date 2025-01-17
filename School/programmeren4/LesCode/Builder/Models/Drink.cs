﻿using System;
using System.Collections.Generic;
using System.Text;
using Builder.Interfaces;

namespace Builder.Models
{
   public abstract class Drink : IItem
    {
        public abstract string Name { get; }
        public abstract float Price { get; }

        public IPacking Packing => new Bottle();
    }
}
