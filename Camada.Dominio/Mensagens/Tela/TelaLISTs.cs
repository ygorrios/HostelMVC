using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaLISTs
    {
        public int ID { get; set; }
        [Display(Name = "File name")]
        public string FILE_NAME { get; set; }
        public long FILE_SIZE { get; set; }
        public string FILE_TYPE { get; set; }
        public byte[] FILE_CONTENT { get; set; }
        public DateTime DT_INSERT { get; set; }
        public DateTime? DT_DELETE { get; set; }
        public int ID_LIST_TYPE { get; set; }
        [Display(Name = "Check-in Date")]
        public DateTime? CHECK_IN { get; set; }
        [Display(Name = "Check-out Date")]
        public DateTime? CHECK_OUT { get; set; }
        public DateTime? DT_START_BLOCK { get; set; }
        public DateTime? DT_START_BOOK { get; set; }
        [Display(Name = "Notes")]
        public string NOTES { get; set; }
        [Display(Name = "First Name")]
        public string FIRST_NAME_GUEST { get; set; }
        [Display(Name = "Last Name")]
        public string LAST_NAME_GUEST { get; set; }
        public string LOGLOGIN { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LAST_NAME_GUEST + ", " + FIRST_NAME_GUEST;
            }
        }
    }
}
