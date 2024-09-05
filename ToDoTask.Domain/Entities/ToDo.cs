using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTask.Domain.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public bool Completed { get; set; } = false;
        public  int  UserId { get; set; }
        public User User { get; set; }
    }
   
    
}
