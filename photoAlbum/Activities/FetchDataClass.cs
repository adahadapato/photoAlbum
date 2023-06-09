using Newtonsoft.Json;
using photoAlbum.DB;
using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Activities
{
    public class FetchDataClass:IDisposable
    {
        public async Task<albumModel> FetchAlbum(string FileName)
        {
            return await Task.Run(() =>
            {
                var Json = LoadJson(FileName);
                albumModel model = JsonConvert.DeserializeObject<albumModel>(Json);
                return model;
            });
        }

        public async Task<List<FinModel>> FetchSchoolsToPreview(string Search)
            => await Task.Run(() => FoxProDalFactory.GetDal(GrConnector.FoxProDal).FetchSchoolsToPreview(Search));

        public async Task<List<FinModel>> FetchSchoolsToPreview(string stateCode, string Search)
            => await Task.Run(() => FoxProDalFactory.GetDal(GrConnector.FoxProDal).FetchSchoolsToPreview(stateCode, Search));

        public async Task<List<State>> ReadStateInfo()
            => await FoxProDalFactory.GetDal(GrConnector.FoxProDal).ReadStateInfo();

        public async Task<State> ReadStateInfo(string code)
           => await FoxProDalFactory.GetDal(GrConnector.FoxProDal).ReadStateInfo(code);

        public async Task<FinModel> FetchSchoolToPreview(string Search)
           => await Task.Run(() => FoxProDalFactory.GetDal(GrConnector.FoxProDal).FetchSchoolToPreview(Search));//

        public async Task<List<AlbumPersonalInfo>> ReadAlbumPersonalInfo(string schnum)
            => await FoxProDalFactory.GetDal(GrConnector.FoxProDal).ReadAlbumPersonalInfo(schnum);
        
        public async Task<List<FinModel>> FetchSchoolsToPreview()
            => await FoxProDalFactory.GetDal(GrConnector.FoxProDal).FetchSchoolsToPreview();

        private string LoadJson(string FileName)
        {
            using (StreamReader r = new StreamReader(FileName))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

        public async Task<List<PageSizeModel>> FetchPageSize()
        {
            return await Task.Run(() =>
            {
                List<PageSizeModel> lst = new List<PageSizeModel>();
                for (int i = 5; i < 101; i++)
                {
                    lst.Add(new PageSizeModel
                    {
                        size = i
                    });
                }
                return lst;
            });
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FetchDataClass() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
