using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Plus54PortfolioRedesign2014.Web
{
    public class BaseModel<T1, T2> where T2 : class //EntityObject 
    {
        public static List<T1> ConvertList(List<T2> list)
        {
            if (list == null)
                return new List<T1>();

            ConstructorInfo ctor = typeof(T1).GetConstructor(new[] { typeof(T2) });
            //if a constructor with parameters exists
            if (ctor != null)
                return list.Select(o => (T1)Activator.CreateInstance(typeof(T1), o)).ToList();

            return list.Select(o => GetModelFromEntity(o)).ToList();
        }

        public static List<T2> ConvertList(List<T1> list)
        {
            if (list == null)
                return new List<T2>();

            ConstructorInfo ctor = typeof(T2).GetConstructor(new[] { typeof(T1) });
            //if a constructor with parameters exists
            if (ctor != null)
                return list.Select(o => (T2)Activator.CreateInstance(typeof(T2), o)).ToList();

            return list.Select(o => GetEntityFromModel(o)).ToList();
        }

        public static T2 GetEntityFromModel(T1 model)
        {
            var entity = Activator.CreateInstance(typeof(T2));

            foreach (PropertyInfo info in model.GetType().GetProperties())
            {
                if (info.CanWrite && entity.GetType().GetProperties().Where(a => a.Name == info.Name).Count() > 0)
                {
                    var targetProperty = entity.GetType().GetProperty(info.Name);
                    targetProperty.SetValue(entity, info.GetValue(model, null), null);
                }
            }
            return entity as T2;
        }

        public static T1 GetModelFromEntity(T2 entity)
        {
            var model = Activator.CreateInstance(typeof(T1));

            foreach (PropertyInfo info in entity.GetType().GetProperties())
            {
                if (info.CanWrite && model.GetType().GetProperties().Where(a => a.Name == info.Name).Count() > 0)
                {
                    var targetProperty = model.GetType().GetProperty(info.Name);
                    targetProperty.SetValue(model, info.GetValue(entity, null), null);
                }
            }
            return (T1)model;
        }

        public void UpdateModelFromEntity(T2 entity)
        {
            foreach (PropertyInfo info in entity.GetType().GetProperties())
            {
                if (info.CanWrite && this.GetType().GetProperties().Where(a => a.Name == info.Name).Count() > 0)
                {
                    var targetProperty = this.GetType().GetProperty(info.Name);
                    targetProperty.SetValue(this, info.GetValue(entity, null), null);
                }
            }
        }
    }
}