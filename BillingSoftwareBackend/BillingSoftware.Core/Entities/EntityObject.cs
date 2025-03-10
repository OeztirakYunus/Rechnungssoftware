﻿using System;
using System.ComponentModel.DataAnnotations;
using BillingSoftware.Core.Contracts;

namespace BillingSoftware.Core.Entities
{
    public class EntityObject : IEntityObject
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] RowVersion
        {
            get;
            set;
        }
    }
}
