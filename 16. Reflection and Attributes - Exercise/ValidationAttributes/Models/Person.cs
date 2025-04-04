﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Models
{
    using ValidationAttributes.Attributes;

    public class Person
    {
        public Person(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
        }

        [MyRequired] public string FullName { get; }
        [MyRange(minValue: 12, maxValue: 90)] public int Age { get; }
    }
}
