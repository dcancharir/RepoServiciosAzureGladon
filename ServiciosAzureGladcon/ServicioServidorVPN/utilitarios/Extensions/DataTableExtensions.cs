using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ServicioServidorVPN.utilitarios.Extensions {
    public static class DataTableExtensions {
        public static DataTable ToDataTable<T>(this IEnumerable<T> items) {
            var dataTable = new DataTable(typeof(T).Name);

            // Obtener las propiedades del tipo T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Agregar columnas al DataTable
            foreach(var property in properties) {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            // Agregar filas con los valores de los objetos
            foreach(var item in items) {
                var values = new object[properties.Length];
                for(int i = 0; i < properties.Length; i++) {
                    values[i] = properties[i].GetValue(item);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
