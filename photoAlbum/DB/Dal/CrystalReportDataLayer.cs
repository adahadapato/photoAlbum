using CrystalDecisions.CrystalReports.Engine;
using photoAlbum.Activities;
using photoAlbum.DB.Reports;
using photoAlbum.Models;
using photoAlbum.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.DB.Dal
{
    public class CrystalReportDataLayer
    {
        public ReportDocument GeneratePhotoAlbumDocument(albumModel data)
        {

            try
            {

                DataSet ds = new dsphotoAlbum();
                var rpt = new RptPhotoAlbum2();

                if (data != null)
                {
                    string schnum = string.Format("[{0}]", data.schnum);
                    string schName = data.schn_name.Trim();
                    //string year = EntryPoint.ExaminationYear;
                    string exam_details = $"{EntryPoint.ExaminationYear} SSCE ({EntryPoint.Examination})";
                    //string month = EntryPoint.Examination;
                    //string lga = "";// data.lga.Trim();
                    string custodian = data.custodian.Trim();
                    
                    foreach (var m in data.candidates)
                    {
                        DataRow drow = ds.Tables[0].NewRow();
                        Image passport1 = ProcessImageData.stringToImage(m.passport1);
                        byte[] bImage1 = (byte[])ProcessImageData.CopyImageToByteArray(passport1);
                        drow["reg_no1"] = m.reg_no1;
                        drow["cand_name1"] = m.cand_name1;
                        drow["ser_no1"] = m.ser_no1;
                        drow["passport1"] = bImage1;
                        drow["exam_type"] = "";
                        drow["exam_year"] = EntryPoint.ExaminationYear;
                        drow["schnum"] = "";
                        drow["sch_name"] = "";

                        if (m.reg_no2 != null)
                        {
                            Image passport2 = ProcessImageData.stringToImage(m.passport2);
                            byte[] bImage2 = (byte[])ProcessImageData.CopyImageToByteArray(passport2);
                            drow["reg_no2"] = m.reg_no2;
                            drow["cand_name2"] = m.cand_name2;
                            drow["ser_no2"] = m.ser_no2;
                            drow["passport2"] = bImage2;
                            drow["exam_type"] = "";
                            drow["exam_year"] = EntryPoint.ExaminationYear;
                            drow["schnum"] = "";
                            drow["sch_name"] = "";
                        }

                        if (m.reg_no3 != null)
                        {
                            Image passport3 = ProcessImageData.stringToImage(m.passport3);
                            byte[] bImage3 = (byte[])ProcessImageData.CopyImageToByteArray(passport3);
                            drow["reg_no3"] = m.reg_no3;
                            drow["cand_name3"] = m.cand_name3;
                            drow["ser_no3"] = m.ser_no3;
                            drow["passport3"] = bImage3;
                            drow["exam_type"] = "";
                            drow["exam_year"] = EntryPoint.ExaminationYear;
                            drow["schnum"] = "";
                            drow["sch_name"] = "";
                        }

                        ds.Tables[0].Rows.Add(drow);
                    }

                    rpt.DataDefinition.FormulaFields["fmlschnum"].Text = '"' + schnum + '"';
                    rpt.DataDefinition.FormulaFields["fmlsch_name"].Text = '"' + schName + '"';
                    rpt.DataDefinition.FormulaFields["fmlyear"].Text = '"' + exam_details + '"';
                    //rpt.DataDefinition.FormulaFields["fmlmonth"].Text = '"' + month + '"';
                    //rpt.DataDefinition.FormulaFields["fmllga"].Text = '"' + lga + '"';
                    rpt.DataDefinition.FormulaFields["fmlcustodian"].Text = '"' + custodian + '"';
                    ds.Tables[0].AcceptChanges();
                    rpt.SetDataSource(ds);
                    return rpt;

                }

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
            return null;

        }
    }
}
