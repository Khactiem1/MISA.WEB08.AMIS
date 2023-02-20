using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// nhân viên ứng với bảng employee trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Employee : BaseEntity
    {
        /// <summary>
        /// id nhân viên
        /// </summary>
        [Validate(PrimaryKey = true)]
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// id đơn vị
        /// </summary>7
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Mã đơn vị không được để trống.")]
        public Guid? UnitID { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        [Unique("Mã nhân viên <{0}> đã tồn tại trong hệ thống.")]
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Mã nhân viên không được để trống.")]
        [ColumnName(Name = "Mã nhân viên", Width = 16)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        [Validate(IsNotNullOrEmpty = true, ErrorMessage = "Tên nhân viên không được để trống.")]
        [ColumnName(Name = "Tên nhân viên", Width = 40)]
        public string EmployeeName { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        [ColumnName(Name = "Tên đơn vị", Width = 50)]
        public string UnitName { get; set; }

        /// <summary>
        /// ngày sinh
        /// </summary>
        [ColumnName(Name = "Ngày sinh", Width = 20, IsDate = true)]
        [Validate(MaxDateNow = true, ErrorMessage = "Ngày sinh phải nhỏ hơn ngày hiện tại.")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        [ColumnName(Name = "Giới tính", Width = 10, IsGender = true)]
        public Gender Gender { get; set; }

        /// <summary>
        /// chứng minh thư
        /// </summary>
        [ColumnName(Name = "Chứng minh thư", Width = 20)]
        public string IdentityCard { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        [ColumnName(Name = "Chi nhánh ngân hàng", Width = 40)]
        public string BranchBank { get; set; }

        /// <summary>
        /// chức danh
        /// </summary>
        [ColumnName(Name = "Chức danh", Width = 20)]
        public string EmployeeTitle { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        [ColumnName(Name = "Số tài khoản", Width = 20)]
        public string BankAccount { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        [ColumnName(Name = "Tên ngân hàng", Width = 40)]
        public string NameBank { get; set; }

        /// <summary>
        /// ngày cấp cmnd
        /// </summary>
        [ColumnName(Name = "Ngày cấp chứng minh thư", Width = 30, IsDate = true)]
        [Validate(MaxDateNow = true, ErrorMessage = "Ngày cấp phải nhỏ hơn ngày hiện tại.")]
        public DateTime? DayForIdentity { get; set; }

        /// <summary>
        /// địa chỉ cấp cmnd
        /// </summary>
        [ColumnName(Name = "Nơi cấp", Width = 40)]
        public string GrantAddressIdentity { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        [ColumnName(Name = "Điện thoại", Width = 20)]
        [Validate(PhoneNumber = true, ErrorMessage = "Không đúng định dạng số điện thoại.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        [ColumnName(Name = "Điện thoại cố định", Width = 20)]
        [Validate(PhoneNumber = true, ErrorMessage = "Không đúng định dạng số điện thoại cố định.")]
        public string LandlinePhone { get; set; }

        /// <summary>
        /// điạ chỉ email
        /// </summary>
        [ColumnName(Name = "Địa chỉ Email", Width = 30)]
        [Validate(Email = true, ErrorMessage = "Không đúng định dạng email.")]
        public string EmployeeEmail { get; set; }

        /// <summary>
        /// địa chỉ nhân viên
        /// </summary>
        [ColumnName(Name = "Địa chỉ", Width = 40)]
        public string EmployeeAddress { get; set; }

        /// <summary>
        /// Hoạt động hay không hoạt động
        /// </summary>
        [ColumnName(Name = "Trạng thái", Width = 20)]
        public bool IsActive { get; set; }
    }
}