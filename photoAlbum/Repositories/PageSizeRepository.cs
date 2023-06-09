using photoAlbum.Activities;
using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Repositories
{
    public class PageSizeRepository
    {
        public async Task<List<PageSizeModel>> FetchPageSize()
        {
            using (FetchDataClass fd = new FetchDataClass())
            {
                var result = await fd.FetchPageSize();
                return result;
            }
        }
    }
}
