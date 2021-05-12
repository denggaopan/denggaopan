using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// datatable转list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : new()
        {
            List<T> ts = new List<T>();
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (value.GetType().ToString().ToLower() == "system.double")
                            {
                                pi.SetValue(t,Convert.ToDecimal(value), null);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection) where T : new()
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        /// <summary>
        /// 分割数据表
        /// </summary>
        /// <param name="dt">需要分解的表</param>
        /// <param name="rowsLimit">每个表包含的数据量</param>
        /// <returns></returns>
        public static DataSet Slice(this DataTable dt, int rowsLimit)
        {
            //获取所需创建的表数量
            int tableNum = dt.Rows.Count / rowsLimit;
            //获取数据余数
            int remainder = dt.Rows.Count % rowsLimit;
            DataSet ds = new DataSet();
            //如果只需要创建1个表，直接将原始表存入DataSet
            if (tableNum == 0)
            {
                ds.Tables.Add(dt);
            }
            else
            {
                if (remainder > 0)
                {
                    tableNum++;
                }

                DataTable[] tableSlice = new DataTable[tableNum];
                //Save orginal columns into new table.            
                for (int c = 0; c < tableNum; c++)
                {
                    tableSlice[c] = new DataTable();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        tableSlice[c].Columns.Add(dc.ColumnName, dc.DataType);
                    }
                }
                //Import Rows
                int i;
                if (remainder > 0)
                {
                    for (i = 0; i < tableNum; i++)
                    {
                        // if the current table is not the last one
                        if (i != tableNum - 1)
                        {

                            for (int j = i * rowsLimit; j < ((i + 1) * rowsLimit); j++)
                            {
                                tableSlice[i].ImportRow(dt.Rows[j]);
                            }
                        }
                        else
                        {
                            for (int k = i * rowsLimit; k < (i * rowsLimit + remainder); k++)
                            {
                                tableSlice[i].ImportRow(dt.Rows[k]);
                            }
                        }
                    }
                }
                else
                {
                    for (i = 0; i < tableNum; i++)
                    {
                        for (int j = i * rowsLimit; j < ((i + 1) * rowsLimit); j++)
                        {
                            tableSlice[i].ImportRow(dt.Rows[j]);
                        }
                    }
                }
                //add all tables into a dataset                
                foreach (DataTable tableItem in tableSlice)
                {
                    ds.Tables.Add(tableItem);
                }
            }
            return ds;
        }

        public static string ToCsv(this DataTable dt)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            List<string> hh = new List<string>();
            foreach (DataColumn clm in dt.Columns)
            {
                hh.Add(clm.ColumnName);
            }
            sb.Append(string.Join(",", hh));

            sb.AppendLine();
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colum = dt.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
