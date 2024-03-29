﻿using Lab2.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface ICommentService
    {
        IEnumerable<GetCommentsDto> GetComments(string text = "");
    }
}
