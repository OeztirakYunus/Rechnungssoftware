using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BillingSoftware.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CommonBase.Extensions
{
    public static class EfExtensions
    {
        public static IQueryable<TEntity> IncludeAllRecursively<TEntity>(this IQueryable<TEntity> queryable,
            int maxDepth = int.MaxValue, bool addSeenTypesToIgnoreList = true, HashSet<Type>? ignoreTypes = null)
            where TEntity : class
        {
            var type = typeof(TEntity);
            var includes = new List<string>();
            ignoreTypes ??= new HashSet<Type>();
            if(ignoreTypes.Count <= 0)
            {
                ignoreTypes = GetIgnoreTypes();
            }

            GetIncludeTypes(ref includes, prefix: string.Empty, type, ref ignoreTypes, addSeenTypesToIgnoreList, maxDepth);
            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable;
        }

        private static HashSet<Type> GetIgnoreTypes()
        {
            var ignoreTypes = new HashSet<Type>();
            ignoreTypes.Add(typeof(string));
            ignoreTypes.Add(typeof(Int32));
            ignoreTypes.Add(typeof(Int64));
            ignoreTypes.Add(typeof(Int16));
            ignoreTypes.Add(typeof(double));
            ignoreTypes.Add(typeof(float));
            ignoreTypes.Add(typeof(bool));
            ignoreTypes.Add(typeof(Gender));
            ignoreTypes.Add(typeof(ProductCategory));
            ignoreTypes.Add(typeof(Role));
            ignoreTypes.Add(typeof(Status));
            ignoreTypes.Add(typeof(TypeOfContact));
            ignoreTypes.Add(typeof(TypeOfDiscount));
            ignoreTypes.Add(typeof(Unit));
            ignoreTypes.Add(typeof(int));
            ignoreTypes.Add(typeof(Guid));
            ignoreTypes.Add(typeof(DateTimeOffset?));
            ignoreTypes.Add(typeof(DateTimeOffset));
            ignoreTypes.Add(typeof(byte[]));
            return ignoreTypes;
        }

        private static void GetIncludeTypes(ref List<string> includes, string prefix, Type type, ref HashSet<Type> ignoreSubTypes,
            bool addSeenTypesToIgnoreList = true, int maxDepth = int.MaxValue)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var getter = property.GetGetMethod();
                if (getter != null)
                {
                    var isVirtual = getter.IsVirtual;
                    if (isVirtual)
                    {
                        var propPath = (prefix + "." + property.Name).TrimStart('.');
                        if (maxDepth <= propPath.Count(c => c == '.')) { break; }

                        if (!ignoreSubTypes.Any(x => x.Name.ToLower().Contains(property.PropertyType.Name.ToLower())))
                        {
                            includes.Add(propPath);
                        }

                        var subType = property.PropertyType;
                        if (ignoreSubTypes.Contains(subType))
                        {
                            continue;
                        }
                        else if (addSeenTypesToIgnoreList)
                        {
                            // add each type that we have processed to ignore list to prevent recursions
                            ignoreSubTypes.Add(type);
                        }

                        var isEnumerableType = subType.GetInterface(nameof(IEnumerable)) != null;
                        var genericArgs = subType.GetGenericArguments();
                        if (isEnumerableType && genericArgs.Length == 1)
                        {
                            // sub property is collection, use collection type and drill down
                            var subTypeCollection = genericArgs[0];
                            if (subTypeCollection != null)
                            {
                                GetIncludeTypes(ref includes, propPath, subTypeCollection, ref ignoreSubTypes, addSeenTypesToIgnoreList, maxDepth);
                            }
                        }
                        else
                        {
                            // sub property is no collection, drill down directly
                            GetIncludeTypes(ref includes, propPath, subType, ref ignoreSubTypes, addSeenTypesToIgnoreList, maxDepth);
                        }
                    }
                }
            }
        }
    }
}
