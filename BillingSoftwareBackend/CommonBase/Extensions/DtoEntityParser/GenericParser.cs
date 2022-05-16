//using BillingSoftware.Core.DataTransferObjects;
//using BillingSoftware.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonBase.Extensions.DtoEntityParser
//{
//    public static class GenericParser
//    {
//        private static Type[] REFTYPES = new Type[] { typeof(string), typeof(Guid), typeof(decimal), typeof(DateTime) };
//        public static object ToEntity<T>(this T source) where T : new()
//        {
//            var (dtoType, entity) = GetDtoType(source);

//            PropertyInfo[] properties = dtoType.GetProperties();
//            foreach (PropertyInfo property in properties)
//            {
//                try
//                {
//                    var value = source.GetType().GetProperty(property.Name).GetValue(source);

//                    if (!value.GetType().IsValueType && !REFTYPES.Contains(value.GetType()))
//                    {
//                        entity.GetType().GetProperty(property.Name).SetValue(entity, value.ToEntity());
//                    }
//                    else
//                    {
//                        entity.GetType().GetProperty(property.Name).SetValue(entity, value);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Error: " + ex.Message);
//                }
//            }

//            return entity;
//        }

//        public static ICollection<object> ToEntity<T>(this ICollection<T> source)
//        {
//            var list = new List<object>();
//            var sourceList = source as object;
//            foreach (T item in source)
//            {
//                var tempItem = item as object;
//                list.Add(tempItem.ToEntity());
//            }

//            return list;
//        }

//        private static (Type, object) GetDtoType<T>(T source)
//        {
//            Type dtoType = typeof(object);
//            object entity = null;

//            if (typeof(T) == typeof(ContactDto) || typeof(T) == typeof(object) && source is ContactDto)
//            {
//                dtoType = typeof(ContactDto);
//                entity = new Contact();
//            }
//            else if (typeof(T) == typeof(AddressDto) || typeof(T) == typeof(object) && source is AddressDto)
//            {
//                dtoType = typeof(AddressDto);
//                entity = new Address();
//            }
//            else if (typeof(T) == typeof(OfferDto) || typeof(T) == typeof(object) && source is OfferDto)
//            {
//                dtoType = typeof(OfferDto);
//                entity = new Offer();
//            }
//            else if (typeof(T) == typeof(DocumentInformationDto) || typeof(T) == typeof(object) && source is DocumentInformationDto)
//            {
//                dtoType = typeof(DocumentInformationDto);
//                entity = new DocumentInformations();
//            }
//            else if (typeof(T) == typeof(PositionDto) || typeof(T) == typeof(object) && source is PositionDto)
//            {
//                dtoType = typeof(PositionDto);
//                entity = new Position();
//            }
//            return (dtoType, entity);
//        }
//    }
//}
