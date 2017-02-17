using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridLists
    {

        //[StringLength(5, ErrorMessage = "Número ato não pode ter mais que 5 digitos.")]
        [Display(Name = "ID")]
        public int ID { get; set; }
        public int ID_LIST_TYPE { get; set; }
        [Display(Name = "Check-in Date")]
        public DateTime? CHECK_IN { get; set; }
        [Display(Name = "Check-out Date")]
        public DateTime? CHECK_OUT { get; set; }
        [Display(Name = "Start Block Date")]
        public DateTime? DT_START_BLOCK { get; set; }
        [Display(Name = "Start Book Date")]
        public DateTime? DT_START_BOOK { get; set; }
        [Display(Name = "Notes")]
        public string NOTES { get; set; }
        [Display(Name = "First Name Guest")]
        public string FIRST_NAME_GUEST { get; set; }
        [Display(Name = "Last Name Guest")]
        public string LAST_NAME_GUEST { get; set; }
        [Display(Name = "LogLogin")]
        public string LOGLOGIN { get; set; }
        public int? ID_DOCUMENT { get; set; }

        public string FILE_NAME { get; set; }
        public long FILE_SIZE { get; set; }
        public string FILE_TYPE { get; set; }
        [Display(Name = "Image")]
        public byte[] FILE_CONTENT { get; set; }
        [Display(Name = "Insert Date")]
        public DateTime DT_INSERT { get; set; }
        public DateTime? DT_DELETE { get; set; }

        public string htmlImage { get; set; }

        [Display(Name = "Guest Name")]
        public string FullName
        {
            get
            {
                return LAST_NAME_GUEST + ", " + FIRST_NAME_GUEST;
            }
        }

        [Display(Name = "File View")]
        public string FullFileName
        {
            get
            {
                return FILE_NAME + "." + FILE_TYPE;
            }
        }
    }
}
