using photoAlbum.Activities;
using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Repositories
{
    public class SchoolsRepository
    {
        public async Task<List<FinModel>> FetchSchoolsToPreview(string Search)
        {
            using(FetchDataClass fd= new FetchDataClass())
            {
                var result =(string.IsNullOrWhiteSpace(Search))? await fd.FetchSchoolsToPreview() : await fd.FetchSchoolsToPreview(Search);
                return result;
            }
        }

        
    }
}
