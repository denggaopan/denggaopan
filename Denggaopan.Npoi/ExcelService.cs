using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Denggaopan.Npoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Denggaopan.Helpers;

namespace Denggaopan.Npoi
{
    public class ExcelService : IExcelService
    {
        /// <summary>
        /// 将List导出Excel文件流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="title"></param>
        /// <param name="contitions"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream ExportExcel<T>(List<T> list, string title, string contitions, string fileName = "") where T : new()
        {
            var dt = list.ToDataTable();
            return ExportExcel(dt, title, contitions, fileName);
        }

        /// <summary>
        /// 将DataTable分割后的DataSet导出一个多sheet的Excel文件流
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="title"></param>
        /// <param name="contitions"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream ExportExcel(DataSet ds, string title, string contitions, string fileName = "")
        {
            var book = new HSSFWorkbook();
            var index = 0;
            foreach (DataTable dt in ds.Tables)
            {
                var sheetName = $"sheet{ ++index }";
                createSheet(ref book, dt, title, contitions, sheetName);
            }

            Stream stream;
            if (string.IsNullOrEmpty(fileName))
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            }
            book.Write(stream);
            stream.Position = 0;
            return stream;
        }


        /// <summary>
        /// 将DataTable导出Excel文件流
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="title">文件名称</param>
        /// <param name="contitions">导出条件</param>
        /// <param name="fileName">文件名</param>
        public Stream ExportExcel(DataTable dt, string title, string contitions, string fileName = "")
        {
            var book = new HSSFWorkbook();
            createSheet(ref book, dt, title, contitions);

            Stream stream;
            if (string.IsNullOrEmpty(fileName))
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            }
            book.Write(stream);
            stream.Position = 0;
            return stream;
        }


        /// <summary>
        /// 将List分割后导出一个多sheet的Excel文件流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rowsCountLimit"></param>
        /// <param name="title"></param>
        /// <param name="contitions"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream ExportExcel<T>(List<T> list, int rowsCountLimit, string title, string contitions, string fileName = "") where T : new()
        {
            var ds = list.ToDataTable().Slice(rowsCountLimit);
            return ExportExcel(ds, title, contitions, fileName);
        }

        /// <summary>
        /// 将DataTable分割后导出一个多sheet的Excel文件流
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowsCountLimit"></param>
        /// <param name="title"></param>
        /// <param name="contitions"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream ExportExcel(DataTable dt, int rowsCountLimit, string title, string contitions, string fileName = "")
        {
            var ds = dt.Slice(rowsCountLimit);
            return ExportExcel(ds, title, contitions, fileName);
        }

        /// <summary>
        /// 将Excel文件导入到List
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="startRowNum">起始行数（从0开始）</param>
        /// <returns>List<T></returns>
        public List<T> ImportExcel<T>(string fileName, string sheetName = null, int startRowIndex = 1, int startColumnIndex = 1) where T : new()
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            if (startRowIndex < 0)
            {
                startRowIndex = 0;
            }
            if (startColumnIndex < 0)
            {
                startColumnIndex = 0;
            }

            //根据指定路径读取文件
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            //实例化T数组
            List<T> list = new List<T>();
            //获取数据
            list = ImportExcel<T>(fs, sheetName, startRowIndex, startColumnIndex);

            return list;
        }

        /// <summary>
        /// 将Excel文件流导入到List
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="startRowNum">起始行数（从0开始）</param>
        /// <returns>List<T></returns>
        public List<T> ImportExcel<T>(Stream fileStream, string sheetName = null, int startRowIndex = 1, int startColumnIndex = 1) where T : new()
        {
            if (startRowIndex < 0)
            {
                startRowIndex = 0;
            }
            if (startColumnIndex < 0)
            {
                startColumnIndex = 0;
            }

            //创建Excel数据结构
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            //如果有指定工作表名称
            ISheet sheet = null;
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = workbook.GetSheet(sheetName);
                //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                //如果没有指定的sheetName，则尝试获取第一个sheet
                sheet = workbook.GetSheetAt(0);
            }
            //实例化T数组
            List<T> list = new List<T>();
            if (sheet != null)
            {
                //一行最后一个cell的编号 即总的列数
                IRow cellNum = sheet.GetRow(0);
                int num = cellNum.LastCellNum;
                //获取泛型对象T的所有属性
                var propertys = typeof(T).GetProperties();
                //每行转换为单个T对象
                for (int i = startRowIndex; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    var obj = new T();
                    var pi = 0;
                    for (int j = startColumnIndex; j < num; j++)
                    {
                        //没有数据的单元格都默认是null
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            var value = row.GetCell(j).ToString();
                            string str = (propertys[pi].PropertyType).FullName;
                            if (str == "System.String")
                            {
                                propertys[pi].SetValue(obj, value, null);
                            }
                            else if (str == "System.DateTime")
                            {
                                DateTime pdt = Convert.ToDateTime(value);
                                propertys[pi].SetValue(obj, pdt, null);
                            }
                            else if (str == "System.Boolean")
                            {
                                bool pb = Convert.ToBoolean(value);
                                propertys[pi].SetValue(obj, pb, null);
                            }
                            else if (str == "System.Int16")
                            {
                                short pi16 = Convert.ToInt16(value);
                                propertys[pi].SetValue(obj, pi16, null);
                            }
                            else if (str == "System.Int32")
                            {
                                int pi32 = Convert.ToInt32(value);
                                propertys[pi].SetValue(obj, pi32, null);
                            }
                            else if (str == "System.Int64")
                            {
                                long pi64 = Convert.ToInt64(value);
                                propertys[pi].SetValue(obj, pi64, null);
                            }
                            else if (str == "System.Byte")
                            {
                                byte pb = Convert.ToByte(value);
                                propertys[pi].SetValue(obj, pb, null);
                            }
                            else if (str == "System.Decimal")
                            {
                                var d = Convert.ToDecimal(value);
                                propertys[pi].SetValue(obj, d, null);
                            }
                            else
                            {
                                propertys[pi].SetValue(obj, null, null);
                            }
                        }
                        pi++;
                    }
                    list.Add(obj);
                }
            }
            return list;
        }


        public Stream ExportExcel<T>(List<T> list, ExcelOptions options) where T : new()
        {
            var dt = list.ToDataTable();
            return ExportExcel(dt, options);
        }

        public Stream ExportExcel(DataTable dt, ExcelOptions options)
        {
            if (options == null)
            {
                options = new ExcelOptions();
            }
            var ds = dt.Slice(options.RowsCountLimit);

            return ExportExcel(ds, options);
        }

        /// <summary>
        /// 将DataTable分割后的DataSet导出一个多sheet的Excel文件流
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="options">ExcelOptions.RowsCountList此时失效,默认dataset的每个datatable的count小于RowsCountList</param>
        /// <returns></returns>
        public Stream ExportExcel(DataSet ds, ExcelOptions options)
        {
            var book = new HSSFWorkbook();
            var index = 0;
            foreach (DataTable dt in ds.Tables)
            {
                var startNumber = options.RowsCountLimit * index + 1;
                var sheetName = (string.IsNullOrEmpty(options.SheetName) || options.SheetName == "Sheet1") ? "Sheet" : options.SheetName;
                sheetName = $"{sheetName}{ ++index }";
                createSheet(ref book,
                    dt,
                    options.Title,
                    options.Conditions,
                    sheetName,
                    options.HasTime,
                    options.Headers,
                    options.ColumnNameMappings,
                    options.HasNumber,
                    startNumber,
                    options.HeaderLeft,
                    options.HeaderCenter,
                    options.HeaderRight,
                    options.FooterLeft,
                    options.FooterCenter,
                    options.FooterRight);
            }

            Stream stream;
            if (string.IsNullOrEmpty(options.SaveFileName))
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = new FileStream(options.SaveFileName, FileMode.Create, FileAccess.ReadWrite);
            }
            book.Write(stream);
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// 将Excel文件流导入到List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public List<T> ImportExcel<T>(string fileName, ExcelOptions options) where T : new()
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                return null;
            }

            //根据指定路径读取文件
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            //实例化T数组
            List<T> list = new List<T>();
            //获取数据
            list = ImportExcel<T>(fs, options);

            return list;
        }

        /// <summary>
        /// 将Excel文件流导入到List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileStream"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public List<T> ImportExcel<T>(Stream fileStream, ExcelOptions options) where T : new()
        {
            if (options == null)
            {
                options = new ExcelOptions();
            }
            if (options.StartRowIndex < 0)
            {
                options.StartRowIndex = 0;
            }
            if (options.StartColumnIndex < 0)
            {
                options.StartColumnIndex = 0;
            }

            //创建Excel数据结构
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            //如果有指定工作表名称
            ISheet sheet = null;
            if (!string.IsNullOrEmpty(options.SheetName))
            {
                sheet = workbook.GetSheet(options.SheetName);
                //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                //如果没有指定的sheetName，则尝试获取第一个sheet
                sheet = workbook.GetSheetAt(0);
            }
            //实例化T数组
            List<T> list = new List<T>();
            if (sheet != null)
            {
                //一行最后一个cell的编号 即总的列数
                IRow cellNum = sheet.GetRow(0);
                int num = cellNum.LastCellNum;
                //获取泛型对象T的所有属性
                var propertys = typeof(T).GetProperties();
                //每行转换为单个T对象
                for (int i = options.StartRowIndex; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    var obj = new T();
                    var pi = 0;
                    for (int j = options.StartColumnIndex; j < num; j++)
                    {
                        //没有数据的单元格都默认是null
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            var value = row.GetCell(j).ToString();
                            string str = (propertys[pi].PropertyType).FullName;
                            if (str == "System.String")
                            {
                                propertys[pi].SetValue(obj, value, null);
                            }
                            else if (str == "System.DateTime")
                            {
                                DateTime pdt = Convert.ToDateTime(value);
                                propertys[pi].SetValue(obj, pdt, null);
                            }
                            else if (str == "System.Boolean")
                            {
                                bool pb = Convert.ToBoolean(value);
                                propertys[pi].SetValue(obj, pb, null);
                            }
                            else if (str == "System.Int16")
                            {
                                short pi16 = Convert.ToInt16(value);
                                propertys[pi].SetValue(obj, pi16, null);
                            }
                            else if (str == "System.Int32")
                            {
                                int pi32 = Convert.ToInt32(value);
                                propertys[pi].SetValue(obj, pi32, null);
                            }
                            else if (str == "System.Int64")
                            {
                                long pi64 = Convert.ToInt64(value);
                                propertys[pi].SetValue(obj, pi64, null);
                            }
                            else if (str == "System.Byte")
                            {
                                byte pb = Convert.ToByte(value);
                                propertys[pi].SetValue(obj, pb, null);
                            }
                            else if (str == "System.Decimal")
                            {
                                var d = Convert.ToDecimal(value);
                                propertys[pi].SetValue(obj, d, null);
                            }
                            else
                            {
                                propertys[pi].SetValue(obj, null, null);
                            }
                        }
                        pi++;
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

        #region private

        private static void createSheet(ref HSSFWorkbook book,
            DataTable dt,
            string title,
            string contitions,
            string sheetName = "Sheet1",
            bool hasTime = false,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> columnNameMappings = null,
            bool hasNumber = false,
            int startNumber = 1,
            string hearderLeft = "",
            string headerCenter = "",
            string headerRight = "",
            string footerLeft = "",
            string footCenter = "",
            string footerRight = "")
        {
            var sheet1 = book.CreateSheet(sheetName);
            int dtRows = dt.Rows.Count;
            int dtColums = hasNumber ? (dt.Columns.Count + 1) : dt.Columns.Count;
            var currentRowIndex = 0;

            //标题样式
            var style0 = book.CreateCellStyle();
            style0.Alignment = HorizontalAlignment.Center;//水平居中 
            style0.VerticalAlignment = VerticalAlignment.Center;//垂直居中
            style0.WrapText = true;//自动换行
            var font0 = book.CreateFont();
            font0.IsBold = true;
            font0.FontHeightInPoints = 16;
            style0.SetFont(font0);

            //标题合并单元格
            if (!string.IsNullOrEmpty(title))
            {
                sheet1.AddMergedRegion(new CellRangeAddress(currentRowIndex, currentRowIndex, 0, dtColums - 1));
                var row0 = sheet1.CreateRow(0);
                row0.HeightInPoints = 30;
                var cell0 = row0.CreateCell(0);
                cell0.SetCellValue(title);
                cell0.CellStyle = style0;
                currentRowIndex++;
            }

            var style1 = book.CreateCellStyle();
            style1.VerticalAlignment = VerticalAlignment.Center;
            style1.WrapText = true;
            //style1.BorderRight = BorderStyle.Thin;
            //style1.BorderTop = BorderStyle.Thin;
            //style1.BorderLeft = BorderStyle.Thin;
            //style1.BorderBottom = BorderStyle.Thin;
            var style1Name = book.CreateCellStyle();
            style1Name.CloneStyleFrom(style1);
            style1Name.Alignment = HorizontalAlignment.Center;//水平居中 
            var font1 = book.CreateFont();
            font1.IsBold = true;
            font1.FontHeightInPoints = 10;
            style1Name.SetFont(font1);


            IRow row1 = null;
            //制表时间
            if (hasTime)
            {
                row1 = sheet1.CreateRow(currentRowIndex);
                row1.HeightInPoints = 25;
                var cell1 = row1.CreateCell(dtColums - 2);
                cell1.CellStyle = style1Name;
                cell1.SetCellValue("制表时间");

                var cell2 = row1.CreateCell(dtColums - 1);
                cell2.CellStyle = style1;
                cell2.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                currentRowIndex++;

            }

            //导出条件合并单元格（如果有制表时间，就与之一行）
            if (!string.IsNullOrEmpty(contitions))
            {
                if (hasTime && dtColums >= 3)
                {
                    sheet1.AddMergedRegion(new CellRangeAddress(currentRowIndex - 1, currentRowIndex - 1, 0, dtColums - 3));
                    var cell0 = row1.CreateCell(0);
                    cell0.CellStyle = style1;
                    cell0.SetCellValue(contitions);
                }
                else
                {
                    sheet1.AddMergedRegion(new CellRangeAddress(currentRowIndex, currentRowIndex, 0, dtColums - 1));
                    row1 = sheet1.CreateRow(currentRowIndex);
                    row1.HeightInPoints = 25;
                    var cell0 = row1.CreateCell(0);
                    cell0.CellStyle = style1;
                    cell0.SetCellValue(contitions);
                    currentRowIndex++;
                }
            }

            //表头扩展
            if (headers != null && headers.Count > 0)
            {
                IRow rowx = null;
                var hi = 0;
                foreach (var item in headers)
                {
                    if (hi++ == 0 && hasTime && string.IsNullOrEmpty(contitions) && dtColums >= 4)
                    {
                        sheet1.AddMergedRegion(new CellRangeAddress(currentRowIndex - 1, currentRowIndex - 1, 1, dtColums - 3));
                        rowx = row1;

                        var cell0 = rowx.CreateCell(0);
                        cell0.CellStyle = style1Name;
                        cell0.SetCellValue(item.Key);
                        var cell1 = rowx.CreateCell(1);
                        cell1.CellStyle = style1;
                        cell1.SetCellValue(item.Value);
                    }
                    else
                    {
                        sheet1.AddMergedRegion(new CellRangeAddress(currentRowIndex, currentRowIndex, 1, dtColums - 1));
                        rowx = sheet1.CreateRow(currentRowIndex);

                        rowx.HeightInPoints = 25;
                        var cell0 = rowx.CreateCell(0);
                        cell0.CellStyle = style1Name;
                        cell0.SetCellValue(item.Key);
                        var cell1 = rowx.CreateCell(1);
                        cell1.CellStyle = style1;
                        cell1.SetCellValue(item.Value);
                        currentRowIndex++;
                    }
                }
            }

            //列名样式
            var style2 = book.CreateCellStyle();
            style2.Alignment = HorizontalAlignment.Center;//水平居中 
            style2.VerticalAlignment = VerticalAlignment.Center;//垂直居中 
            style2.WrapText = true;//自动换行
            var font2 = book.CreateFont();
            font2.IsBold = true;
            font2.FontHeightInPoints = 10;
            style2.SetFont(font2);
            style2.BorderBottom = BorderStyle.Thin;
            style2.BorderLeft = BorderStyle.Thin;
            style2.BorderRight = BorderStyle.Thin;
            style2.BorderTop = BorderStyle.Thin;
            style2.Alignment = HorizontalAlignment.Center;
            style2.VerticalAlignment = VerticalAlignment.Center;
            style2.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style2.FillPattern = FillPattern.SolidForeground;

            //列名
            var row = sheet1.CreateRow(currentRowIndex++);
            row.HeightInPoints = 28;
            for (int i = 0; i < dtColums; i++)
            {
                var cellCo = row.CreateCell(i);
                cellCo.CellStyle = style2;

                if (hasNumber && i == 0)
                {
                    cellCo.SetCellValue("序号");
                    sheet1.SetColumnWidth(i, 10 * 256);
                    continue;
                }
                var c = hasNumber ? (i - 1) : i;
                var columnName = dt.Columns[c].ColumnName;
                if (columnNameMappings != null && columnNameMappings.ContainsKey(columnName))
                {
                    columnName = columnNameMappings[columnName];
                }
                cellCo.SetCellValue(columnName);
                sheet1.SetColumnWidth(i, 20 * 256);
            }

            //行内容样式
            var style3 = book.CreateCellStyle();
            var styleInt = book.CreateCellStyle();
            var styleDecimal = book.CreateCellStyle();
            var styleBool = book.CreateCellStyle();
            var styleBoolYes = book.CreateCellStyle();
            var styleBoolNo = book.CreateCellStyle();

            style3.WrapText = true;
            styleInt.DataFormat = 0;
            style3.BorderBottom = BorderStyle.Thin;
            style3.BorderLeft = BorderStyle.Thin;
            style3.BorderRight = BorderStyle.Thin;
            style3.BorderTop = BorderStyle.Thin;
            style3.Alignment = HorizontalAlignment.Center;
            style3.VerticalAlignment = VerticalAlignment.Center;
            style3.FillForegroundColor = HSSFColor.White.Index;
            style3.FillPattern = FillPattern.SolidForeground;

            styleBool.CloneStyleFrom(style3);
            styleBoolYes.CloneStyleFrom(styleBool);
            styleBoolYes.FillForegroundColor = HSSFColor.Green.Index;
            styleBoolNo.CloneStyleFrom(styleBool);
            styleBoolNo.FillForegroundColor = HSSFColor.Red.Index;

            styleInt.CloneStyleFrom(style3);
            styleInt.Alignment = HorizontalAlignment.Right;
            styleInt.DataFormat = 38;
            styleDecimal.CloneStyleFrom(style3);
            styleDecimal.Alignment = HorizontalAlignment.Right;
            styleDecimal.DataFormat = 40;

            //行
            for (int i = 0; i < dtRows; i++)
            {
                var rowtemp = sheet1.CreateRow(i + currentRowIndex);
                rowtemp.HeightInPoints = 25;
                for (int j = 0; j < dtColums; j++)
                {
                    var cellCo = rowtemp.CreateCell(j);

                    if (hasNumber && j == 0)
                    {
                        cellCo.SetCellValue(startNumber + i);
                        cellCo.CellStyle = style3;
                        continue;
                    }

                    var c = hasNumber ? (j - 1) : j;

                    if (dt.Columns[c].DataType == typeof(string) || dt.Columns[c].DataType == typeof(DateTime))
                    {
                        cellCo.SetCellValue(Convert.ToString(dt.Rows[i][c]));
                        cellCo.CellStyle = style3;
                    }
                    else if (dt.Columns[c].DataType == typeof(decimal) || dt.Columns[c].DataType == typeof(float) || dt.Columns[c].DataType == typeof(double))
                    {
                        cellCo.CellStyle = styleDecimal;
                        if (dt.Rows[i][c] != DBNull.Value)
                        {
                            cellCo.SetCellValue(Convert.ToDouble(dt.Rows[i][c]));
                        }
                    }
                    else if (dt.Columns[c].DataType == typeof(bool))
                    {
                        cellCo.CellStyle = styleBool;
                        cellCo.SetCellValue(Convert.ToBoolean(dt.Rows[i][c]) ? "是" : "否");
                    }
                    else // int
                    {
                        cellCo.CellStyle = styleInt;
                        if (dt.Rows[i][c] != DBNull.Value)
                        {
                            cellCo.SetCellValue(Convert.ToDouble(dt.Rows[i][c]));
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(hearderLeft))
            {
                sheet1.Header.Left = hearderLeft;
            }
            if (!string.IsNullOrEmpty(headerCenter))
            {
                sheet1.Header.Center = headerCenter;
            }
            if (!string.IsNullOrEmpty(headerRight))
            {
                sheet1.Header.Right = headerRight;
            }
            if (!string.IsNullOrEmpty(footerLeft))
            {
                sheet1.Footer.Left = footerLeft;
            }
            if (!string.IsNullOrEmpty(footCenter))
            {
                sheet1.Footer.Center = footCenter;
            }
            if (!string.IsNullOrEmpty(footerRight))
            {
                sheet1.Footer.Right = footerRight;
            }

        }

        private static HSSFWorkbook createWorkbook(DataTable dt, ExcelOptions options)
        {
            if (options == null)
            {
                options = new ExcelOptions();
            }
            var book = new HSSFWorkbook();
            var ds = dt.Slice(options.RowsCountLimit);
            var index = 0;
            foreach (DataTable t in ds.Tables)
            {
                var startNumber = options.RowsCountLimit * index + 1;
                var sheetName = (string.IsNullOrEmpty(options.SheetName) || options.SheetName == "Sheet1") ? "Sheet" : options.SheetName;
                sheetName = $"{sheetName}{ ++index }";
                createSheet(ref book,
                    t,
                    options.Title,
                    options.Conditions,
                    sheetName,
                    options.HasTime,
                    options.Headers,
                    options.ColumnNameMappings,
                    options.HasNumber,
                    startNumber,
                    options.HeaderLeft,
                    options.HeaderCenter,
                    options.HeaderRight,
                    options.FooterLeft,
                    options.FooterCenter,
                    options.FooterRight);
            }
            return book;
        }

        #endregion
    }
}
