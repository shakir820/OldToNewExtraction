using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    public class ImageObj
    {
        [Key]
        public int image_id { get; set; }
        public int? upload_obua_id { get; set; }
        public int? document_type_id { get; set; }
        public DateTime? upload_date { get; set; }
        public DateTime? Record_Create_Date { get; set; }
        public int? application_id { get; set; }
        public DateTime? last_download_timestamp { get; set; }
        public int? last_download_obua_id { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        [NotMapped]
        public string oldFileName { get; set; }
        [NotMapped]
        public byte[] image { get;  set; }
    }


    public class OldImageObj
    {
        [Key]
        public int image_id { get; set; }
        public int? upload_obua_id { get; set; }
        public int? document_type_id { get; set; }
        public DateTime? upload_date { get; set; }
        public DateTime? Record_Create_Date { get; set; }
        public int? application_id { get; set; }
        public DateTime? last_download_timestamp { get; set; }
        public int? last_download_obua_id { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public byte[] image { get; set; }


    }


}
