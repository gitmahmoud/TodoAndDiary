﻿using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TodoAgg
{
    public interface ITodoRepository : IRepository<Todo>
    {

    }
}
