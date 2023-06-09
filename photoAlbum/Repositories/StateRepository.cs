using photoAlbum.Activities;
using photoAlbum.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace photoAlbum.ViewModels
{
    public class StateRepository
    {
        public async Task<ObservableCollection<State>>  GetStateCollection()
        {
            var StateCollection = new ObservableCollection<State>();
            using (FetchDataClass fd = new FetchDataClass())
            {
                var result = await fd.ReadStateInfo();
                if (result != null)
                {
                    foreach (var s in result)
                        StateCollection.Add(new State
                        {
                            Code = s.Code,
                            Name = s.Name
                        });
                   
                }
                return StateCollection;
            }
        }


        //public async Task<List<State>> GetStateList()
        //{
        //    var StateList = new List<State>();
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var result = await fd.FetchAllStateRecprds();
        //        if (result != null)
        //        {
        //            foreach (var s in result)
        //                StateList.Add(new State
        //                {
        //                    id = s.id,
        //                    stateCode = s.stateCode,
        //                    stateName = s.stateName
        //                });

        //        }
        //        return StateList;
        //    }
        //}

        //public async Task<State> GetStateName(string Code)
        //{
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var result = await fd.FetchAllStateRecprds();
        //        if (result != null)
        //        { 
        //            var state = result.FirstOrDefault(x => x.stateCode.Equals(Code));
        //            return new State
        //            {
        //                id = state.id,
        //                stateName = state.stateName,
        //                stateCode = state.stateCode
        //            };
        //        }
        //        return null; ;
        //    }
        //}



        //public async Task<ObservableCollection<LocalGovernment>> GetLGACollection()
        //{
        //    var LGACollection = new ObservableCollection<LocalGovernment>();
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var result = await fd.FetchLGARecords();
        //        if (result != null)
        //        {
        //            foreach (var s in result)
        //                LGACollection.Add(new LocalGovernment
        //                {
        //                    LGAId = s.LGAId,
        //                    LGAName = s.LGAName
        //                });

        //            return LGACollection;
        //        }
        //    }
        //    return null;
        //}


        //public async Task<List<LocalGovernment>> GetLGAList()
        //{
        //    var LGAList = new List<LocalGovernment>();
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var result = await fd.FetchLGARecords();
        //        if (result != null)
        //        {
        //            foreach (var s in result)
        //                LGAList.Add(new LocalGovernment
        //                {
        //                    LGAId = s.LGAId,
        //                    LGAName = s.LGAName,
        //                    stateCode = s.stateCode
        //                });

        //            return LGAList;
        //        }
        //    }
        //    return null;
        //}

        //public async Task<ObservableCollection<LocalGovernment>> GetLGACollection(string stateCode)
        //{
        //    var LGACollection = new ObservableCollection<LocalGovernment>();
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var response = await fd.FetchLGARecords();
        //        if (response != null)
        //        {
        //            var result = response.Where(x => x.stateCode.Equals(stateCode)).ToList();
        //            foreach (var s in result)
        //                LGACollection.Add(new LocalGovernment
        //                {
        //                    LGAId = s.LGAId,
        //                    LGAName = s.LGAName
        //                });

        //            return LGACollection;
        //        }
        //    }
        //    return null;
        //}

        //public async Task<List<LocalGovernment>> GetLGAList(string stateCode)
        //{
        //    var LGAList = new List<LocalGovernment>();
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var response = await fd.FetchLGARecords();
        //        if (response != null)
        //        {
        //            var result = response.Where(x => x.stateCode.Equals(stateCode)).ToList();
        //            foreach (var s in result)
        //                LGAList.Add(new LocalGovernment
        //                {
        //                    LGAId = s.LGAId,
        //                    LGAName = s.LGAName,
        //                    stateCode = s.stateCode
        //                });

        //            return LGAList;
        //        }
        //    }
        //    return null;
        //}

        //public async Task<LocalGovernment> GetLGA(string LGAId)
        //{
        //    using (FetchDataClass fd = new FetchDataClass())
        //    {
        //        var response = await fd.FetchLGARecords();
        //        if (response != null)
        //        {
        //            var s = response.FirstOrDefault(x => x.LGAId.Equals(LGAId));
        //            return new LocalGovernment
        //            {
        //                LGAId = s.LGAId,
        //                LGAName = s.LGAName,
        //                stateCode = s.stateCode
        //            };
 
        //        }
        //    }
        //    return null;
        //}
    }
}
