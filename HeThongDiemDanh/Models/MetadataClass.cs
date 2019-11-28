using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongDiemDanh.Models
{
    [MetadataType(typeof(NGUOIDUNGMetadata))]
    public partial class NGUOIDUNG
    {
        public string LoginErroMsg { get; internal set; }
    }
    public partial class NGUOIDUNGMetadata
    {
        [DisplayName("TENDANGNHAP")]
        [Required(ErrorMessage = " This field is required")]
        public string TENDANGNHAP { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = " This field is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string MATKHAU { get; set; }

    }
}