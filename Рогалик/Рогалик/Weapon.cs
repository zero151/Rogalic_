﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Рогалик
{
    public class Weapon
    {
        public string name;
        public int damage;
        public Weapon(string n, int dmg)
        {
            this.name = n;
            this.damage = dmg;
        }
    }
}

