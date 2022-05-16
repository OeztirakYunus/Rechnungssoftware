using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Extensions.DtoEntityParser
{
    public static class ContactParserExtension
    {
        public static Contact ToEntity(this ContactDto source)
        {
            return new Contact()
            {
                Address = source.Address.ToEntity(),
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
                Gender = source.Gender,
                NameOfOrganisation = source.NameOfOrganisation,
                Title = source.Title,
                TypeOfContactEnum = source.TypeOfContactEnum
            };
        }
    }
}
