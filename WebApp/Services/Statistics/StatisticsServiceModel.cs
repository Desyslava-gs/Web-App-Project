﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services.Statistics
{
    public class StatisticsServiceModel
    {
        public int AllCars { get; set; }
        public int AllRepairs { get; set; }
        public int FinishedRepairs { get; set; }
        public int AllClients { get; set; }
    }
}