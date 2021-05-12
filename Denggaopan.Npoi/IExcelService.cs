using Denggaopan.Npoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Denggaopan.Npoi
{
    public interface IExcelService
    {
        Stream ExportExcel(DataTable dt, string title, string conditions, string fileName = "");
        Stream ExportExcel(DataTable dt, int rowsCountLimit, string title, string conditions, string fileName = "");
        Stream ExportExcel(DataSet ds, string title, string conditions, string fileName = "");
        Stream ExportExcel<T>(List<T> list, string title, string conditions, string fileName = "") where T : new();
        Stream ExportExcel<T>(List<T> list, int rowsCountLimit, string title, string conditions, string fileName = "") where T : new();
        List<T> ImportExcel<T>(string fileName, string sheetName = null, int startRowIndex = 1, int startColumnIndex = 1) where T : new();
        List<T> ImportExcel<T>(Stream fileStream, string sheetName = null, int startRowIndex = 1, int startColumnIndex = 1) where T : new();


        Stream ExportExcel(DataTable dt, ExcelOptions options);
        Stream ExportExcel(DataSet ds, ExcelOptions options);
        Stream ExportExcel<T>(List<T> list, ExcelOptions options) where T : new();
        List<T> ImportExcel<T>(string fileName, ExcelOptions options) where T : new();
        List<T> ImportExcel<T>(Stream fileStream, ExcelOptions options) where T : new();
    }
}
