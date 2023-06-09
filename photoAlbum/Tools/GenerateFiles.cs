using Newtonsoft.Json;
using photoAlbum.Activities;
using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Tools
{
    public class GenerateFiles
    {

        public static async Task<bool> GenerateByState(string state)
        {
            if (string.IsNullOrWhiteSpace(EntryPoint.DataBasePath))
            {
                SafeGuiWpf.ShowError("Please select a valid Database Directory");
                return false;
            }

            if (string.IsNullOrWhiteSpace(EntryPoint.DestinationFilesPath))
            {
                SafeGuiWpf.ShowError("Please select a valid Destination Directory");
                return false;
            }



            if (string.IsNullOrWhiteSpace(EntryPoint.PictureFilesFolder))
            {
                SafeGuiWpf.ShowError("Please select a valid Pictures Directory");
                return false;
            }

            string errfile = "";
            string errCand = "";
            //FetchDataClass fd = new FetchDataClass();
            try
            {
                var fin = new List<FinModel>();
                using (FetchDataClass fd = new FetchDataClass())
                {
                    fin = await fd.FetchSchoolsToPreview(state);
                }


                //if (s == null)
                //{
                //    SafeGuiWpf.ShowInformation("No records found for the School");
                //    return;
                //}
                var students = new List<AlbumPersonalInfo>();
                var p = new List<AlbumPersonalInfo>();
                foreach (var s in fin)
                {
                    string schnum = s.schnum;
                    string schname = s.sch_name.Trim();
                    string sFolder = s.state_name.Trim();
                    string StateCode = schnum.Substring(0, 3);
                    string lga = s.lga.Trim();
                    string custodian = s.custodian.Trim();
                    string fileName = schnum;

                    students.Clear();// = new List<AlbumPersonalInfo>();
                    using (FetchDataClass fd = new FetchDataClass())
                    {
                        students = await fd.ReadAlbumPersonalInfo(schnum);
                    }
                    //var students = await fd.ReadAlbumPersonalInfo(schnum);
                    if (students == null)
                        continue;

                    if (students.Count == 0)
                        continue;


                    p.Clear();

                    foreach (var m in students)
                    {
                        string imgPath = "";
                        //E:\photoAlbum\ssce2020\SSCE_INT_2020
                        string tempf = string.Format($"P{schnum.Substring(0, 3)}\\");
                        imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{m.reg_no.Trim()}.jpg");
                        //imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{schnum}\\{m.reg_no.Trim()}.jpg");
                        //imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{m.reg_no.Trim()}_thumbnail.jpg");
                        //imgPath = "";// @"e:\novext17picts\" + dtp.Rows[i]["Reg_No"].ToString().Trim() + ".jpg";
                        System.Drawing.Image img;
                        errCand = m.reg_no;
                        if (!File.Exists(imgPath))
                        {
                            imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_2");
                            if (Directory.Exists(imgPath))
                            {
                                var imp  = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                                imgPath = imp;
                                if (!File.Exists(imgPath))
                                {
                                    imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_3");
                                    if (Directory.Exists(imgPath))
                                    {
                                        var impp = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                                        imgPath = impp;
                                    }
                                }
                            }
                            else
                            {
                                imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_3");
                                if (Directory.Exists(imgPath))
                                {
                                    var impp = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                                    imgPath = impp;
                                }
                            }
                            if (!File.Exists(imgPath))
                            {
                                imgPath = EntryPoint.Null_passportPath;
                            }
                        }


                        try
                        {
                            img = System.Drawing.Image.FromFile(imgPath);
                        }
                        catch
                        {
                            imgPath = EntryPoint.Null_passportPath;

                            img = System.Drawing.Image.FromFile(imgPath);
                        }

                        p.Add(
                            new AlbumPersonalInfo
                            {
                                reg_no = m.reg_no,
                                cand_name = m.cand_name.Trim(),
                                ser_no = m.ser_no,
                                passport = ProcessImageData.imageToString(img),
                            });

                    }
                    bool cand1 = false, cand2 = false, cand3 = false;
                    var candidates = new List<CandidateModel>();
                    int iteration = 0;
                    int count = 0, recCount = p.Count;
                    var scands = new CandidateModel();
                    foreach (var m in p)
                    {
                        count++;
                        if (iteration == 0 && (cand1 || cand2 || cand3))
                        {
                            candidates.Add(scands);
                            cand1 = cand2 = cand3 = false;
                        }

                        iteration++;
                        if (iteration == 1)
                            scands = new CandidateModel();

                        if (iteration == 1)
                        {
                            //var cand1 = new AlbumPersonalInfo();
                            scands.reg_no1 = m.reg_no;
                            scands.cand_name1 = m.cand_name;
                            scands.ser_no1 = m.ser_no;
                            scands.passport1 = m.passport;
                            cand1 = true;

                        }

                        if (iteration == 2)
                        {
                            //var cand2 = new AlbumPersonalInfo();
                            scands.reg_no2 = m.reg_no;
                            scands.cand_name2 = m.cand_name;
                            scands.ser_no2 = m.ser_no;
                            scands.passport2 = m.passport;
                            cand2 = true;
                        }

                        if (iteration == 3)
                        {
                            //var cand3 = new AlbumPersonalInfo();
                            scands.reg_no3 = m.reg_no;
                            scands.cand_name3 = m.cand_name;
                            scands.ser_no3 = m.ser_no;
                            scands.passport3 = m.passport;
                            iteration = 0;
                            cand3 = true;
                        }

                        if (count == recCount)
                        {
                            candidates.Add(scands);
                            cand1 = cand2 = cand3 = false;
                        }
                    }


                    albumModel f = new albumModel
                    {
                        schnum = fileName,
                        schn_name = schname,
                        lga = lga,
                        custodian = custodian,
                        candidates = candidates
                    };


                    string albumpath = $"{EntryPoint.DataBasePath}\\{sFolder}";
                    if (!Directory.Exists(albumpath))
                        Directory.CreateDirectory(albumpath);

                    string Json = JsonConvert.SerializeObject(f);
                    string sFile = string.Format($"{albumpath}\\{schnum}.alb");

                    File.WriteAllText(sFile, Json);

                }
                return true;
            }
            catch (Exception ex)
            {
               WpfMessageBox.Show("Generate Files", $"{ex.Message} {errfile} Candidate : {errCand}");
            }
            return false;
        }



        public static async Task GenerateBySchool(string school)
        {
            if (string.IsNullOrWhiteSpace(EntryPoint.DataBasePath))
            {
               SafeGuiWpf.ShowError("Please select a valid Database Directory");
                return;
            }

            if (string.IsNullOrWhiteSpace(EntryPoint.DestinationFilesPath))
            {
               SafeGuiWpf.ShowError("Please select a valid Destination Directory");
               return;
            }



            if (string.IsNullOrWhiteSpace(EntryPoint.PictureFilesFolder))
            {
               SafeGuiWpf.ShowError("Please select a valid Pictures Directory");
                return;
            }

            string errfile = "";
            string errCand = "";
            //FetchDataClass fd = new FetchDataClass();
            try
            {
                var s = new FinModel();
                using (FetchDataClass fd = new FetchDataClass())
                {
                    s = await fd.FetchSchoolToPreview(school);
                }


                if (s == null)
                {
                    SafeGuiWpf.ShowInformation("No records found for the School");
                    return ;
                }

                //foreach (var s in fin)
                //{
                string schnum = s.schnum;
                string schname = s.sch_name.Trim();
                string sFolder = s.state_name.Trim();
                string StateCode = schnum.Substring(0, 3);
                string lga = s.lga.Trim();
                string custodian = s.custodian.Trim();
                string fileName = schnum;

                var students = new List<AlbumPersonalInfo>();
                using (FetchDataClass ffd = new FetchDataClass())
                {
                    students = await ffd.ReadAlbumPersonalInfo(schnum);
                }
                //var students = await fd.ReadAlbumPersonalInfo(schnum);
                if (students == null || students.Count==0)
                {
                    SafeGuiWpf.ShowInformation("No records found for the School");
                    return ;
                }

              
                List<AlbumPersonalInfo> p = new List<AlbumPersonalInfo>();

                foreach (var m in students)
                {
                    string imgPath = "";
                    //E:\photoAlbum\ssce2020\SSCE_INT_2020
                    string tempf = string.Format($"P{schnum.Substring(0, 3)}\\");
                    imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{m.reg_no.Trim()}.jpg");
                    
                    //imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{schnum}\\{m.reg_no.Trim()}.jpg");
                    //imgPath = string.Format($"{EntryPoint.PictureFilesFolder}\\{m.reg_no.Trim()}_thumbnail.jpg");
                    //imgPath = "";// @"e:\novext17picts\" + dtp.Rows[i]["Reg_No"].ToString().Trim() + ".jpg";
                    System.Drawing.Image img;
                    errCand = m.reg_no;
                    if (!File.Exists(imgPath))
                    {
                        imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_2");
                        if (Directory.Exists(imgPath))
                        {
                            var imp = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                            imgPath = imp;
                            if (!File.Exists(imgPath))
                            {
                                imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_3");
                                if (Directory.Exists(imgPath))
                                {
                                    var impp = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                                    imgPath = impp;
                                }
                            }
                        }
                        else
                        {
                            imgPath = string.Format($"{EntryPoint.PictureFilesFolder}_3");
                            if (Directory.Exists(imgPath))
                            {
                                var impp = $"{imgPath}\\{m.reg_no.Trim()}.jpg";
                                imgPath = impp;
                            }
                        }
                        if (!File.Exists(imgPath))
                        {
                            imgPath = EntryPoint.Null_passportPath;
                        }
                    }


                    try
                    {
                        img = System.Drawing.Image.FromFile(imgPath);
                    }
                    catch
                    {
                        imgPath = EntryPoint.Null_passportPath;

                        img = System.Drawing.Image.FromFile(imgPath);
                    }

                    p.Add(
                        new AlbumPersonalInfo
                        {
                            reg_no = m.reg_no,
                            cand_name = m.cand_name.Trim(),
                            ser_no = m.ser_no,
                            passport = ProcessImageData.imageToString(img),
                        });

                }
                bool cand1 = false, cand2 = false, cand3 = false;
                var candidates = new List<CandidateModel>();
                int iteration = 0;
                int count = 0, recCount = p.Count;
                CandidateModel scands = new CandidateModel();
                foreach (var m in p)
                {
                    count++;
                    if (iteration == 0 && (cand1 || cand2 || cand3))
                    {
                        candidates.Add(scands);
                        cand1 = cand2 = cand3 = false;
                    }

                    iteration++;
                    if (iteration == 1)
                        scands = new CandidateModel();

                    if (iteration == 1)
                    {
                        //var cand1 = new AlbumPersonalInfo();
                        scands.reg_no1 = m.reg_no;
                        scands.cand_name1 = m.cand_name;
                        scands.ser_no1 = m.ser_no;
                        scands.passport1 = m.passport;
                        cand1 = true;

                    }

                    if (iteration == 2)
                    {
                        //var cand2 = new AlbumPersonalInfo();
                        scands.reg_no2 = m.reg_no;
                        scands.cand_name2 = m.cand_name;
                        scands.ser_no2 = m.ser_no;
                        scands.passport2 = m.passport;
                        cand2 = true;
                    }

                    if (iteration == 3)
                    {
                        //var cand3 = new AlbumPersonalInfo();
                        scands.reg_no3 = m.reg_no;
                        scands.cand_name3 = m.cand_name;
                        scands.ser_no3 = m.ser_no;
                        scands.passport3 = m.passport;
                        iteration = 0;
                        cand3 = true;
                    }

                    if (count == recCount)
                    {
                        candidates.Add(scands);
                        cand1 = cand2 = cand3 = false;
                    }
                }


                albumModel f = new albumModel
                {
                    schnum = fileName,
                    schn_name = schname,
                    lga = lga,
                    custodian = custodian,
                    candidates = candidates
                };


                string albumpath = $"{EntryPoint.DataBasePath}\\{sFolder}";
                if (!Directory.Exists(albumpath))
                    Directory.CreateDirectory(albumpath);

                string Json = JsonConvert.SerializeObject(f);
                string sFile = string.Format($"{albumpath}\\{schnum}.alb");

                File.WriteAllText(sFile, Json);
                SafeGuiWpf.ShowSuccess($"Files generated for {schnum}");
                //}
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show("Generate Files", $"{ex.Message} {errfile} Candidate : {errCand}");
            }
            
        }
    }
}
