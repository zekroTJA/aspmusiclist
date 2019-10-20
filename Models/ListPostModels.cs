using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Models
{
    /// <summary>
    /// List Entry Model when received as post
    /// request for creating List Entries.
    /// </summary>
    public class ListEntryPostModel
    {
        [Required]
        public string Content { get; set; }
    }

    public class ListPasswordChangeModel : IMasterKey
    {
        [Required]
        public string MasterKey { get; set; }
        [Required]
        public string NewKeyword { get; set; }
    }

    public class ListMasterKeyModel : IMasterKey
    {
        [Required]
        public string MasterKey { get; set; }
    }
}
