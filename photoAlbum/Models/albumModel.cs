using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Models
{
    public class CandidateModel
    {
        public string cand_name1 { get; set; }
        public string ser_no1 { get; set; }
        public string reg_no1 { get; set; }
        public string passport1 { get; set; }

        public string reg_no2 { get; set; }
        public string cand_name2 { get; set; }
        public string ser_no2 { get; set; }
        public string passport2 { get; set; }

        public string cand_name3 { get; set; }
        public string ser_no3 { get; set; }
        public string reg_no3 { get; set; }
        public string passport3 { get; set; }
    }

    public class albumModel
    {
        public string exam_year { get; set; }
        public string exam_type { get; set; }
        public string schnum { get; set; }
        public string schn_name { get; set; }
        public string custodian { get; set; }
        public string lga { get; set; }
        public List<CandidateModel> candidates { get; set; }
    }
}
