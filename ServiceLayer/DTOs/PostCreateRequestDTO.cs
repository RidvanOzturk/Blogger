using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs;
public class PostCreateRequestDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Username { get; set; }
}