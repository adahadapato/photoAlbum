using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.DB
{
    public interface IGRFoxProDal
    {
        Task<List<FinModel>> FetchSchoolsToPreview();
        Task<List<FinModel>> FetchSchoolsToPreview(string StateCode);
        Task<List<FinModel>> FetchSchoolsToPreview(string StateCode, string SchoolNo);
        Task<List<AlbumPersonalInfo>> ReadAlbumPersonalInfo(string schnum);
        Task<List<State>> ReadStateInfo();
        Task<State> ReadStateInfo(string code);
        Task<FinModel> FetchSchoolToPreview(string SchoolNo);
    }
}
