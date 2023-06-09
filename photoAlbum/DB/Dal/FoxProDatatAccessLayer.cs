using photoAlbum.Models;
using System;
using System.Collections.Generic;
//using System.Data;
//using System.Data.OleDb;
//using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialExplorer.IO.FastDBF;
using System.IO;
using photoAlbum.Tools;
using photoAlbum.Utils;

namespace photoAlbum.DB.Dal
{
    public class FoxProDatatAccessLayer : IGRFoxProDal
    {
        //public readonly string CONNECTION_STRING;
        //public OdbcConnection dbConection;
        string APP_PATH = EntryPoint.DataBasePath;// @"c:\works\photoalbum\ssce2020\";

        private FoxProDatatAccessLayer()
        {
            //CONNECTION_STRING = @"Driver ={ Microsoft Visual FoxPro Driver}; SourceType = DBC; SourceDB = "+ APP_PATH + "; Exclusive = No; NULL = NO; Collate = Machine; BACKGROUNDFETCH = NO; DELETED = NO;";
            ////@"Provider = VFPOLEDB.1; DATA Source = " + APP_PATH;
            //dbConection = new OdbcConnection(CONNECTION_STRING);
        }

        public  async Task<List<FinModel>> FetchSchoolsToPreview()
        {
            return await Task.Run(() =>
            {
                var fin = new List<FinModel>();
                //string sql = "SELECT * FROM fin ORDER BY schnum";
                ////using (OdbcCommand cmd = new OdbcCommand(sql, dbConection))
                ////{
                ////    OdbcDataReader dr = cmd.ExecuteReader();
                ////    if (dr.HasRows)
                ////    {
                ////        while (dr.Read())
                ////        {
                ////            fin.Add(new FinModel
                ////            {
                ////                schnum = dr["schnum"].ToString(),
                ////                sch_name = dr["sch_name"].ToString().Trim(),
                ////                state_name = dr["state_name"].ToString().Trim(),
                ////                cust_code = dr["cust_code"].ToString(),
                ////                custodian = dr["custodian"].ToString(),
                ////                lga = dr["town"].ToString(),
                ////            });
                ////        }
                ////    }
                ////}
                return fin;
            });
           
        }


        public async Task<List<FinModel>> FetchSchoolsToPreview(string StateCode, string SchoolNo)
        {

            return await Task.Run(() =>
            {

                var data = Path.Combine(APP_PATH, "fin.DBF");
                DbfFile dbf = new DbfFile(encoding);
                try
                {
                    dbf.Open(data, FileMode.Open);

                    //output values for all but binary and memo...
                    DbfRecord orec = new DbfRecord(dbf.Header);
                    var fin = new List<FinModel>();
                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            if (string.IsNullOrWhiteSpace(StateCode))
                            {
                                var schnum = orec["SCHNUM"];
                                if (schnum.StartsWith(SchoolNo))
                                {
                                    var cands = Convert.ToInt32(orec["NOFCAND"]);
                                    if (cands > 0)
                                    {
                                        fin.Add(new FinModel
                                        {
                                            schnum = orec["SCHNUM"].ToString(),
                                            sch_name = StrPaginationUtil.Strip(orec["SCH_NAME"].ToString().Trim()),
                                            state_name = orec["STATE_NAME"].ToString().Trim(),
                                            cust_code = orec["CUST_CODE"].ToString(),
                                            custodian = StrPaginationUtil.Strip(orec["CUSTODIAN"].ToString().Trim()),
                                            lga = orec["TOWN"].ToString(),
                                            Candidates = Convert.ToInt32(orec["NOFCAND"])
                                        });
                                    }
                                }

                            }
                            else
                            {
                                var seek = orec["STATE"];
                                var cands = Convert.ToInt32(orec["NOFCAND"]);
                                var schnum = orec["SCHNUM"];
                                if (seek == StateCode && cands > 0 && schnum.StartsWith(SchoolNo))
                                {
                                    fin.Add(new FinModel
                                    {
                                        schnum = orec["SCHNUM"].ToString(),
                                        sch_name = StrPaginationUtil.Strip(orec["SCH_NAME"].ToString().Trim()),
                                        state_name = orec["STATE_NAME"].ToString().Trim(),
                                        cust_code = orec["CUST_CODE"].ToString(),
                                        custodian = StrPaginationUtil.Strip(orec["CUSTODIAN"].ToString().Trim()),
                                        lga = orec["TOWN"].ToString(),
                                        Candidates = Convert.ToInt32(orec["NOFCAND"])
                                    });
                                }
                            }

                        }
                    }
                    dbf.Close();
                    return fin.OrderBy(x => x.schnum).ToList();
                }
                catch (Exception ex)
                {
                    SafeGuiWpf.ShowError(ex.Message + " " + ex.InnerException);
                }
                finally
                {
                    dbf.Close();
                }
                return null;

            });
        }

        public async Task<List<FinModel>> FetchSchoolsToPreview(string StateCode)
        {
            
            return await Task.Run(() =>
            {

                var data = Path.Combine(APP_PATH, "fin.DBF");
                DbfFile dbf = new DbfFile(encoding);
                try
                {
                    dbf.Open(data, FileMode.Open);

                    //output values for all but binary and memo...
                    DbfRecord orec = new DbfRecord(dbf.Header);
                    var fin = new List<FinModel>();
                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            if (string.IsNullOrWhiteSpace(StateCode))
                            {
                                var cands = Convert.ToInt32(orec["NOFCAND"]);
                                if (cands > 0)
                                {
                                    fin.Add(new FinModel
                                    {
                                        schnum = orec["SCHNUM"].ToString(),
                                        sch_name = StrPaginationUtil.Strip(orec["SCH_NAME"].ToString().Trim()),
                                        state_name = orec["STATE_NAME"].ToString().Trim(),
                                        cust_code = orec["CUST_CODE"].ToString(),
                                        custodian = StrPaginationUtil.Strip(orec["CUSTODIAN"].ToString().Trim()),
                                        lga = orec["TOWN"].ToString(),
                                        Candidates = Convert.ToInt32(orec["NOFCAND"])
                                    });
                                }
                                
                            }
                            else
                            {
                                var seek = orec["STATE"];
                                var cands = Convert.ToInt32(orec["NOFCAND"]);
                                if (seek == StateCode && cands > 0)
                                {
                                    fin.Add(new FinModel
                                    {
                                        schnum = orec["SCHNUM"].ToString(),
                                        sch_name = StrPaginationUtil.Strip(orec["SCH_NAME"].ToString().Trim()),
                                        state_name = orec["STATE_NAME"].ToString().Trim(),
                                        cust_code = orec["CUST_CODE"].ToString(),
                                        custodian = StrPaginationUtil.Strip(orec["CUSTODIAN"].ToString().Trim()),
                                        lga = orec["TOWN"].ToString(),
                                        Candidates = Convert.ToInt32(orec["NOFCAND"])
                                    });
                                }
                            }
                           
                        }
                    }
                    dbf.Close();
                    return fin.OrderBy(x=> x.schnum).ToList();
                }
                catch(Exception ex)
                {
                   SafeGuiWpf.ShowError(ex.Message + " " + ex.InnerException);
                }
                finally
                {
                    dbf.Close();
                }
                return null;
               
                //string sql = (string.IsNullOrWhiteSpace(StateCode))? "SELECT * FROM fin ORDER BY state,schnum" : "SELECT * FROM fin ORDER BY schnum WHERE State=?";
                //using (OdbcCommand cmd = new OdbcCommand(sql, dbConection))
                //{
                //    cmd.Parameters.Add(new OleDbParameter("@StateCode", StateCode));
                //    OdbcDataReader dr = cmd.ExecuteReader();
                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            fin.Add(new FinModel
                //            {
                //                schnum = dr["schnum"].ToString(),
                //                sch_name = dr["sch_name"].ToString().Trim(),
                //                state_name = dr["state_name"].ToString().Trim(),
                //                cust_code = dr["cust_code"].ToString(),
                //                custodian = dr["custodian"].ToString(),
                //                lga = dr["town"].ToString(),
                //            });
                //        }
                //    }
                //}
               
            });
        }


        public async Task<FinModel> FetchSchoolToPreview(string SchoolNo)
        {
            var data = Path.Combine(APP_PATH, "fin.DBF");
            DbfFile dbf = new DbfFile(encoding);

            
            try
            {

                dbf.Open(data, FileMode.Open);
                //output values for all but binary and memo...
                DbfRecord orec = new DbfRecord(dbf.Header);
                return await Task.Run(() =>
                {
                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            var seek = orec["SCHNUM"];
                            var cands = Convert.ToInt32(orec["NOFCAND"]);
                            if (seek == SchoolNo && cands > 0)
                            {
                                return new FinModel
                                {
                                    schnum = orec["SCHNUM"].ToString(),
                                    sch_name = StrPaginationUtil.Strip(orec["SCH_NAME"].ToString().Trim()),
                                    state_name = orec["STATE_NAME"].ToString().Trim(),
                                    cust_code = orec["CUST_CODE"].ToString(),
                                    custodian = StrPaginationUtil.Strip(orec["CUSTODIAN"].ToString().Trim()),
                                    lga = orec["TOWN"].ToString(),
                                    Candidates = Convert.ToInt32(orec["NOFCAND"])
                                };
                            }
                        }
                    }
                    dbf.Close();
                    return null;
                });
            }
            catch(Exception ex)
            {
                SafeGuiWpf.ShowError(ex.Message);
            }
            finally
            {
                dbf.Close();
            }
            //dbf.Close();
            return null;
        }

        public async Task<List<AlbumPersonalInfo>> ReadAlbumPersonalInfo(string schnum)
        {
            //if (dbConection.State == ConnectionState.Closed)
            //    dbConection.Open();

            return await Task.Run(() =>
            {
                var data = Path.Combine(APP_PATH, "mast.DBF");
                DbfFile dbf = new DbfFile(encoding);
                try
                {
                   
                    dbf.Open(data, FileMode.Open);

                    //output values for all but binary and memo...
                    DbfRecord orec = new DbfRecord(dbf.Header);

                    var students = new List<AlbumPersonalInfo>();

                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            var seek = orec["SCHNUM"];
                            //var cands = Convert.ToInt32(orec["NOFCAND"]);
                            if (seek == schnum)
                            {
                                students.Add(new AlbumPersonalInfo
                                {
                                    reg_no = orec["REG_NO"].ToString(),
                                    cand_name = StrPaginationUtil.Strip(orec["CAND_NAME"].ToString().Trim()),
                                    ser_no = orec["SER_NO"].ToString(),

                                });
                            }
                        }
                    }
                    //string sql = "SELECT reg_no,cand_name,ser_no FROM master WHERE schnum='" + schnum + "'";
                    //using (OdbcCommand cmd = new OdbcCommand(sql, dbConection))
                    //{
                    //    OdbcDataReader dr = cmd.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            students.Add(new AlbumPersonalInfo
                    //            {
                    //                reg_no = dr["reg_no"].ToString(),
                    //                cand_name = dr["cand_name"].ToString(),
                    //                ser_no = dr["ser_no"].ToString(),

                    //            });
                    //        }
                    //        return students;
                    //    }
                    //}
                    dbf.Close();
                    return students.OrderBy(x=> x.ser_no).ToList();
                }
                catch (Exception e)
                {
                 SafeGuiWpf.ShowError(e.Message + " " + e.InnerException);
                }
                finally
                {
                    dbf.Close();
                }
                return null;
            });
           
            
        }

        Encoding encoding = Encoding.GetEncoding(1252);
        public async Task<List<State>> ReadStateInfo()
        {

            return await Task.Run(() =>
            {
                var data = Path.Combine(APP_PATH, "state.DBF");
                DbfFile dbf = new DbfFile(encoding);
                try
                {
                    
                    dbf.Open(data, FileMode.Open);

                    //output values for all but binary and memo...
                    DbfRecord orec = new DbfRecord(dbf.Header);
                    var state = new List<State>();
                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            state.Add(new State
                            {
                                Code = orec["CODE"].ToString(),
                                Name = orec["STATE"].ToString().Trim(),
                                Schools = Convert.ToInt32(orec["SCHOOLS"])
                            });
                        }
                    }
                    dbf.Close();
                    return state.OrderBy(x=> x.Code).ToList();
                   

                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message + " " + e.InnerException);
                }
                finally
                {
                    dbf.Close();
                }
                return null;
            });
        }

        public async Task<State> ReadStateInfo(string code)
        {

            return await Task.Run(() =>
            {
                var data = Path.Combine(APP_PATH, "state.DBF");
                DbfFile dbf = new DbfFile(encoding);
                try
                {

                    dbf.Open(data, FileMode.Open);

                    //output values for all but binary and memo...
                    DbfRecord orec = new DbfRecord(dbf.Header);
                    //var state = new State();
                    while (dbf.ReadNext(orec))
                    {
                        //output column values...
                        if (!orec.IsDeleted)
                        {
                            var seek = orec["CODE"].ToString();
                            if (seek == code)
                            {
                                return new State
                                {
                                    Code = orec["CODE"].ToString(),
                                    Name = orec["STATE"].ToString().Trim(),
                                    Schools = Convert.ToInt32(orec["SCHOOLS"])
                                };
                            }
                        }
                    }
                    dbf.Close();
                    return null;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message + " " + e.InnerException);
                }
                finally
                {
                    dbf.Close();
                }
                return null;
            });
        }

    }
}
