using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// nhân viên ứng với bảng employee trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Employee
    {
        /// <summary>
        /// id nhân viên
        /// </summary>
        public Guid employee_id { get; set; }

        /// <summary>
        /// id đơn vị
        /// </summary>
        public Guid unit_id { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string unit_name { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string employee_code { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        public string employee_name { get; set; }

        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime date_of_birth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public Gender gender { get; set; }

        /// <summary>
        /// chứng minh thư
        /// </summary>
        public string identity_card { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        public string branch_bank { get; set; }

        /// <summary>
        /// chức danh
        /// </summary>
        public string employee_title { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        public string bank_account { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string name_bank { get; set; }

        /// <summary>
        /// ngày cấp cmnd
        /// </summary>
        public string day_for_identity { get; set; }

        /// <summary>
        /// địa chỉ cấp cmnd
        /// </summary>
        public string grant_address_identity { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string phone_number { get; set; }

        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        public string landline_phone { get; set; }

        /// <summary>
        /// điạ chỉ email
        /// </summary>
        public string employee_email { get; set; }

        /// <summary>
        /// địa chỉ nhân viên
        /// </summary>
        public string employee_address { get; set; }

        /// <summary>
        /// Người thêm
        /// </summary>
        public string created_by { get; set; }

        /// <summary>
        /// ngày thêm
        /// </summary>
        public DateTime created_date { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        public string modified_by { get; set; }

        /// <summary>
        /// ngày sửa
        /// </summary>
        public DateTime modified_date { get; set; }
    }
}