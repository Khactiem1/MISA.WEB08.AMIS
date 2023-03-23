using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using MISA.WEB08.AMIS.Common.Resources;
using MISA.WEB08.AMIS.DL;
using OfficeOpenXml;

namespace MISA.WEB08.AMIS.BL
{
    public class SaveFileImage
    {
        /// <summary>
        /// Save image to Folder's Avatar
        /// </summary>
        /// <param name="userProfile"></param>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        public static string Post(string imageBase64, string url)
        {
            string nameImage = "";
            //For demo purpose I only use jpg file and save file name by userId
            using (Image image = Base64ToImage(imageBase64.Split(",")[1]))
            {
                nameImage = Guid.NewGuid().ToString() + ".Jpeg";
                string filePath = $@"{DataContext.Path_root}/Images/{url}/" + nameImage;
                image.Save(filePath);
            }
            return Resource.Path_SaveImageInDB + url + "/" + nameImage;
        }

        /// <summary>
        /// Chuyển đổi hình ảnh base64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        /// CreatedBy: Nguyễn Khắc Tiềm (6/10/2022)
        private static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (Image image = Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }

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
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int baseNumber = letters.Length;
            List<int> digits = new List<int>();
            while (index > 0)
            {
                digits.Add(index % baseNumber);
                index = (index / baseNumber);
            }
            string columnName = "";
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                columnName += letters[digits[i]];
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
        /// <param name="fileName"></param>
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
