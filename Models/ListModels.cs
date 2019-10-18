using musicList2.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace musicList2.Models {

    /// <summary>
    /// List Model.
    /// </summary>
    public class List
    {
        [Key]
        public Guid GUID { get; set; }

        public string Identifier { get; set; }

        [IgnoreDataMember]
        public string KeywordHash { get; set; }

        [IgnoreDataMember]
        public string MasterKeyHash { get; set; }

        public List() { }

        public List(string identifier, string keyword)
        {
            Identifier = identifier;

            GUID = Guid.NewGuid();
            KeywordHash = Hashing.CreatePasswordHash(keyword);
        }
    }

    /// <summary>
    /// Lisr response model which is returned on
    /// list creation containing the randomly generated
    /// master key.
    /// </summary>
    public class ListCreated : List
    {
        public string MasterKey { get; set; }

        public ListCreated(List list, string masterKey)
        {
            GUID = list.GUID;
            Identifier = list.Identifier;
            MasterKey = masterKey;
        }
    }

    /// <summary>
    /// generic List Entry Model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListEntry<T> {
        [Key]
        public Guid GUID { get; set; }

        public Guid ListGUID { get; set; }

        public T Content { get; set; }

        public ListEntry(T content, Guid listGUID) {
            Content = content;
            ListGUID = listGUID;

            GUID = Guid.NewGuid();
        }
    }
}