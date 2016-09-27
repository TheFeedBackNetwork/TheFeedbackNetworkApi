using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Base;
using TFN.Api.Models.ResponseModels;

namespace TFN.Api.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// A list of ordered key names.
        /// Priority keys should appear at the top of a list in the specified order.
        /// </summary>
        private static string[] priorityKeys = new string[] { "id", "href" };

        public static JsonResult Json<T>(this Controller controller, T data, IEnumerable<string> excludedAttributes)
        {
            // Handles Objects
            if (data.GetType().GetInterface("IEnumerable") == null)
            {
                var filteredExpando = new ExpandoObject() as ICollection<KeyValuePair<string, Object>>;
                return controller.Json(ProcessFilters<T>(filteredExpando, data, excludedAttributes.Select(x => x.ToLower())));
            }
            // Handles Collections of Objects
            else
            {
                var collection = new List<ICollection<KeyValuePair<string, Object>>>();
                foreach (object o in (data as IEnumerable))
                {
                    var innerExpando = new ExpandoObject() as ICollection<KeyValuePair<string, Object>>;
                    collection.Add(ProcessFilters(innerExpando, o, excludedAttributes));
                }

                return controller.Json(collection);
            }
        }

        public static CreatedAtActionResult CreatedAtAction<T>(this Controller controller, string actionName, object routeValues, T data, IEnumerable<string> excludedAttributes)
        {
            // Handles Objects
            if (data.GetType().GetInterface("IEnumerable") == null)
            {
                var filteredExpando = new ExpandoObject() as IDictionary<string, Object>;
                return controller.CreatedAtAction(actionName, routeValues, ProcessFilters<T>(filteredExpando, data, excludedAttributes.Select(x => x.ToLower())));
            }
            // Handles Collections of Objects
            else
            {
                var collection = new List<object>();

                foreach (object o in (data as IEnumerable))
                {
                    var innerExpando = new ExpandoObject() as IDictionary<string, object>;
                    collection.Add(ProcessFilters(innerExpando, o, excludedAttributes));
                }

                return controller.CreatedAtAction(actionName, routeValues, collection);
            }
        }

        private static ICollection<KeyValuePair<string, object>> ProcessFilters<T>(ICollection<KeyValuePair<string, object>> expando, T data, IEnumerable<string> excludedAttributes)
        {
            // Recursive method that deals with properties of the response models.
            if (data == null) return expando;
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                // If this property is null ignore it.
                object propertyValue = property.GetValue(data);
                if (propertyValue != null)
                {
                    string propertyName = property.Name;

                    // Do not process the property if the "JsonIgnore" attribute is applied
                    bool isJsonIgnore = property.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).Length > 0;
                    if (isJsonIgnore) { continue; }

                    // Check if the property has been explicity excluded
                    bool isExcluded = excludedAttributes.Contains(propertyName.ToLower());

                    // Check if the "Excludable" attribute has been applied to the property
                    bool isExcludable = property.GetCustomAttributes(typeof(ExcludableAttribute), false).Length > 0;

                    if (isExcluded && isExcludable)
                    {
                        // If this property represents a resource, then add a collapsed version to the response expando. Otherwise, don't add anything and return (exclude the object from the model).
                        if (IsResource(propertyValue))
                        {
                            Object value = CollapsedResourceModel.From((ResourceResponseModel)propertyValue);
                            expando.Add(new KeyValuePair<string, Object>(propertyName, value));
                        }
                    }
                    else if (IsGuid(propertyValue))
                    {
                        Guid guid = (Guid)propertyValue;
                        expando.Add(new KeyValuePair<string, Object>(propertyName, guid.ToString()));
                    }
                    else
                    {
                        if (IsPrimitive(propertyValue))
                        {
                            // If this is a primitive type, then add it to the response expando (no need for further recursion).
                            expando.Add(new KeyValuePair<string, Object>(propertyName, propertyValue));
                        }
                        else if (IsCollection(property))
                        {
                            var propertyValues = propertyValue as IEnumerable;

                            // Is this a list of type squid?
                            if (propertyValues.OfType<Guid>().Count() == (propertyValues as IList).Count)
                            {
                                IList<string> squids = new List<string>();
                                foreach (var value in propertyValues)
                                {
                                    squids.Add(value.ToString());
                                }
                                expando.Add(new KeyValuePair<string, Object>(propertyName, squids));
                            }
                            else if (propertyValues.OfType<string>().Count() == (propertyValues as IList).Count)
                            {
                                IList<string> strings = new List<string>();
                                foreach (var value in propertyValues)
                                {
                                    strings.Add(value.ToString());
                                }
                                expando.Add(new KeyValuePair<string, Object>(propertyName, strings));
                            }
                            else
                            {
                                // If this is a collection type, then process filters for each of the elements in the collection.
                                IList<object> innerCollection = new List<object>();

                                // Each property in the collection
                                foreach (var value in propertyValues)
                                {
                                    var newExpando = new ExpandoObject() as ICollection<KeyValuePair<string, Object>>;
                                    innerCollection.Add(ProcessFilters(newExpando, value, excludedAttributes));
                                }

                                expando.Add(new KeyValuePair<string, Object>(propertyName, innerCollection));
                            }
                        }
                        else
                        {
                            // If this is an object type, then recursively process filters for adding each of its properties to the response expando.
                            var innerExpando = ProcessFilters(new ExpandoObject() as ICollection<KeyValuePair<string, Object>>, propertyValue, excludedAttributes);
                            expando.Add(new KeyValuePair<string, Object>(propertyName, innerExpando));
                        }
                    }
                }
            }

            return GetOrderedExpando(expando);
        }

        private static ICollection<KeyValuePair<string, object>> GetOrderedExpando(ICollection<KeyValuePair<string, object>> expando)
        {
            if (!priorityKeys.Any())
            {
                return expando;
            }

            List<KeyValuePair<string, object>> orderedList = new List<KeyValuePair<string, object>>();

            // Find priority keys by order and add them to the "orderedList"
            foreach (var key in priorityKeys)
            {
                if (expando.Any(p => p.Key.ToLower() == key))
                {
                    var found = expando.Single(e => e.Key.ToLower() == key);
                    orderedList.Add(found);
                }
            }

            // No priority keys were found - Immediately return the original expando
            if (!orderedList.Any())
                return expando;

            // Add the remaining expando properties to the "orderedList"
            var remainingPairs = expando.Where(p => !priorityKeys.Contains(p.Key.ToLower()));
            foreach (var pair in remainingPairs)
            {
                orderedList.Add(pair);
            }

            // Remove all expando items
            expando.Clear();

            // Re-populate the expando ordered by priority
            orderedList.ForEach(e => expando.Add(e));

            // Return the original expando
            return expando;
        }

        private static bool IsResource(object propertyValue)
        {
            return propertyValue is ResourceResponseModel;
        }

        private static bool IsPrimitive(object propertyValue)
        {
            var type = propertyValue.GetType();
            return
                type.IsPrimitive ||
                type == typeof(Decimal) ||
                type == typeof(string) ||
                type == typeof(Guid) ||
                type == typeof(DateTime) ||
                type == typeof(double[]) ||
                type == typeof(int[]) ||
                type == typeof(string[]) ||
                propertyValue is ElementalObjectResponseModel;
        }

        private static bool IsCollection(PropertyInfo property)
        {
            return
                (typeof(IEnumerable)).IsAssignableFrom(property.PropertyType) &&
                property.PropertyType != typeof(string);
        }

        private static bool IsGuid(object propertyValue)
        {
            return propertyValue is Guid;
        }
    }
}
