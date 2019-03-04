using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace WindowsFormsApp1.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=StudentDatabase")
        {
                    
        }

        public virtual DbSet<StudentInformation> Students { get; set; }
    }
}
