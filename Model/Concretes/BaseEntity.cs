using Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Concretes
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            CreatedDate= DateTime.Now;
        }
        public int Id { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
    }
}
