using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Npoi.Models
{
    public class ExcelOptions
    {
        /// <summary>
        /// 保存目标文件名（值为空，不生成本地文件；否则，生成本地文件）
        /// </summary>
        public string SaveFileName { get; set; }

        /// <summary>
        /// 表格文件后缀 (默认xls)
        /// </summary>
        public ExcelExtType ExtType { get; set; } = ExcelExtType.Xls;

        /// <summary>
        /// SheetName
        /// </summary>
        public string SheetName { get; set; } = "Sheet1";

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// 表头扩展
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// 表头列名映射字典
        /// </summary>
        public Dictionary<string, string> ColumnNameMappings { get; set; } = null;

        public string HeaderLeft { get; set; }
        public string HeaderCenter { get; set; }
        public string HeaderRight { get; set; }
        public string FooterLeft { get; set; }
        public string FooterCenter { get; set; }
        public string FooterRight { get; set; }

        /// <summary>
        /// 是否有生成时间
        /// </summary>
        public bool HasTime { get; set; }

        /// <summary>
        /// 是否有序号（默认有）
        /// </summary>
        public bool HasNumber { get; set; } = true;

        /// <summary>
        /// 起始序号
        /// </summary>
        public int StartNumber { get; set; } = 1;

        /// <summary>
        /// 表格行数限制（默认50万的限制）
        /// </summary>
        public int RowsCountLimit { get; set; } = 500000;

        /// <summary>
        /// 有效数据起始行数（0开始，默认1，一般表格有表头）
        /// </summary>
        public int StartRowIndex { get; set; } = 1;

        /// <summary>
        /// 有效数据起始列数（0开始，默认1，一般表格有序号）
        /// </summary>
        public int StartColumnIndex { get; set; } = 1;
    }
}
