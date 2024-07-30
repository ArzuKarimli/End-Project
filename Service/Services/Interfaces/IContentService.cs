﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IContentService
    {
        Task<IEnumerable<Content>> GetAllAsync();
    }
}
