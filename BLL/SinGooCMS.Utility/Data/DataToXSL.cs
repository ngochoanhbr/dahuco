using System;
using System.Data;
using System.Configuration;
using System.IO;
using org.in2bits.MyXls; //MyXls命名空间

namespace SinGooCMS.Utility
{
    /// <summary>
    /// dataset导出excel
    /// </summary>
    public class DataToXSL
    {
        #region DataTable 导出为 Excel

        /// <summary>
        /// 生成XSL
        /// </summary>
        /// <param name="table">DataTable对象</param>
        /// <param name="path">保存路径(包含文件名)</param>
        /// <returns></returns>
        public static bool CreateXLS(DataTable table, string path)
        {
            return CreateXLS(table, path, true);
        }

        /// <summary>
        /// 生成XSL
        /// </summary>
        /// <param name="table">DataTable对象</param>
        /// <param name="path">保存路径(包含文件名)</param>
        /// <param name="overwrite">是否覆盖</param>
        /// <returns></returns>
        public static bool CreateXLS(DataTable table, string path, bool overwrite)
        {
            if (File.Exists(path) && !overwrite)
                return false;

            try
            {
                //1.创建xls对象
                XlsDocument xlsDoc = new XlsDocument();
                xlsDoc.FileName = Path.GetFileName(path);

                //2.创建表
                string sheetName = string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName;
                Worksheet sheet = xlsDoc.Workbook.Worksheets.Add(sheetName);

                //3.创建行列,注意cellRow,cellColumn都必须>=1
                Cells cells = sheet.Cells;

                //3.1 添加字段名
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Cell cell = cells.Add(1, col + 1, table.Columns[col].ColumnName);
                    cell.Font.Weight = FontWeight.Bold;
                }

                //3.2 添加记录
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        cells.Add(row + 2, col + 1, string.IsNullOrEmpty(table.Rows[row][col].ToString()) ? "-" : table.Rows[row][col].ToString());
                    }
                }

                //4.准备保存文件夹
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                //5.保存
                xlsDoc.Save(Path.GetDirectoryName(path), overwrite);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

        #region DataSet 导出为 Excel

        /// <summary>
        /// 生成XSL
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="path">保存路径(包含文件名)</param>
        /// <returns></returns>
        public static bool CreateXLS(DataSet ds, string path)
        {
            return CreateXLS(ds, path, true);
        }

        /// <summary>
        /// 生成XSL
        /// </summary>
        /// <param name="ds">DataSet对象</param>
        /// <param name="path">保存路径(包含文件名)</param>
        /// <param name="overwrite">是否覆盖</param>
        /// <returns></returns>
        public static bool CreateXLS(DataSet ds, string path, bool overwrite)
        {
            if (File.Exists(path) && !overwrite)
                return false;

            try
            {
                //1.创建xls对象
                XlsDocument xlsDoc = new XlsDocument();
                xlsDoc.FileName = Path.GetFileName(path);

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    //2.创建表
                    string sheetName = string.IsNullOrEmpty(ds.Tables[i].TableName) ? "Sheet" + i.ToString() : ds.Tables[i].TableName;
                    Worksheet sheet = xlsDoc.Workbook.Worksheets.Add(sheetName);

                    //3.创建行列,注意cellRow,cellColumn都必须>=1
                    Cells cells = sheet.Cells;

                    //3.1 添加字段名
                    for (int col = 0; col < ds.Tables[i].Columns.Count; col++)
                    {
                        Cell cell = cells.Add(1, col + 1, ds.Tables[i].Columns[col].ColumnName);
                        cell.Font.Weight = FontWeight.Bold;
                    }

                    //3.2 添加记录
                    for (int row = 0; row < ds.Tables[i].Rows.Count; row++)
                    {
                        for (int col = 0; col < ds.Tables[i].Columns.Count; col++)
                        {
                            cells.Add(row + 2, col + 1, string.IsNullOrEmpty(ds.Tables[i].Rows[row][col].ToString()) ? "-" : ds.Tables[i].Rows[row][col].ToString());
                        }
                    }
                }

                //4.准备保存文件夹
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                //5.保存
                xlsDoc.Save(Path.GetDirectoryName(path), overwrite);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}