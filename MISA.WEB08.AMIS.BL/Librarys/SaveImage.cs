using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;

namespace MISA.WEB08.AMIS.BL
{
    public class SaveFileImage
    {
        /// <summary>
        /// Code để tạo filestream từ stream và ghi vào ổ đĩa
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public static string SaveExcelFileToDisk(Stream stream, string fileName)
        {
            // Đường dẫn đến thư mục trong source code backend
            var path = Path.Combine(Directory.GetCurrentDirectory(), DataContext.Path_root + "/Excel/Export/");

            // Nếu thư mục không tồn tại, tạo mới thư mục
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Tạo đường dẫn đầy đủ đến tập tin Excel
            var filePath = Path.Combine(path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
            return "/Excel/Export/" + fileName;
        }

        /// <summary>
        /// Hàm lấy ra column name theo số cột muốn lấy
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public static string GetColumnName(int index)
        {
            string columnName = "";
            while (index > 0)
            {
                int remainder = (index - 1) % 26;
                columnName = (char)(65 + remainder) + columnName;
                index = (index - 1) / 26;
            }
            return columnName;
        }

        /// <summary>
        /// Hàm xử lý đọc file excel
        /// </summary>
        /// <param name="path"></param>
        /// <param name="headerRow"></param>
        /// <param name="messageError"></param>
        /// <returns></returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public static DataTable ReadFromExcelFile(string path, int headerRow, out string messageError)
        {
            DataTable result = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    messageError = "FILE_NOT_EXISTS";
                    return null;
                }

                if (!string.IsNullOrEmpty(path) && Path.HasExtension(path) && Path.GetExtension(path).ToLower() != ".xlsx")
                {
                    messageError = "WRONG_FORMAT_FILE";
                    return null;
                }

                using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
                {
                    var workBook = package.Workbook;
                    if (workBook == null || workBook.Worksheets.Count == 0)
                    {
                        messageError = "EMPTY_DATA";
                        return null;
                    }
                    var workSheet = workBook.Worksheets.First();
                    // Đọc từng header column
                    int columnCount = 0;
                    foreach (var firstRowCell in workSheet.Cells[headerRow, 1, headerRow, workSheet.Dimension.End.Column])
                    {
                        string columnName = "" + firstRowCell.Text.Trim();
                        if (string.IsNullOrEmpty(columnName))
                            break;
                        columnCount++;
                        result.Columns.Add(firstRowCell.Text.Trim());
                    }
                    //Đọc dữ liệu từ từng header row
                    for (var rowNumber = headerRow + 1; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                    {
                        var row = workSheet.Cells[rowNumber, 1, rowNumber, columnCount];
                        var newRow = result.NewRow();
                        bool isRowData = false;
                        string str = "";
                        //Đọc và check rỗng
                        foreach (var cell in row)
                        {
                            if (cell != null)
                                isRowData = true;

                            newRow[cell.Start.Column - 1] = cell.Value;
                            str += cell.Value != null ? cell.Value : "";
                        }
                        //Đưa dữ liệu vào datatable
                        if (isRowData && !string.IsNullOrEmpty(str.Trim()))
                            result.Rows.Add(newRow);
                        if (string.IsNullOrEmpty(str.Trim())) break;
                    }
                }
                messageError = "";
            }
            catch (Exception ex) { messageError = "ERROR:" + ex.Message; }

            return result;
        }

        /// <summary>
        /// Hàm thực hiện xoá file
        /// </summary>
        /// <param name="pathFileName">Đường dẫn file và tên file</param>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public static void DeleteFile(string pathFileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathFileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
