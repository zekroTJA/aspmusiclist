using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace musicList2.Models {

    public class ListEntry<T> {
        [Key]
        public Guid GUID { get; set; }

        public T Content { get; set; }

        public ListEntry(T content) {
            Content = content;
            GUID = Guid.NewGuid();
        }
    }

    public class ListEntryPostModel
    {
        public string Content { get; set; }
    }
}