using Core;
using Interfaces;
using System;
namespace InputProcessing
{
    class Parser : Interfaces.IParser
    {
        public User Parse(string line)
        {
            var data = line.Split('\t');
            return new User(data[0], data[1]);
        }

      
    }
}