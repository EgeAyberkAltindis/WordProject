
using BLL.Services.Concretes;
using Model.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IEnglishWordService:IServiceManager<EnglishWord>
    {
        
        Task<bool>EnglishWordExistsAsync (string word);
        IQueryable<EnglishWord> GetAllWithIncludes();

        

    }
}
