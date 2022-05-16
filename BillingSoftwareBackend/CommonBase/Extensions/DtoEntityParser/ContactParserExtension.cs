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
        public static object ToEntity<T>(this T source) where T : new()
        {
            Type dtoType = typeof(ContactDto);
            object entity = null;
            if (typeof(T) == typeof(ContactDto))
            {
                dtoType = typeof(ContactDto);
                entity = new Contact();
            }
            
            PropertyInfo[] properties = dtoType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    var en = entity.GetType().GetProperty(property.Name);
                    var val = source.GetType().GetProperty(property.Name).GetValue(source);
                    
                    if (val.GetType().IsPrimitive)
                    {
                        en.SetValue(entity, val.ToEntity());
                    }

                    en.SetValue(entity, val);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return entity;
        }

        private static void GetDtoType<T>()
        {
           
        }
    }
}
