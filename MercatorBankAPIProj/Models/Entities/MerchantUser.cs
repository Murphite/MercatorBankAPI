using MercatorBankAPIProj.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercatorBankAPIProj.Models.Entities
{
    public class MerchantUser : IdentityUser
    {
        
        public string AccountName { get; set; }

        [ForeignKey("MoreUserDetails")]
        public int? ReferenceId { get; set; }


        //Navigation Property
        public virtual MoreUserDetails MoreUserDetails  {  get; set; }
        public List<Card> Cards { get; set; }
    }
}
